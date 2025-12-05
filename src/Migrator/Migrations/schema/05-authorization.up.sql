CREATE SCHEMA IF NOT EXISTS author;

CREATE TABLE IF NOT EXISTS author.m_attributes
(
    id UUID NOT NULL DEFAULT GEN_RANDOM_UUID() PRIMARY KEY,
    created_by UUID NOT NULL REFERENCES authentication.t_users(id),
    created_at TIMESTAMP WITHOUT TIME ZONE NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_by UUID REFERENCES authentication.t_users(id),
    updated_at TIMESTAMP WITHOUT TIME ZONE,

    is_active BOOLEAN NOT NULL DEFAULT FALSE,
    inactive_at TIMESTAMP WITHOUT TIME ZONE,
    inactive_by UUID REFERENCES authentication.t_users(id),

    is_deleted BOOLEAN NOT NULL DEFAULT FALSE,
    deleted_at TIMESTAMP WITHOUT TIME ZONE,
    deleted_by UUID REFERENCES authentication.t_users(id),

    is_parameter BOOLEAN NOT NULL DEFAULT FALSE,
    is_required BOOLEAN NOT NULL DEFAULT FALSE,
    is_display BOOLEAN NOT NULL DEFAULT FALSE,

    category VARCHAR(32) DEFAULT 'USER' CHECK (category IN ('USER','ATTRIBUTE', 'RESOURCE','ENVIRONMENT')),
    key_group VARCHAR(128),
    name VARCHAR(128) NOT NULL,
    key VARCHAR(128) NOT NULL,
    data_type VARCHAR(64) NOT NULL,
    title VARCHAR(256),
    description TEXT
);
COMMENT ON TABLE author.m_attributes IS 'Stores attributes used for authorization, including type, category, and metadata';
COMMENT ON COLUMN author.m_attributes.id IS 'Primary key of the attribute';
COMMENT ON COLUMN author.m_attributes.created_by IS 'User who created the attribute';
COMMENT ON COLUMN author.m_attributes.created_at IS 'Timestamp when the attribute was created';
COMMENT ON COLUMN author.m_attributes.updated_by IS 'User who last updated the attribute';
COMMENT ON COLUMN author.m_attributes.updated_at IS 'Timestamp of last update';
COMMENT ON COLUMN author.m_attributes.is_active IS 'Indicates whether the attribute is active';
COMMENT ON COLUMN author.m_attributes.inactive_at IS 'Timestamp when the attribute became inactive';
COMMENT ON COLUMN author.m_attributes.inactive_by IS 'User who set inactive';
COMMENT ON COLUMN author.m_attributes.is_deleted IS 'Indicates if the attribute is deleted';
COMMENT ON COLUMN author.m_attributes.deleted_at IS 'Timestamp when the attribute was deleted';
COMMENT ON COLUMN author.m_attributes.deleted_by IS 'User who deleted the attribute';
COMMENT ON COLUMN author.m_attributes.is_parameter IS 'Indicates if the attribute is a system parameter';
COMMENT ON COLUMN author.m_attributes.is_required IS 'Indicates if the attribute is required';
COMMENT ON COLUMN author.m_attributes.is_display IS 'Indicates if the attribute should be displayed in UI';
COMMENT ON COLUMN author.m_attributes.category IS 'Category of the attribute: USER, ATTRIBUTE, RESOURCE, or ENVIRONMENT';
COMMENT ON COLUMN author.m_attributes.key_group IS 'Logical group or namespace of the attribute';
COMMENT ON COLUMN author.m_attributes.name IS 'Name of the attribute (internal identifier)';
COMMENT ON COLUMN author.m_attributes.key IS 'Unique key of the attribute';
COMMENT ON COLUMN author.m_attributes.data_type IS 'Data type of the attribute';
COMMENT ON COLUMN author.m_attributes.title IS 'Title or label of the attribute';
COMMENT ON COLUMN author.m_attributes.description IS 'Additional description of the attribute';

CREATE UNIQUE INDEX IF NOT EXISTS idx_m_attributes_key ON author.m_attributes(key);
COMMENT ON INDEX author.idx_m_attributes_key IS 'Unique index on attribute key to ensure uniqueness';

