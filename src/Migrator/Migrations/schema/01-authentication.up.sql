CREATE EXTENSION IF NOT EXISTS pgcrypto;

CREATE SCHEMA IF NOT EXISTS authentication;
COMMENT ON SCHEMA authentication IS 'Schema to manage system users and authentication types';

CREATE TABLE IF NOT EXISTS authentication.t_users
(
    id UUID NOT NULL DEFAULT GEN_RANDOM_UUID() PRIMARY KEY,
    created_by UUID,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_by UUID,
    updated_at TIMESTAMP,

    is_active BOOLEAN NOT NULL DEFAULT FALSE,
    inactive_at TIMESTAMP,
    inactive_by UUID REFERENCES authentication.t_users(id),

    is_deleted BOOLEAN NOT NULL DEFAULT FALSE,
    deleted_at TIMESTAMP,
    deleted_by UUID REFERENCES authentication.t_users(id),

    code VARCHAR(32) NOT NULL,
    username VARCHAR(128) NOT NULL,
    authentication_type VARCHAR(16) NOT NULL CHECK (authentication_type IN ('PASSWORD', 'OAUTH', 'EMAIL_OTP', 'MOBILE_OTP'))
);

COMMENT ON TABLE authentication.t_users IS 'Stores system users, their authentication type, and status.';
COMMENT ON COLUMN authentication.t_users.id IS 'Primary key for the user record.';
COMMENT ON COLUMN authentication.t_users.created_by IS 'User who created this user record (references authentication.t_users.id).';
COMMENT ON COLUMN authentication.t_users.created_at IS 'Timestamp when the user record was created.';
COMMENT ON COLUMN authentication.t_users.updated_by IS 'User who last updated this record (references authentication.t_users.id).';
COMMENT ON COLUMN authentication.t_users.updated_at IS 'Timestamp when the user record was last updated.';
COMMENT ON COLUMN authentication.t_users.is_active IS 'Indicates if the user account is active.';
COMMENT ON COLUMN authentication.t_users.inactive_at IS 'Timestamp when the user was marked inactive.';
COMMENT ON COLUMN authentication.t_users.inactive_by IS 'User who deactivated this account (references authentication.t_users.id).';
COMMENT ON COLUMN authentication.t_users.is_deleted IS 'Indicates if the user account has been soft-deleted.';
COMMENT ON COLUMN authentication.t_users.deleted_at IS 'Timestamp when the user was deleted.';
COMMENT ON COLUMN authentication.t_users.deleted_by IS 'User who deleted this record (references authentication.t_users.id).';
COMMENT ON COLUMN authentication.t_users.code IS 'Unique code assigned to the user.';
COMMENT ON COLUMN authentication.t_users.username IS 'Unique username for login.';
COMMENT ON COLUMN authentication.t_users.authentication_type IS 'Type of authentication: PASSWORD, OAUTH, EMAIL_OTP, MOBILE_OTP.';

CREATE UNIQUE INDEX IF NOT EXISTS uq_t_users_username ON authentication.t_users(username);
COMMENT ON INDEX authentication.uq_t_users_username IS 'Ensures that usernames are unique across all users.';

