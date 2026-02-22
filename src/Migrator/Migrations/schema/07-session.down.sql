DROP INDEX IF EXISTS session.idx_t_sessions_user_id;
DROP INDEX IF EXISTS session.idx_t_session_attributes_session_id;
DROP INDEX IF EXISTS session.idx_t_session_policies_session_id;

DROP TABLE IF EXISTS session.t_session_policies CASCADE;
DROP TABLE IF EXISTS session.t_session_attributes CASCADE;
DROP TABLE IF EXISTS session.t_sessions CASCADE;

DROP SCHEMA IF EXISTS session CASCADE;