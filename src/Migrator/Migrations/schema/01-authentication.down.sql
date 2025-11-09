DROP INDEX IF EXISTS authentication.idx_t_user_open_authentication_provider_provider_user_id;
DROP INDEX IF EXISTS authentication.idx_t_user_referrer_mappings_user_id;
DROP INDEX IF EXISTS authentication.uq_t_user_referrer_mappings_user_id_referral_id;
DROP INDEX IF EXISTS authentication.idx_t_user_authentications_user_id;
DROP INDEX IF EXISTS authentication.idx_t_users_username;

DROP TABLE IF EXISTS authentication.t_user_open_authentication CASCADE;
DROP TABLE IF EXISTS authentication.t_user_referrer_mappings CASCADE;
DROP TABLE IF EXISTS authentication.t_user_authentications CASCADE;
DROP TABLE IF EXISTS authentication.t_users CASCADE;

DROP SCHEMA IF EXISTS authentication CASCADE;