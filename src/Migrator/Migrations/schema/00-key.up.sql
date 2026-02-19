CREATE SCHEMA IF NOT EXISTS key;
COMMENT ON SCHEMA key IS 'Schema for managing cryptographic keys used in various algorithms and security processes.';

CREATE TABLE IF NOT EXISTS key.m_algorithms
(
    id UUID NOT NULL DEFAULT GEN_RANDOM_UUID() PRIMARY KEY,
    created_by UUID,
    created_at TIMESTAMP WITHOUT TIME ZONE NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_by UUID,
    updated_at TIMESTAMP WITHOUT TIME ZONE,

    row_status VARCHAR(16) NOT NULL DEFAULT 'ACTIVE' CHECK (row_status IN ('ACTIVE', 'INACTIVE', 'DELETED', 'REVOKED')),

    effective_at TIMESTAMP WITHOUT TIME ZONE NOT NULL DEFAULT CURRENT_TIMESTAMP,
    expires_at TIMESTAMP WITHOUT TIME ZONE CHECK (expires_at IS NOT NULL AND expires_at > effective_at),

    name VARCHAR(128) NOT NULL,
    algorithm BYTEA NOT NULL,
    key_required JSONB
);
COMMENT ON TABLE key.m_algorithms IS 'Stores algorithm definitions including their metadata and required keys';
COMMENT ON COLUMN key.m_algorithms.id IS 'Primary key of the algorithm record';
COMMENT ON COLUMN key.m_algorithms.created_by IS 'User who created the algorithm record';
COMMENT ON COLUMN key.m_algorithms.created_at IS 'Timestamp when the algorithm record was created';
COMMENT ON COLUMN key.m_algorithms.updated_by IS 'User who last updated the algorithm record';
COMMENT ON COLUMN key.m_algorithms.updated_at IS 'Timestamp of last update';
COMMENT ON COLUMN key.m_algorithms.row_status IS 'Status of the record (ACTIVE, INACTIVE, DELETED, REVOKED)';
;
COMMENT ON COLUMN key.m_algorithms.effective_at IS 'Effective start timestamp of the algorithm';
COMMENT ON COLUMN key.m_algorithms.expires_at IS 'Expiration timestamp of the algorithm';
COMMENT ON COLUMN key.m_algorithms.name IS 'Name of the algorithm';
COMMENT ON COLUMN key.m_algorithms.algorithm IS 'Binary representation of the algorithm';
COMMENT ON COLUMN key.m_algorithms.key_required IS 'JSONB containing keys required for the algorithm';

CREATE UNIQUE INDEX IF NOT EXISTS idx_m_algorithms_name ON key.m_algorithms(name);
COMMENT ON INDEX key.idx_m_algorithms_name IS 'Unique index to ensure algorithm names are unique.';

CREATE TABLE IF NOT EXISTS key.m_key_types
(
    id UUID NOT NULL DEFAULT GEN_RANDOM_UUID() PRIMARY KEY,
    created_by UUID,
    created_at TIMESTAMP WITHOUT TIME ZONE NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_by UUID,
    updated_at TIMESTAMP WITHOUT TIME ZONE,

    row_status VARCHAR(16) NOT NULL DEFAULT 'ACTIVE' CHECK (row_status IN ('ACTIVE', 'INACTIVE', 'DELETED', 'REVOKED')),

    effective_at TIMESTAMP WITHOUT TIME ZONE NOT NULL DEFAULT CURRENT_TIMESTAMP,
    expires_at TIMESTAMP WITHOUT TIME ZONE CHECK (expires_at IS NOT NULL AND expires_at > effective_at),

    name VARCHAR(128) NOT NULL,
    title VARCHAR(512),
    description TEXT
);
COMMENT ON TABLE key.m_key_types IS 'Stores types of keys along with metadata for key management';
COMMENT ON COLUMN key.m_key_types.id IS 'Primary key of the key type record';
COMMENT ON COLUMN key.m_key_types.created_by IS 'User who created the key type record';
COMMENT ON COLUMN key.m_key_types.created_at IS 'Timestamp when the key type record was created';
COMMENT ON COLUMN key.m_key_types.updated_by IS 'User who last updated the key type record';
COMMENT ON COLUMN key.m_key_types.updated_at IS 'Timestamp of last update';
COMMENT ON COLUMN key.m_key_types.row_status IS 'Status of the key type record (ACTIVE, INACTIVE, DELETED, REVOKED)';
;
COMMENT ON COLUMN key.m_key_types.effective_at IS 'Effective start timestamp of the key type';
COMMENT ON COLUMN key.m_key_types.expires_at IS 'Expiration timestamp of the key type';
COMMENT ON COLUMN key.m_key_types.name IS 'Name of the key type';
COMMENT ON COLUMN key.m_key_types.title IS 'Title or label of the key type';
COMMENT ON COLUMN key.m_key_types.description IS 'Detailed description of the key type';

CREATE UNIQUE INDEX IF NOT EXISTS idx_m_key_types_name ON key.m_key_types(name);
COMMENT ON INDEX key.idx_m_key_types_name IS 'Unique index to ensure key type names are unique.';

CREATE TABLE IF NOT EXISTS key.m_keys
(
    id UUID NOT NULL DEFAULT GEN_RANDOM_UUID() PRIMARY KEY,
    created_by UUID,
    created_at TIMESTAMP WITHOUT TIME ZONE NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_by UUID,
    updated_at TIMESTAMP WITHOUT TIME ZONE,

    row_status VARCHAR(16) NOT NULL DEFAULT 'ACTIVE' CHECK (row_status IN ('ACTIVE', 'INACTIVE', 'DELETED', 'REVOKED')),

    effective_at TIMESTAMP WITHOUT TIME ZONE NOT NULL DEFAULT CURRENT_TIMESTAMP,
    expires_at TIMESTAMP WITHOUT TIME ZONE CHECK (expires_at IS NOT NULL AND expires_at > effective_at),

    type_id UUID NOT NULL REFERENCES key.m_key_types(id),
    key BYTEA NOT NULL
);
COMMENT ON TABLE key.m_keys IS 'Stores cryptographic keys along with metadata and type reference';
COMMENT ON COLUMN key.m_keys.id IS 'Primary key of the table';
COMMENT ON COLUMN key.m_keys.created_by IS 'Reference to the user who created the record';
COMMENT ON COLUMN key.m_keys.created_at IS 'Timestamp when the record was created';
COMMENT ON COLUMN key.m_keys.updated_by IS 'Reference to the user who last updated the record';
COMMENT ON COLUMN key.m_keys.updated_at IS 'Timestamp of last update';
COMMENT ON COLUMN key.m_keys.row_status IS 'Status of the record (ACTIVE, INACTIVE, DELETED, REVOKED)';
COMMENT ON COLUMN key.m_keys.effective_at IS 'Effective start timestamp';
COMMENT ON COLUMN key.m_keys.expires_at IS 'Expiration timestamp';
COMMENT ON COLUMN key.m_keys.type_id IS 'Reference to key type (key.m_key_types.id)';
COMMENT ON COLUMN key.m_keys.key IS 'The cryptographic key data';

CREATE INDEX IF NOT EXISTS idx_t_keys_type_id ON key.m_keys(type_id);
COMMENT ON INDEX key.idx_t_keys_type_id IS 'Index to optimize queries filtering by key type.';