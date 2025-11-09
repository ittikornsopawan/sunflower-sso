DROP INDEX IF EXISTS author.idx_m_attributes_key;
DROP INDEX IF EXISTS author.idx_t_policies_name;
DROP INDEX IF EXISTS author.idx_t_policy_attribute_mappings_policy_id_attribute_id;
DROP INDEX IF EXISTS author.idx_t_policy_decision_logs_user_id_policy_id;
DROP INDEX IF EXISTS author.idx_t_policy_decision_logs_user_id;
DROP INDEX IF EXISTS author.idx_t_policy_decision_logs_policy_id;
DROP INDEX IF EXISTS author.idx_t_user_attribute_mappings_user_id_attribute_id;

DROP TABLE IF EXISTS author.t_policy_attribute_mappings CASCADE;
DROP TABLE IF EXISTS author.t_user_attribute_mappings CASCADE;
DROP TABLE IF EXISTS author.t_policy_decision_logs CASCADE;
DROP TABLE IF EXISTS author.t_policies CASCADE;
DROP TABLE IF EXISTS author.m_attributes CASCADE;

DROP SCHEMA IF EXISTS author CASCADE;