CREATE TABLE IF NOT EXISTS author.t_policies
(
    id UUID NOT NULL DEFAULT GEN_RANDOM_UUID() PRIMARY KEY,
    created_by UUID NOT NULL REFERENCES authentication.t_users(id),
    created_at TIMESTAMP WITHOUT TIME ZONE NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_by UUID REFERENCES authentication.t_users(id),
    updated_at TIMESTAMP WITHOUT TIME ZONE,

    is_active BOOLEAN NOT NULL DEFAULT FALSE,
    inactive_at TIMESTAMP WITHOUT TIME ZONE,
    inactive_by UUID REFERENCES authentication.t_users(id),

    is_deleted BOOLEAN NOT NULL DEFAULT FALSE,
    deleted_at TIMESTAMP WITHOUT TIME ZONE,
    deleted_by UUID REFERENCES authentication.t_users(id),

    name VARCHAR(128) NOT NULL,
    description TEXT,

    code VARCHAR(32) NOT NULL,
    effect VARCHAR(16) NOT NULL CHECK (effect IN ('ALLOW', 'DENY')),
    action VARCHAR(128) NOT NULL,
    resource VARCHAR(256) NOT NULL,
    condition_logic TEXT
);
COMMENT ON TABLE author.t_policies IS 'Stores authorization policies including effect, actions, resources, and conditions';
COMMENT ON COLUMN author.t_policies.id IS 'Primary key of the policy';
COMMENT ON COLUMN author.t_policies.created_by IS 'User who created the policy';
COMMENT ON COLUMN author.t_policies.created_at IS 'Timestamp when the policy was created';
COMMENT ON COLUMN author.t_policies.updated_by IS 'User who last updated the policy';
COMMENT ON COLUMN author.t_policies.updated_at IS 'Timestamp when the policy was last updated';
COMMENT ON COLUMN author.t_policies.is_active IS 'Indicates whether the policy is active';
COMMENT ON COLUMN author.t_policies.inactive_at IS 'Timestamp when the policy became inactive';
COMMENT ON COLUMN author.t_policies.inactive_by IS 'User who marked the policy as inactive';
COMMENT ON COLUMN author.t_policies.is_deleted IS 'Indicates whether the policy is deleted';
COMMENT ON COLUMN author.t_policies.deleted_at IS 'Timestamp when the policy was deleted';
COMMENT ON COLUMN author.t_policies.deleted_by IS 'User who deleted the policy';
COMMENT ON COLUMN author.t_policies.name IS 'Name of the policy';
COMMENT ON COLUMN author.t_policies.description IS 'Description of the policy';
COMMENT ON COLUMN author.t_policies.code IS 'Unique code of the policy';
COMMENT ON COLUMN author.t_policies.effect IS 'Policy effect: ALLOW or DENY';
COMMENT ON COLUMN author.t_policies.action IS 'Action that the policy governs';
COMMENT ON COLUMN author.t_policies.resource IS 'Resource that the policy applies to';
COMMENT ON COLUMN author.t_policies.condition_logic IS 'Optional logic conditions in JSON or text format';

CREATE UNIQUE INDEX IF NOT EXISTS idx_t_policies_name ON author.t_policies(name);
COMMENT ON INDEX author.idx_t_policies_name IS 'Unique index on policy name to ensure uniqueness';

