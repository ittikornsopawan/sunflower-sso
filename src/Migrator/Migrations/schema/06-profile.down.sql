DROP INDEX IF EXISTS profile.idx_m_user_profiles_user_id;
DROP INDEX IF EXISTS profile.idx_m_user_profiles_personal_id;

DROP TABLE IF EXISTS profile.m_user_profiles CASCADE;

DROP SCHEMA IF EXISTS profile CASCADE;