CREATE TABLE IF NOT EXISTS authentication.t_user_authentications
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

    effective_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    expires_at TIMESTAMP CHECK (expires_at IS NOT NULL AND expires_at > effective_at),

    user_id UUID NOT NULL REFERENCES authentication.t_users(id),
    is_temporary BOOLEAN NOT NULL DEFAULT FALSE,

    algorithm_id UUID NOT NULL REFERENCES key.m_algorithms(id),
    algorithm_keys JSONB NOT NULL,

    password_hash BYTEA NOT NULL
);
COMMENT ON TABLE authentication.t_user_authentications IS 'Stores authentication credentials for users, including password hash, algorithm info, and validity period';
COMMENT ON COLUMN authentication.t_user_authentications.id IS 'Primary key of the authentication record';
COMMENT ON COLUMN authentication.t_user_authentications.created_by IS 'User who created the record';
COMMENT ON COLUMN authentication.t_user_authentications.created_at IS 'Timestamp when the record was created';
COMMENT ON COLUMN authentication.t_user_authentications.updated_by IS 'User who last updated the record';
COMMENT ON COLUMN authentication.t_user_authentications.updated_at IS 'Timestamp when the record was last updated';
COMMENT ON COLUMN authentication.t_user_authentications.is_active IS 'Indicates if the record is active';
COMMENT ON COLUMN authentication.t_user_authentications.inactive_at IS 'Timestamp when record became inactive';
COMMENT ON COLUMN authentication.t_user_authentications.inactive_by IS 'User who marked inactive';
COMMENT ON COLUMN authentication.t_user_authentications.is_deleted IS 'Indicates if the record has been deleted';
COMMENT ON COLUMN authentication.t_user_authentications.effective_at IS 'Effective start timestamp';
COMMENT ON COLUMN authentication.t_user_authentications.expires_at IS 'Expiration timestamp (must be after effective_at)';
COMMENT ON COLUMN authentication.t_user_authentications.user_id IS 'Reference to the user';
COMMENT ON COLUMN authentication.t_user_authentications.is_temporary IS 'Indicates if this authentication is temporary';
COMMENT ON COLUMN authentication.t_user_authentications.algorithm_id IS 'Reference to the algorithm used';
COMMENT ON COLUMN authentication.t_user_authentications.algorithm_keys IS 'Keys or parameters required by the algorithm';
COMMENT ON COLUMN authentication.t_user_authentications.password_hash IS 'Hashed password';

CREATE INDEX IF NOT EXISTS idx_t_user_authentications_user_id ON authentication.t_user_authentications(user_id);
COMMENT ON INDEX authentication.idx_t_user_authentications_user_id IS 'Index to quickly find authentications by user ID.';

CREATE TABLE IF NOT EXISTS authentication.t_user_referrer_mappings
(
    id UUID NOT NULL DEFAULT GEN_RANDOM_UUID() PRIMARY KEY,
    created_by UUID NOT NULL REFERENCES authentication.t_users(id),
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,

    user_id UUID NOT NULL REFERENCES authentication.t_users(id),
    referrer_id UUID NOT NULL REFERENCES authentication.t_users(id)
);
COMMENT ON TABLE authentication.t_user_referrer_mappings IS 'Stores mappings between users and their referrers';
COMMENT ON COLUMN authentication.t_user_referrer_mappings.id IS 'Primary key of the mapping record';
COMMENT ON COLUMN authentication.t_user_referrer_mappings.created_by IS 'User who created the record';
COMMENT ON COLUMN authentication.t_user_referrer_mappings.created_at IS 'Timestamp when the record was created';
COMMENT ON COLUMN authentication.t_user_referrer_mappings.referrer_id IS 'Reference to the referrer user';
COMMENT ON COLUMN authentication.t_user_referrer_mappings.user_id IS 'Reference to the user being referred';

CREATE INDEX IF NOT EXISTS idx_t_user_referrer_mappings_user_id ON authentication.t_user_referrer_mappings(user_id);
CREATE UNIQUE INDEX IF NOT EXISTS uq_t_user_referrer_mappings_user_id_referral_id ON authentication.t_user_referrer_mappings(user_id, referrer_id);
COMMENT ON INDEX authentication.idx_t_user_referrer_mappings_user_id IS 'Index to quickly find referrers by user ID.';
COMMENT ON INDEX authentication.uq_t_user_referrer_mappings_user_id_referral_id IS 'Ensures unique user-referrer pairs.';

