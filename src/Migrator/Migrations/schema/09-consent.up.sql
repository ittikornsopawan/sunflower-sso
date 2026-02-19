CREATE SCHEMA IF NOT EXISTS consent;

CREATE TABLE IF NOT EXISTS consent.m_consent_types
(
    id UUID NOT NULL DEFAULT GEN_RANDOM_UUID() PRIMARY KEY,
    created_by UUID,
    created_at TIMESTAMP WITHOUT TIME ZONE NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_by UUID,
    updated_at TIMESTAMP WITHOUT TIME ZONE,

    row_status VARCHAR(16) NOT NULL DEFAULT 'ACTIVE' CHECK (row_status IN ('ACTIVE', 'INACTIVE', 'DELETED', 'REVOKED')),

    name VARCHAR(128) NOT NULL,
    descruiption TEXT,
    is_required BOOLEAN NOT NULL DEFAULT FALSE,
    latest_version VARCHAR(8) NOT NULL DEFAULT '1.0.0'
);
COMMENT ON TABLE consent.m_consent_types IS 'Master table for consent types';
COMMENT ON COLUMN consent.m_consent_types.id IS 'Primary key';
COMMENT ON COLUMN consent.m_consent_types.created_by IS 'User who created the record';
COMMENT ON COLUMN consent.m_consent_types.created_at IS 'Timestamp when the record was created';
COMMENT ON COLUMN consent.m_consent_types.updated_by IS 'User who last updated the record';
COMMENT ON COLUMN consent.m_consent_types.updated_at IS 'Timestamp when the record was last updated';
COMMENT ON COLUMN consent.m_consent_types.row_status IS 'Status of the consent type record: ACTIVE, INACTIVE, DELETED, or REVOKED';
COMMENT ON COLUMN consent.m_consent_types.name IS 'Name of the consent type';
COMMENT ON COLUMN consent.m_consent_types.descruiption IS 'Description of the consent type';
COMMENT ON COLUMN consent.m_consent_types.is_required IS 'Indicates if the consent type is mandatory for users';
COMMENT ON COLUMN consent.m_consent_types.latest_version IS 'Latest version of the consent type';

CREATE UNIQUE INDEX IF NOT EXISTS uq_m_consent_types_name ON consent.m_consent_types (name);
COMMENT ON INDEX consent.uq_m_consent_types_name IS 'Unique index to ensure consent type names are unique';

CREATE TABLE IF NOT EXISTS consent.t_consents
(
    id UUID NOT NULL DEFAULT GEN_RANDOM_UUID() PRIMARY KEY,
    created_by UUID,
    created_at TIMESTAMP WITHOUT TIME ZONE NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_by UUID,
    updated_at TIMESTAMP WITHOUT TIME ZONE,

    row_status VARCHAR(16) NOT NULL DEFAULT 'ACTIVE' CHECK (row_status IN ('ACTIVE', 'INACTIVE', 'DELETED', 'REVOKED')),

    consent_type_id UUID NOT NULL REFERENCES consent.m_consent_types(id),
    version VARCHAR(8) NOT NULL,
    name VARCHAR(128) NOT NULL,
    description TEXT,
    content TEXT NOT NULL,
    language VARCHAR(8) NOT NULL DEFAULT 'en'
);
COMMENT ON TABLE consent.t_consents IS 'Table for storing different versions of consents for each consent type';
COMMENT ON COLUMN consent.t_consents.id IS 'Primary key';
COMMENT ON COLUMN consent.t_consents.created_by IS 'User who created the record';
COMMENT ON COLUMN consent.t_consents.created_at IS 'Timestamp when the record was created';
COMMENT ON COLUMN consent.t_consents.updated_by IS 'User who last updated the record';
COMMENT ON COLUMN consent.t_consents.updated_at IS 'Timestamp when the record was last updated';
COMMENT ON COLUMN consent.t_consents.row_status IS 'Status of the consent version record: ACTIVE, INACTIVE, DELETED, or REVOKED';
COMMENT ON COLUMN consent.t_consents.consent_type_id IS 'Foreign key referencing the consent type';
COMMENT ON COLUMN consent.t_consents.version IS 'Version of the consent';
COMMENT ON COLUMN consent.t_consents.name IS 'Name of the consent version';
COMMENT ON COLUMN consent.t_consents.description IS 'Description of the consent version';
COMMENT ON COLUMN consent.t_consents.content IS 'Content of the consent';
COMMENT ON COLUMN consent.t_consents.language IS 'Language of the consent content';

