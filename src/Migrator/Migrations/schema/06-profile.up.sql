CREATE SCHEMA IF NOT EXISTS profile;
COMMENT ON SCHEMA profile IS 'Schema to store user profile related tables';

CREATE TABLE IF NOT EXISTS profile.m_user_profiles
(
    id UUID NOT NULL DEFAULT GEN_RANDOM_UUID() PRIMARY KEY,
    created_by UUID NOT NULL REFERENCES authentication.t_users(id),
    created_at TIMESTAMP WITHOUT TIME ZONE NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_by UUID REFERENCES authentication.t_users(id),
    updated_at TIMESTAMP WITHOUT TIME ZONE,

    row_status VARCHAR(16) NOT NULL DEFAULT 'ACTIVE' CHECK (row_status IN ('ACTIVE', 'INACTIVE', 'DELETED', 'REVOKED')),

    user_id UUID NOT NULL REFERENCES authentication.t_users(id),
    personal_id UUID NOT NULL REFERENCES t_personal_info(id)
);
COMMENT ON TABLE profile.m_user_profiles IS 'Stores mapping between a user account and their personal information profile';
COMMENT ON COLUMN profile.m_user_profiles.id IS 'Primary key of the user profile record';
COMMENT ON COLUMN profile.m_user_profiles.created_by IS 'User who created the profile';
COMMENT ON COLUMN profile.m_user_profiles.created_at IS 'Timestamp when the profile was created';
COMMENT ON COLUMN profile.m_user_profiles.updated_by IS 'User who last updated the profile';
COMMENT ON COLUMN profile.m_user_profiles.updated_at IS 'Timestamp of last update';
COMMENT ON COLUMN profile.m_user_profiles.row_status IS 'Status of the profile row: ACTIVE, INACTIVE, or DELETED';
COMMENT ON COLUMN profile.m_user_profiles.user_id IS 'Reference to the user account';
COMMENT ON COLUMN profile.m_user_profiles.personal_id IS 'Reference to the personal information record';

CREATE INDEX IF NOT EXISTS idx_m_user_profiles_user_id ON profile.m_user_profiles(user_id);
CREATE INDEX IF NOT EXISTS idx_m_user_profiles_personal_id ON profile.m_user_profiles(personal_id);
COMMENT ON INDEX profile.idx_m_user_profiles_user_id IS 'Index to optimize queries filtering by user ID';
COMMENT ON INDEX profile.idx_m_user_profiles_personal_id IS 'Index to optimize queries filtering by personal ID';