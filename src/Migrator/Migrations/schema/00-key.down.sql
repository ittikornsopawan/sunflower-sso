DROP INDEX IF EXISTS key.idx_m_key_types_name;
DROP INDEX IF EXISTS key.idx_t_keys_type_id;
DROP INDEX IF EXISTS key.idx_m_algorithms_name;

DROP TABLE IF EXISTS key.m_keys CASCADE;
DROP TABLE IF EXISTS key.m_key_types CASCADE;

DROP TABLE IF EXISTS key.m_algorithms CASCADE;

DROP SCHEMA IF EXISTS key CASCADE;