CREATE UNIQUE INDEX IF NOT EXISTS uq_t_consents_type_version_language ON consent.t_consents (consent_type_id, version, language);
COMMENT ON INDEX consent.uq_t_consents_type_version_language IS 'Unique index to ensure a consent type version and language combination is unique';

CREATE TABLE IF NOT EXISTS consent.t_user_consents
(
    id UUID NOT NULL DEFAULT GEN_RANDOM_UUID() PRIMARY KEY,
    created_by UUID,
    created_at TIMESTAMP WITHOUT TIME ZONE NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_by UUID,
    updated_at TIMESTAMP WITHOUT TIME ZONE,

    row_status VARCHAR(16) NOT NULL DEFAULT 'ACTIVE' CHECK (row_status IN ('ACTIVE', 'INACTIVE', 'DELETED', 'REVOKED')),

    user_id UUID NOT NULL REFERENCES authentication.t_users(id),
    consent_type_id UUID NOT NULL REFERENCES consent.m_consent_types(id),
    consent_id UUID NOT NULL REFERENCES consent.t_consents(id),
    version VARCHAR(8) NOT NULL,
    result BOOLEAN NOT NULL DEFAULT FALSE
);
COMMENT ON TABLE consent.t_user_consents IS 'Table for tracking user consents for different consent types and versions';
COMMENT ON COLUMN consent.t_user_consents.id IS 'Primary key';
COMMENT ON COLUMN consent.t_user_consents.created_by IS 'User who created the record';
COMMENT ON COLUMN consent.t_user_consents.created_at IS 'Timestamp when the record was created';
COMMENT ON COLUMN consent.t_user_consents.updated_by IS 'User who last updated the record';
COMMENT ON COLUMN consent.t_user_consents.updated_at IS 'Timestamp when the record was last updated';
COMMENT ON COLUMN consent.t_user_consents.row_status IS 'Status of the user consent record: ACTIVE, INACTIVE, DELETED, or REVOKED';
COMMENT ON COLUMN consent.t_user_consents.user_id IS 'Foreign key referencing the user';
COMMENT ON COLUMN consent.t_user_consents.consent_type_id IS 'Foreign key referencing the consent type';
COMMENT ON COLUMN consent.t_user_consents.consent_id IS 'Foreign key referencing the specific consent record';
COMMENT ON COLUMN consent.t_user_consents.version IS 'Version of the consent given by the user';
COMMENT ON COLUMN consent.t_user_consents.result IS 'Indicates whether the user gave or denied consent';

CREATE UNIQUE INDEX IF NOT EXISTS uq_t_user_consents_user_consent_id ON consent.t_user_consents (user_id, consent_type_id, version);
CREATE INDEX IF NOT EXISTS idx_t_user_consents_user_id ON consent.t_user_consents (user_id);
CREATE INDEX IF NOT EXISTS idx_t_user_consents_consent_id ON consent.t_user_consents (consent_id);
COMMENT ON INDEX consent.uq_t_user_consents_user_consent_id IS 'Unique index to ensure a user can have only one consent record per consent type and version';
COMMENT ON INDEX consent.idx_t_user_consents_user_id IS 'Index to optimize queries filtering by user_id';
COMMENT ON INDEX consent.idx_t_user_consents_consent_id IS 'Index to optimize queries filtering by consent_id';