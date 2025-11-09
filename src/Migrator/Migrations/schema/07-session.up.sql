CREATE SCHEMA IF NOT EXISTS session;
COMMENT ON SCHEMA session IS 'Schema to store user session related tables';

CREATE TABLE IF NOT EXISTS session.t_sessions
(
    id UUID NOT NULL DEFAULT GEN_RANDOM_UUID() PRIMARY KEY,
    created_by UUID NOT NULL REFERENCES authentication.t_users(id),
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_by UUID REFERENCES authentication.t_users(id),
    updated_at TIMESTAMP,

    is_active BOOLEAN NOT NULL DEFAULT FALSE,
    inactive_at TIMESTAMP,
    inactive_by UUID REFERENCES authentication.t_users(id),

    is_revoked BOOLEAN NOT NULL DEFAULT FALSE,
    revoked_at TIMESTAMP,
    revoked_by UUID REFERENCES authentication.t_users(id),
    revoked_reason TEXT,

    expires_at TIMESTAMP,

    user_id UUID NOT NULL REFERENCES authentication.t_users(id),

    code VARCHAR(32) NOT NULL,
    access_token BYTEA NOT NULL,
    access_token_expires_at TIMESTAMP NOT NULL,
    refresh_access_token BYTEA NOT NULL,
    refresh_access_token_expires_at TIMESTAMP NOT NULL,
    payload JSONB NOT NULL
);
COMMENT ON TABLE session.t_sessions IS 'Stores session information and tokens for users';
COMMENT ON COLUMN session.t_sessions.id IS 'Primary key of the session record';
COMMENT ON COLUMN session.t_sessions.created_by IS 'User who created the session';
COMMENT ON COLUMN session.t_sessions.created_at IS 'Timestamp when the session was created';
COMMENT ON COLUMN session.t_sessions.updated_by IS 'User who last updated the session';
COMMENT ON COLUMN session.t_sessions.updated_at IS 'Timestamp of last update';
COMMENT ON COLUMN session.t_sessions.is_active IS 'Indicates if the session is active';
COMMENT ON COLUMN session.t_sessions.inactive_at IS 'Timestamp when the session became inactive';
COMMENT ON COLUMN session.t_sessions.inactive_by IS 'User who marked the session inactive';
COMMENT ON COLUMN session.t_sessions.is_revoked IS 'Indicates if the session has been revoked';
COMMENT ON COLUMN session.t_sessions.revoked_at IS 'Timestamp when the session was revoked';
COMMENT ON COLUMN session.t_sessions.revoked_by IS 'User who revoked the session';
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

    is_active BOOLEAN NOT NULL DEFAULT FALSE,
    inactive_at TIMESTAMP,
    inactive_by UUID REFERENCES authentication.t_users(id),

    is_deleted BOOLEAN NOT NULL DEFAULT FALSE,
    deleted_at TIMESTAMP,
    deleted_by UUID REFERENCES authentication.t_users(id),

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
COMMENT ON COLUMN session.t_session_attributes.is_active IS 'Indicates if the record is active';
COMMENT ON COLUMN session.t_session_attributes.inactive_at IS 'Timestamp when the record became inactive';
COMMENT ON COLUMN session.t_session_attributes.inactive_by IS 'User who marked the record inactive';
COMMENT ON COLUMN session.t_session_attributes.is_deleted IS 'Indicates if the record is deleted';
COMMENT ON COLUMN session.t_session_attributes.deleted_at IS 'Timestamp when the record was deleted';
COMMENT ON COLUMN session.t_session_attributes.deleted_by IS 'User who deleted the record';
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

    is_active BOOLEAN NOT NULL DEFAULT FALSE,
    inactive_at TIMESTAMP,
    inactive_by UUID REFERENCES authentication.t_users(id),

    is_deleted BOOLEAN NOT NULL DEFAULT FALSE,
    deleted_at TIMESTAMP,
    deleted_by UUID REFERENCES authentication.t_users(id),

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
COMMENT ON COLUMN session.t_session_policies.is_active IS 'Indicates if the record is active';
COMMENT ON COLUMN session.t_session_policies.inactive_at IS 'Timestamp when the record became inactive';
COMMENT ON COLUMN session.t_session_policies.inactive_by IS 'User who marked the record inactive';
COMMENT ON COLUMN session.t_session_policies.is_deleted IS 'Indicates if the record is deleted';
COMMENT ON COLUMN session.t_session_policies.deleted_at IS 'Timestamp when the record was deleted';
COMMENT ON COLUMN session.t_session_policies.deleted_by IS 'User who deleted the record';
COMMENT ON COLUMN session.t_session_policies.session_id IS 'Reference to the user session';
COMMENT ON COLUMN session.t_session_policies.policy_id IS 'Reference to the policy definition';
COMMENT ON COLUMN session.t_session_policies.values IS 'Policy values associated with the session';

CREATE INDEX IF NOT EXISTS idx_t_session_policies_session_id ON session.t_session_policies(session_id);
COMMENT ON INDEX session.idx_t_session_policies_session_id IS 'Index to optimize queries filtering by session ID';