CREATE TABLE IF NOT EXISTS authentication.t_user_open_authentication
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

    provider VARCHAR(32) NOT NULL CHECK (provider IN ('GOOGLE', 'MICROSOFT', 'APPLE', 'FACEBOOK', 'LINE', 'GITHUB', 'OTHER')),
    provider_name VARCHAR(64) CHECK (provider <> 'OTHER' or provider_name IS NOT NULL),
    provider_user_id VARCHAR(256) NOT NULL,
    user_id UUID NOT NULL REFERENCES authentication.t_users(id),
    
    email BYTEA,
    display_name VARCHAR(256),
    profile_picture_url TEXT,

    access_token TEXT,
    refresh_token TEXT,
    token_expires_at TIMESTAMP,

    raw_response JSONB
);
COMMENT ON TABLE authentication.t_user_open_authentication IS 'Stores social login / OAuth accounts linked to system users.';
COMMENT ON COLUMN authentication.t_user_open_authentication.id IS 'Primary key for the open authentication record.';
COMMENT ON COLUMN authentication.t_user_open_authentication.created_by IS 'User who created this record (references authentication.t_users.id).';
COMMENT ON COLUMN authentication.t_user_open_authentication.created_at IS 'Timestamp when the record was created.';
COMMENT ON COLUMN authentication.t_user_open_authentication.updated_by IS 'User who last updated the record (references authentication.t_users.id).';
COMMENT ON COLUMN authentication.t_user_open_authentication.updated_at IS 'Timestamp when the record was last updated.';
COMMENT ON COLUMN authentication.t_user_open_authentication.is_active IS 'Indicates whether the social login account is active.';
COMMENT ON COLUMN authentication.t_user_open_authentication.inactive_at IS 'Timestamp when the social login account was deactivated.';
COMMENT ON COLUMN authentication.t_user_open_authentication.inactive_by IS 'User who deactivated the account (references authentication.t_users.id).';
COMMENT ON COLUMN authentication.t_user_open_authentication.is_deleted IS 'Indicates whether the record has been soft-deleted.';
COMMENT ON COLUMN authentication.t_user_open_authentication.deleted_at IS 'Timestamp when the record was deleted.';
COMMENT ON COLUMN authentication.t_user_open_authentication.deleted_by IS 'User who deleted the record (references authentication.t_users.id).';
COMMENT ON COLUMN authentication.t_user_open_authentication.provider IS 'Name of the external provider: GOOGLE, MICROSOFT, APPLE, FACEBOOK, LINE, GITHUB, OTHER.';
COMMENT ON COLUMN authentication.t_user_open_authentication.provider_name IS 'Custom name for the provider when provider = OTHER.';
COMMENT ON COLUMN authentication.t_user_open_authentication.provider_user_id IS 'User identifier provided by the external provider.';
COMMENT ON COLUMN authentication.t_user_open_authentication.user_id IS 'Reference to the system user (authentication.t_users.id) linked to this social account.';
COMMENT ON COLUMN authentication.t_user_open_authentication.email IS 'Encrypted email retrieved from the external provider (optional, BYTEA).';
COMMENT ON COLUMN authentication.t_user_open_authentication.display_name IS 'Display name from the provider (optional).';
COMMENT ON COLUMN authentication.t_user_open_authentication.profile_picture_url IS 'URL to profile picture from the provider (optional).';
COMMENT ON COLUMN authentication.t_user_open_authentication.access_token IS 'OAuth access token from the provider.';
COMMENT ON COLUMN authentication.t_user_open_authentication.refresh_token IS 'OAuth refresh token from the provider (if applicable).';
COMMENT ON COLUMN authentication.t_user_open_authentication.token_expires_at IS 'Expiration timestamp for the access token.';
COMMENT ON COLUMN authentication.t_user_open_authentication.raw_response IS 'Raw JSON response received from the provider during authentication.';

CREATE UNIQUE INDEX IF NOT EXISTS idx_t_user_open_authentication_provider_provider_user_id ON authentication.t_user_open_authentication(provider, provider_user_id);
COMMENT ON INDEX authentication.idx_t_user_open_authentication_provider_provider_user_id IS 'Ensures unique combination of provider and provider_user_id across all records.';