CREATE TABLE IF NOT EXISTS author.t_policy_attribute_mappings
(
    id UUID NOT NULL DEFAULT GEN_RANDOM_UUID() PRIMARY KEY,
    created_by UUID NOT NULL REFERENCES authentication.t_users(id),
    created_at TIMESTAMP WITHOUT TIME ZONE NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_by UUID REFERENCES authentication.t_users(id),
    updated_at TIMESTAMP WITHOUT TIME ZONE,

    is_active BOOLEAN NOT NULL DEFAULT FALSE,
    inactive_at TIMESTAMP WITHOUT TIME ZONE,
    inactive_by UUID REFERENCES authentication.t_users(id),

    is_deleted BOOLEAN NOT NULL DEFAULT FALSE,
    deleted_at TIMESTAMP WITHOUT TIME ZONE,
    deleted_by UUID REFERENCES authentication.t_users(id),

    policy_id UUID NOT NULL REFERENCES author.t_policies(id),
    attribute_id UUID NOT NULL REFERENCES author.m_attributes(id),
    operator VARCHAR(32) NOT NULL,
    expected_value TEXT NOT NULL,
    logic_group VARCHAR(16) DEFAULT 'AND' CHECK (logic_group IN ('AND','OR'))
);
COMMENT ON TABLE author.t_policy_attribute_mappings IS 'Maps policies to attributes with operators, expected values, and logical grouping';
COMMENT ON COLUMN author.t_policy_attribute_mappings.id IS 'Primary key of the mapping';
COMMENT ON COLUMN author.t_policy_attribute_mappings.created_by IS 'User who created the mapping';
COMMENT ON COLUMN author.t_policy_attribute_mappings.created_at IS 'Timestamp when the mapping was created';
COMMENT ON COLUMN author.t_policy_attribute_mappings.updated_by IS 'User who last updated the mapping';
COMMENT ON COLUMN author.t_policy_attribute_mappings.updated_at IS 'Timestamp when the mapping was last updated';
COMMENT ON COLUMN author.t_policy_attribute_mappings.is_active IS 'Indicates whether the mapping is active';
COMMENT ON COLUMN author.t_policy_attribute_mappings.inactive_at IS 'Timestamp when the mapping became inactive';
COMMENT ON COLUMN author.t_policy_attribute_mappings.inactive_by IS 'User who set the mapping as inactive';
COMMENT ON COLUMN author.t_policy_attribute_mappings.is_deleted IS 'Indicates whether the mapping is deleted';
COMMENT ON COLUMN author.t_policy_attribute_mappings.deleted_at IS 'Timestamp when the mapping was deleted';
COMMENT ON COLUMN author.t_policy_attribute_mappings.deleted_by IS 'User who deleted the mapping';
COMMENT ON COLUMN author.t_policy_attribute_mappings.policy_id IS 'Reference to the policy (author.t_policies.id)';
COMMENT ON COLUMN author.t_policy_attribute_mappings.attribute_id IS 'Reference to the attribute (author.m_attributes.id)';
COMMENT ON COLUMN author.t_policy_attribute_mappings.operator IS 'Operator used for attribute evaluation';
COMMENT ON COLUMN author.t_policy_attribute_mappings.expected_value IS 'Expected value for the attribute';
COMMENT ON COLUMN author.t_policy_attribute_mappings.logic_group IS 'Logical grouping for multiple conditions: AND or OR';

CREATE INDEX IF NOT EXISTS idx_t_policy_attribute_mappings_policy_id_attribute_id ON author.t_policy_attribute_mappings(policy_id, attribute_id);
COMMENT ON INDEX author.idx_t_policy_attribute_mappings_policy_id_attribute_id IS 'Index to optimize queries filtering by policy_id and attribute_id.';

CREATE TABLE IF NOT EXISTS author.t_policy_decision_logs
(
    id UUID NOT NULL DEFAULT GEN_RANDOM_UUID() PRIMARY KEY,
    created_by UUID NOT NULL REFERENCES authentication.t_users(id),
    created_at TIMESTAMP WITHOUT TIME ZONE NOT NULL DEFAULT CURRENT_TIMESTAMP,

    user_id UUID NOT NULL REFERENCES authentication.t_users(id),
    policy_id UUID REFERENCES author.t_policies(id),
    resource VARCHAR(256),
    action VARCHAR(128),
    decision VARCHAR(16) NOT NULL CHECK (decision IN ('ALLOW','DENY')),
    evaluated_attributes JSONB,
    reason TEXT
);
COMMENT ON TABLE author.t_policy_decision_logs IS 'Stores logs of policy decisions for users and resources';
COMMENT ON COLUMN author.t_policy_decision_logs.id IS 'Primary key of the decision log';
COMMENT ON COLUMN author.t_policy_decision_logs.created_by IS 'User who created the log entry';
COMMENT ON COLUMN author.t_policy_decision_logs.created_at IS 'Timestamp when the log entry was created';
COMMENT ON COLUMN author.t_policy_decision_logs.user_id IS 'Reference to the user for whom the policy was evaluated';
COMMENT ON COLUMN author.t_policy_decision_logs.policy_id IS 'Reference to the policy that was evaluated';
COMMENT ON COLUMN author.t_policy_decision_logs.resource IS 'The resource being accessed or evaluated';
COMMENT ON COLUMN author.t_policy_decision_logs.action IS 'The action attempted on the resource';
COMMENT ON COLUMN author.t_policy_decision_logs.decision IS 'Result of the policy evaluation: ALLOW or DENY';
COMMENT ON COLUMN author.t_policy_decision_logs.evaluated_attributes IS 'JSONB of attributes used in the policy evaluation';
COMMENT ON COLUMN author.t_policy_decision_logs.reason IS 'Reason or explanation for the decision';

