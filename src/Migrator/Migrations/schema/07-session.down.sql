DROP INDEX IF EXISTS token.idx_t_sessions_user_id;
DROP INDEX IF EXISTS token.idx_t_session_attributes_session_id;
DROP INDEX IF EXISTS token.idx_t_session_policies_session_id;

DROP TABLE IF EXISTS token.t_session_policies CASCADE;
DROP TABLE IF EXISTS token.t_session_attributes CASCADE;
DROP TABLE IF EXISTS token.t_sessions CASCADE;

DROP SCHEMA IF EXISTS sessions CASCADE;