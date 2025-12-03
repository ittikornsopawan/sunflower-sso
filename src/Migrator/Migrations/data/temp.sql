-- AWC Welcome Series Flow || AWC Reactivation Flow
SELECT
    cu.date_joined as registered_at,
    cu.update_at as updated_at,
    cu.id as pikul_unique_id,
    cu.membership_code as pikul_membership_id,
    NULLIF(cu.first_name, '') as first_name,
    NULLIF(cu.last_name, '') as last_name,
    NULLIF(cu.fullname, '') as full_name,
    CASE
        WHEN NULLIF(cu.email, '') IS NOT NULL
        AND NULLIF(cu.tenant_company_email, '') IS NULL THEN cu.email
        WHEN NULLIF(cu.email, '') IS NULL
        AND NULLIF(cu.tenant_company_email, '') IS NOT NULL THEN cu.tenant_company_email
        ELSE NULL
    END as email,
    NULLIF(cu.mobile_number, '') as sms,
    cu.date_of_birth as birth_date,
    cu.gender,
    cu.ewallet_id,
    CASE
        WHEN ewallet_id IS NOT NULL
        AND wr.id IS NOT NULL THEN wr.create_at
        WHEN ewallet_id IS NOT NULL
        AND wr.id IS NULL THEN cu.date_joined
        ELSE NULL
    END as ewallet_registered_at,
    CASE
        WHEN do.first_purchase_at IS NOT NULL THEN TRUE
        ELSE FALSE
    END as is_first_purchase,
    do.first_purchase_at,
    do.order_no,
    er.last_activity_at,
    TIMESTAMPDIFF(DAY, er.last_activity_at, CURRENT_TIMESTAMP) AS diff_day,
    CASE
        WHEN DATE(cu.date_joined) = DATE_SUB(CURDATE(), INTERVAL 1 DAY) THEN 'SIGN_UP'
        ELSE 'UPDATED'
    END AS event_profile,
    CASE
        WHEN cu.ewallet_id IS NULL
        AND wr.id IS NULL THEN 'WALLET_NOT_REGISTRATION'
        WHEN cu.ewallet_id IS NOT NULL
        OR wr.id IS NOT NULL THEN 'WALLET_REGISTRATION'
    END as event_wallet,
    CASE
        WHEN do.order_no IS NULL
        OR do.order_no = 0 THEN 'NO_PURCHASE'
        WHEN do.order_no > 0 THEN 'ORDER_PURCHASE'
    END as event_purchase
FROM
    core_user cu
    LEFT JOIN wallet_registration wr ON cu.id = wr.user_id
    AND wr.wallet_registration_status IS NOT NULL
    AND wr.wallet_registration_status NOT IN('fail', 'reject')
    LEFT JOIN(
        SELECT
            do.user_id,
            MIN(do.status_paid_at) as first_purchase_at,
            COUNT(1) as order_no
        FROM
            deal_order do
        WHERE
            do.status_paid_at IS NOT NULL
        GROUP BY
            do.user_id
    ) as do ON cu.id = do.user_id
    LEFT JOIN(
        SELECT
            er.user_id,
            MAX(er.`datetime`) as last_activity_at
        FROM
            easyaudit_requestevent er
        GROUP BY
            er.user_id
    ) as er ON cu.id = er.user_id
WHERE
    DATE(cu.date_joined) <= DATE_SUB(CURDATE(), INTERVAL 1 DAY)
ORDER BY
    cu.date_joined desc,
    cu.membership_code;

-- AWC First-Time Purchase Offer Flow
WITH
    first_order AS(
        SELECT
            user_id,
            MIN(create_at) AS first_order_at
        FROM
            deal_order
        GROUP BY
            user_id
    ),
    order_items AS(
        SELECT
            order_id,
            SUM(quantity) AS total_quantity
        FROM
            deal_orderitem
        GROUP BY
            order_id
    )
SELECT
    cu.id AS pikul_unique_id,
    cu.membership_code AS pikul_membership_id,
    NULLIF(cu.fullname, '') AS full_name,
    do.id AS order_id,
    do.code AS transaction_no,
    do.create_at AS order_at,
    do.status_paid_at AS succeed_at,
    COALESCE(oi.total_quantity, 0) AS total_quantity,
    pc.discount_name_en AS discount_name,
    NULLIF(do.discount_code, '') AS discount_code,
    pc.start_date AS discount_effectives_at,
    pc.end_date AS discount_expires_at,
    do.total_price_before_discount AS before_discount_price,
    do.discount_amount AS voucher_discount_price,
    do.instant_use_amount AS voucher_instant_discount_price,
    do.instant_use_pikul_amount AS pikul_point_discount,
    do.discount_amount + do.instant_use_amount + do.instant_use_pikul_amount AS total_discount,
    do.total_price AS after_discount_price,
    CASE
        WHEN do.create_at = fo.first_order_at THEN 1
        ELSE 0
    END AS is_first_purchase
FROM
    core_user cu
    LEFT JOIN deal_order do ON cu.id = do.user_id
    LEFT JOIN order_items oi ON do.id = oi.order_id
    LEFT JOIN promotioncode_discountcode pd ON BINARY do.discount_code = BINARY pd.discount_code
    LEFT JOIN promotioncode_campaigndiscount pc ON pd.campaigndiscount_id = pc.id
    LEFT JOIN first_order fo ON do.user_id = fo.user_id