CREATE INDEX IF NOT EXISTS idx_t_policy_decision_logs_user_id_policy_id ON author.t_policy_decision_logs(user_id, policy_id);
CREATE INDEX IF NOT EXISTS idx_t_policy_decision_logs_user_id ON author.t_policy_decision_logs(user_id);
CREATE INDEX IF NOT EXISTS idx_t_policy_decision_logs_policy_id ON author.t_policy_decision_logs(policy_id);
COMMENT ON INDEX author.idx_t_policy_decision_logs_user_id_policy_id IS 'Index to optimize queries filtering by user_id and policy_id.';
COMMENT ON INDEX author.idx_t_policy_decision_logs_user_id IS 'Index to optimize queries filtering by user_id.';
COMMENT ON INDEX author.idx_t_policy_decision_logs_policy_id IS 'Index to optimize queries filtering by policy_id.';

CREATE TABLE IF NOT EXISTS author.t_user_attribute_mappings
(
    id UUID NOT NULL DEFAULT GEN_RANDOM_UUID() PRIMARY KEY,
    created_by UUID NOT NULL REFERENCES authentication.t_users(id),
    created_at TIMESTAMP WITHOUT TIME ZONE NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_by UUID REFERENCES authentication.t_users(id),
    updated_at TIMESTAMP WITHOUT TIME ZONE,

    is_active BOOLEAN NOT NULL DEFAULT FALSE,
    inactive_at TIMESTAMP WITHOUT TIME ZONE,
    inactive_by UUID REFERENCES authentication.t_users(id),

    is_deleted BOOLEAN NOT NULL DEFAULT FALSE,
    deleted_at TIMESTAMP WITHOUT TIME ZONE,
    deleted_by UUID REFERENCES authentication.t_users(id),

    user_id UUID NOT NULL REFERENCES authentication.t_users(id),
    attribute_id UUID NOT NULL REFERENCES author.m_attributes(id),
    value BYTEA NOT NULL
);
COMMENT ON TABLE author.t_user_attribute_mappings IS 'Maps users to their attributes and corresponding values';
COMMENT ON COLUMN author.t_user_attribute_mappings.id IS 'Primary key of the user-attribute mapping';
COMMENT ON COLUMN author.t_user_attribute_mappings.created_by IS 'User who created the mapping';
COMMENT ON COLUMN author.t_user_attribute_mappings.created_at IS 'Timestamp when the mapping was created';
COMMENT ON COLUMN author.t_user_attribute_mappings.updated_by IS 'User who last updated the mapping';
COMMENT ON COLUMN author.t_user_attribute_mappings.updated_at IS 'Timestamp when the mapping was last updated';
COMMENT ON COLUMN author.t_user_attribute_mappings.is_active IS 'Indicates whether the mapping is active';
COMMENT ON COLUMN author.t_user_attribute_mappings.inactive_at IS 'Timestamp when mapping became inactive';
COMMENT ON COLUMN author.t_user_attribute_mappings.inactive_by IS 'User who set the mapping as inactive';
COMMENT ON COLUMN author.t_user_attribute_mappings.is_deleted IS 'Indicates whether the mapping is deleted';
COMMENT ON COLUMN author.t_user_attribute_mappings.deleted_at IS 'Timestamp when the mapping was deleted';
COMMENT ON COLUMN author.t_user_attribute_mappings.deleted_by IS 'User who deleted the mapping';
COMMENT ON COLUMN author.t_user_attribute_mappings.user_id IS 'Reference to the user';
COMMENT ON COLUMN author.t_user_attribute_mappings.attribute_id IS 'Reference to the attribute';
COMMENT ON COLUMN author.t_user_attribute_mappings.value IS 'Value of the attribute for the user';

CREATE INDEX IF NOT EXISTS idx_t_user_attribute_mappings_user_id_attribute_id ON author.t_user_attribute_mappings(user_id, attribute_id);
COMMENT ON INDEX author.idx_t_user_attribute_mappings_user_id_attribute_id IS 'Index to optimize queries filtering by user_id and attribute_id.';