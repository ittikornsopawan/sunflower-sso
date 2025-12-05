-- Drop indexes for t_user_consents
DROP INDEX IF EXISTS consent.uq_t_user_consents_user_consent_id;
DROP INDEX IF EXISTS consent.idx_t_user_consents_user_id;
DROP INDEX IF EXISTS consent.idx_t_user_consents_consent_id;

-- Drop indexes for t_consents
DROP INDEX IF EXISTS consent.uq_t_consents_type_version_language;

-- Drop indexes for m_consent_types
DROP INDEX IF EXISTS consent.uq_m_consent_types_name;

-- Drop tables
DROP TABLE IF EXISTS consent.t_user_consents CASCADE;
DROP TABLE IF EXISTS consent.t_consents CASCADE;
DROP TABLE IF EXISTS consent.m_consent_types CASCADE;

-- Drop schema
DROP SCHEMA IF EXISTS consent CASCADE;