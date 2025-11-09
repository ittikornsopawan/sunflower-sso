DROP INDEX IF EXISTS audit.idx_t_request_logs_endpoint;
DROP INDEX IF EXISTS audit.idx_t_request_logs_created_at;
DROP INDEX IF EXISTS audit.idx_t_data_change_log_headers_table_name;
DROP INDEX IF EXISTS audit.idx_t_data_change_log_headers_created_at;
DROP INDEX IF EXISTS audit.idx_t_data_change_log_items_header_id;

DROP TABLE IF EXISTS audit.t_change_log_items CASCADE;
DROP TABLE IF EXISTS audit.t_change_logs CASCADE;
DROP TABLE IF EXISTS audit.t_request_logs CASCADE;

DROP SCHEMA IF EXISTS audit CASCADE;