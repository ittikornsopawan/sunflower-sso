CREATE SCHEMA IF NOT EXISTS session;
COMMENT ON SCHEMA session IS 'Schema to store user session related tables';

CREATE TABLE IF NOT EXISTS session.t_sessions
(
    id UUID NOT NULL DEFAULT GEN_RANDOM_UUID() PRIMARY KEY,
    created_by UUID NOT NULL REFERENCES authentication.t_users(id),
    created_at TIMESTAMP WITHOUT TIME ZONE NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_by UUID REFERENCES authentication.t_users(id),
    updated_at TIMESTAMP WITHOUT TIME ZONE,

    row_status VARCHAR(16) NOT NULL DEFAULT 'ACTIVE' CHECK (row_status IN ('ACTIVE', 'INACTIVE', 'DELETED', 'REVOKED')),

    revoked_reason TEXT,

    expires_at TIMESTAMP WITHOUT TIME ZONE,

    user_id UUID NOT NULL REFERENCES authentication.t_users(id),

    code VARCHAR(32) NOT NULL,
    access_token BYTEA NOT NULL,
    access_token_expires_at TIMESTAMP WITHOUT TIME ZONE NOT NULL,
    refresh_access_token BYTEA NOT NULL,
    refresh_access_token_expires_at TIMESTAMP WITHOUT TIME ZONE NOT NULL,
    payload JSONB NOT NULL
);
COMMENT ON TABLE session.t_sessions IS 'Stores session information and tokens for users';
COMMENT ON COLUMN session.t_sessions.id IS 'Primary key of the session record';
COMMENT ON COLUMN session.t_sessions.created_by IS 'User who created the session';
COMMENT ON COLUMN session.t_sessions.created_at IS 'Timestamp when the session was created';
COMMENT ON COLUMN session.t_sessions.updated_by IS 'User who last updated the session';
COMMENT ON COLUMN session.t_sessions.updated_at IS 'Timestamp of last update';
COMMENT ON COLUMN session.t_sessions.row_status IS 'Status of the session (ACTIVE, INACTIVE, DELETED, REVOKED)';
COMMENT ON COLUMN session.t_sessions.revoked_reason IS 'Reason for revoking the session';
COMMENT ON COLUMN session.t_sessions.expires_at IS 'Expiration timestamp of the session';
COMMENT ON COLUMN session.t_sessions.user_id IS 'Reference to the user account';
COMMENT ON COLUMN session.t_sessions.code IS 'Session code';
COMMENT ON COLUMN session.t_sessions.access_token IS 'Access token data';
COMMENT ON COLUMN session.t_sessions.access_token_expires_at IS 'Expiration timestamp of the access token';
COMMENT ON COLUMN session.t_sessions.refresh_access_token IS 'Refresh token data';
COMMENT ON COLUMN session.t_sessions.refresh_access_token_expires_at IS 'Expiration timestamp of the refresh token';
COMMENT ON COLUMN session.t_sessions.payload IS 'Additional session payload data';

CREATE INDEX IF NOT EXISTS idx_t_sessions_user_id ON session.t_sessions(user_id);
COMMENT ON INDEX session.idx_t_sessions_user_id IS 'Index to optimize queries filtering by user ID';

CREATE TABLE IF NOT EXISTS session.t_session_attributes
(
    id UUID NOT NULL DEFAULT GEN_RANDOM_UUID() PRIMARY KEY,
    created_by UUID NOT NULL REFERENCES authentication.t_users(id),
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_by UUID REFERENCES authentication.t_users(id),
    updated_at TIMESTAMP,

    row_status VARCHAR(16) NOT NULL DEFAULT 'ACTIVE' CHECK (row_status IN ('ACTIVE', 'INACTIVE', 'DELETED', 'REVOKED')),

    session_id UUID NOT NULL REFERENCES session.t_sessions(id),
    attribute_id UUID NOT NULL REFERENCES author.m_attributes(id),
    values JSONB NOT NULL
);
COMMENT ON TABLE session.t_session_attributes IS 'Stores attribute values associated with a user session';
COMMENT ON COLUMN session.t_session_attributes.id IS 'Primary key of the session attribute record';
COMMENT ON COLUMN session.t_session_attributes.created_by IS 'User who created the record';
COMMENT ON COLUMN session.t_session_attributes.created_at IS 'Timestamp when the record was created';
COMMENT ON COLUMN session.t_session_attributes.updated_by IS 'User who last updated the record';
COMMENT ON COLUMN session.t_session_attributes.updated_at IS 'Timestamp of last update';
COMMENT ON COLUMN session.t_session_attributes.row_status IS 'Status of the session attribute record: ACTIVE, INACTIVE, DELETED, or REVOKED';
COMMENT ON COLUMN session.t_session_attributes.session_id IS 'Reference to the user session';
COMMENT ON COLUMN session.t_session_attributes.attribute_id IS 'Reference to the attribute definition';
COMMENT ON COLUMN session.t_session_attributes.values IS 'Attribute values associated with the session';

CREATE INDEX IF NOT EXISTS idx_t_session_attributes_session_id ON session.t_session_attributes(session_id);
COMMENT ON INDEX session.idx_t_session_attributes_session_id IS 'Index to optimize queries filtering by session ID';

CREATE TABLE IF NOT EXISTS session.t_session_policies
(
    id UUID NOT NULL DEFAULT GEN_RANDOM_UUID() PRIMARY KEY,
    created_by UUID NOT NULL REFERENCES authentication.t_users(id),
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_by UUID REFERENCES authentication.t_users(id),
    updated_at TIMESTAMP,

    row_status VARCHAR(16) NOT NULL DEFAULT 'ACTIVE' CHECK (row_status IN ('ACTIVE', 'INACTIVE', 'DELETED', 'REVOKED')),

    session_id UUID NOT NULL REFERENCES session.t_sessions(id),
    policy_id UUID NOT NULL REFERENCES author.t_policies(id),
    values JSONB NOT NULL
);
COMMENT ON TABLE session.t_session_policies IS 'Stores policy values associated with a user session';
COMMENT ON COLUMN session.t_session_policies.id IS 'Primary key of the session policy record';
COMMENT ON COLUMN session.t_session_policies.created_by IS 'User who created the record';
COMMENT ON COLUMN session.t_session_policies.created_at IS 'Timestamp when the record was created';
COMMENT ON COLUMN session.t_session_policies.updated_by IS 'User who last updated the record';
COMMENT ON COLUMN session.t_session_policies.updated_at IS 'Timestamp of last update';
COMMENT ON COLUMN session.t_session_policies.row_status IS 'Status of the session policy record: ACTIVE, INACTIVE, DELETED, or REVOKED';
COMMENT ON COLUMN session.t_session_policies.session_id IS 'Reference to the user session';
COMMENT ON COLUMN session.t_session_policies.policy_id IS 'Reference to the policy definition';
COMMENT ON COLUMN session.t_session_policies.values IS 'Policy values associated with the session';

CREATE INDEX IF NOT EXISTS idx_t_session_policies_session_id ON session.t_session_policies(session_id);
COMMENT ON INDEX session.idx_t_session_policies_session_id IS 'Index to optimize queries filtering by session ID';