ORDER BY
    cu.id,
    cu.membership_code,
    do.id,
    do.code;

-- 4. AWC Abandoned Browse Flow
-- Create TABLE(DON'T EXECUTION)
CREATE TABLE
    IF NOT EXISTS deal_activity(
        id BIGINT AUTO_INCREMENT PRIMARY KEY,
        uuid CHAR(32) NOT NULL DEFAULT(UUID()) UNIQUE,
        user_id BIGINT NOT NULL REFERENCES core_user(id),
        deal_id BIGINT NOT NULL REFERENCES deal_deal(id),
        activity_at TIMESTAMP(6) DEFAULT CURRENT_TIMESTAMP(6)
    );

-- Get activity with user(DON'T EXECUTION)
SELECT
    cu.id AS pikul_unique_id,
    cu.membership_code AS pikul_membership_id,
    NULLIF(cu.fullname, '') AS full_name,
    dd.id as deal_id,
    dd.title_en,
    da.activity_at
FROM
    core_user cu
    LEFT JOIN deal_activity da ON da.user_id = cu.id
    LEFT JOIN deal_deal dd ON da.deal_id = dd.id;

SELECT
    NULLIF(cu.first_name, '') AS first_name,
    NULLIF(cu.last_name, '') AS last_name,
    NULLIF(cu.fullname, '') AS full_name,
    CAST(SUBSTRING_INDEX(er.url, '/', -1) AS UNSIGNED) as deal_id,
    dd.title_en,
    dd.title_th,
    er.`datetime`
FROM
    easyaudit_requestevent er
    LEFT JOIN core_user cu ON er.user_id = cu.id
    LEFT JOIN deal_deal dd ON CAST(SUBSTRING_INDEX(er.url, '/', -1) AS UNSIGNED) = dd.id
WHERE
    url LIKE '/v1/public/deals/%'
    -- 5. AWC Abandoned Cart Flow
    -- Create TABLE(DON'T EXECUTION)
CREATE TABLE
    IF NOT EXISTS carts_items(
        id BIGINT AUTO_INCREMENT PRIMARY KEY,
        uuid CHAR(32) NOT NULL DEFAULT(UUID()) UNIQUE,
        user_id BIGINT NOT NULL REFERENCES core_user(id),
        deal_id BIGINT NOT NULL REFERENCES deal_deal(id),
        quantity INT NOT NULL,
        remark VARCHAR(64),
        is_delete BOOLEAN DEFAULT TRUE,
        delete_at DATETIME(6),
        create_by BIGINT NOT NULL REFERENCES core_user(id),
        create_at TIMESTAMP(6) DEFAULT CURRENT_TIMESTAMP(6)
    );

-- Get user's cart(DON'T EXECUTION)
SELECT
    cu.id AS pikul_unique_id,
    cu.membership_code AS pikul_membership_id,
    NULLIF(cu.fullname, '') AS full_name,
    dd.id as deal_id,
    dd.title_en,
    dc.quantity,
    da.create_by as add_cart_at
FROM
    core_user cu
    LEFT JOIN deal_cart dc ON cu.id = dc.user_id
    LEFT JOIN deal_deal dd ON da.deal_id = dd.id;

-- 
/* 10. Promotion_Master
KPI : Promotion_Master
 */
select
    pcdis.id as campaigndiscount_id,
    pcdis.create_at as 'campaigndiscount_create_at',
    pcdis.update_at as 'campaigndiscount_update_at',
    pcdis.discount_id,
    pdc.discount_code,
    pcdis.discount_name_en,
    pcdis.start_date,
    pcdis.end_date,
    pcdis.number_of_discount,
    pcdis.minimum_spending,
    pcdis.instant_discount,
    pcdis.max_discount,
    pcdis.percentage_discount,
    pcdis.coupon_type,
    pcdis.is_single_coupon,
    pcdis.is_bundle,
    is_marketplace,
    pcdis.code_type,
    pcdis.number_of_code_needed,
    pcdis.code_prefix,
    pcdis.code_suffix,
    pcdis.key_mechanics_id,
    pck.name as keymechanics_name,
    pcdis.promotioncode_campaign_id as promotioncode_campaignid,
    pdc.campaigndiscount_id,
    pcam.title as promotioncode_campaign_name,
    pcdis.create_at,
    pcdis.update_at
from
    promotioncode_campaigndiscount pcdis
    left join promotioncode_discountcode pdc on pdc.id = pcdis.discount_id
    and pdc.is_active = 1
    left join promotioncode_campaign pcam on pcam.id = pcdis.promotioncode_campaign_id
    and pcam.is_active = 1
    left join promotioncode_keymechanics pck on pck.is_active = 1
    and pck.id = pcdis.key_mechanics_id;

/* 11. PromotionTransaction 
KPI:Promotion_Transaction
 */
SELECT
    do.id as order_id,
    do.code as transaction_code,
    do.create_at as 'transaction_create_at',
    do.update_at as 'transaction_update_at',
    do.status_paid_at as 'transaction_compete_at',
    do.discount_code,
    pt.status as 'transactopm_promotion_status',
    pt.cash_purchase as 'transaction_promotion_cash',
    pt.cost as 'transaction_promotion_cost',
    do.total_price_before_discount,
    do.discount_amount,
    do.total_price
FROM
    deal_order do
    INNER JOIN promotioncode_transactiondiscount pt ON do.discount_code = pt.code
WHERE
    NULLIF(do.discount_code, '') IS NOT NULL