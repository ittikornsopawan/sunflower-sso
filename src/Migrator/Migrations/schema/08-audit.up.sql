CREATE SCHEMA IF NOT EXISTS audit;

CREATE TABLE IF NOT EXISTS audit.t_request_logs
(
    id UUID NOT NULL DEFAULT GEN_RANDOM_UUID() PRIMARY KEY,
    created_by UUID REFERENCES authentication.t_users(id),
    created_at TIMESTAMP WITHOUT TIME ZONE NOT NULL DEFAULT CURRENT_TIMESTAMP,
    
    endpoint TEXT NOT NULL,
    method TEXT NOT NULL,
    request_payload JSONB,
    response_payload JSONB,
    status_code INT,
    ip_address TEXT,
    user_agent TEXT,
    duration_ms INT
);
COMMENT ON TABLE audit.t_request_logs IS 'Stores all API request and response activities, used for auditing and tracing.';
COMMENT ON COLUMN audit.t_request_logs.id IS 'Primary key of the API request log record';
COMMENT ON COLUMN audit.t_request_logs.created_by IS 'User who initiated the request';
COMMENT ON COLUMN audit.t_request_logs.created_at IS 'Timestamp when the request occurred';
COMMENT ON COLUMN audit.t_request_logs.endpoint IS 'API endpoint that was accessed';
COMMENT ON COLUMN audit.t_request_logs.method IS 'HTTP method used in the request';
COMMENT ON COLUMN audit.t_request_logs.request_payload IS 'Request data payload';
COMMENT ON COLUMN audit.t_request_logs.response_payload IS 'Response data payload';
COMMENT ON COLUMN audit.t_request_logs.status_code IS 'HTTP status code returned';
COMMENT ON COLUMN audit.t_request_logs.ip_address IS 'IP address of the requester';
COMMENT ON COLUMN audit.t_request_logs.user_agent IS 'User agent or client information';
COMMENT ON COLUMN audit.t_request_logs.duration_ms IS 'Execution time of the request in milliseconds';

CREATE INDEX IF NOT EXISTS idx_t_request_logs_endpoint ON audit.t_request_logs(endpoint);
CREATE INDEX IF NOT EXISTS idx_t_request_logs_created_at ON audit.t_request_logs(created_at);
COMMENT ON INDEX audit.idx_t_request_logs_endpoint IS 'Index to optimize queries filtering by API endpoint';
COMMENT ON INDEX audit.idx_t_request_logs_created_at IS 'Index to optimize queries filtering by creation timestamp';

CREATE TABLE IF NOT EXISTS audit.t_change_logs
(
    id UUID NOT NULL DEFAULT GEN_RANDOM_UUID() PRIMARY KEY,
    created_by UUID REFERENCES authentication.t_users(id),
    created_at TIMESTAMP WITHOUT TIME ZONE NOT NULL DEFAULT CURRENT_TIMESTAMP,

    table_name TEXT NOT NULL,
    record_id UUID NOT NULL,
    operation TEXT NOT NULL,
    request_id UUID REFERENCES audit.t_request_logs(id)
);
COMMENT ON TABLE audit.t_change_logs IS 'Stores header-level information for grouped data change events (transactions).';
COMMENT ON COLUMN audit.t_change_logs.id IS 'Primary key of the change log header record';
COMMENT ON COLUMN audit.t_change_logs.created_by IS 'User who made the change';
COMMENT ON COLUMN audit.t_change_logs.created_at IS 'Timestamp when the change occurred';
COMMENT ON COLUMN audit.t_change_logs.table_name IS 'Name of the table where the change occurred';
COMMENT ON COLUMN audit.t_change_logs.record_id IS 'ID of the affected record';
COMMENT ON COLUMN audit.t_change_logs.operation IS 'Type of operation (INSERT, UPDATE, DELETE)';
COMMENT ON COLUMN audit.t_change_logs.request_id IS 'Reference to related API request record';

CREATE INDEX IF NOT EXISTS idx_t_data_change_log_headers_table_name ON audit.t_change_logs(table_name);
CREATE INDEX IF NOT EXISTS idx_t_data_change_log_headers_created_at ON audit.t_change_logs(created_at);
COMMENT ON INDEX audit.idx_t_data_change_log_headers_table_name IS 'Index to optimize queries filtering by table name';
COMMENT ON INDEX audit.idx_t_data_change_log_headers_created_at IS 'Index to optimize queries filtering by creation timestamp';

CREATE TABLE IF NOT EXISTS audit.t_change_log_items
(
    id UUID NOT NULL DEFAULT GEN_RANDOM_UUID() PRIMARY KEY,
    created_by UUID REFERENCES authentication.t_users(id),
    created_at TIMESTAMP WITHOUT TIME ZONE NOT NULL DEFAULT CURRENT_TIMESTAMP,

    header_id UUID NOT NULL REFERENCES audit.t_change_logs(id),
    column_name TEXT NOT NULL,
    old_value TEXT,
    new_value TEXT
);
COMMENT ON TABLE audit.t_change_log_items IS 'Stores column-level data changes associated with a change log header.';
COMMENT ON COLUMN audit.t_change_log_items.id IS 'Primary key of the change log item record';
COMMENT ON COLUMN audit.t_change_log_items.created_by IS 'User who made the change';
COMMENT ON COLUMN audit.t_change_log_items.created_at IS 'Timestamp when the change occurred';
COMMENT ON COLUMN audit.t_change_log_items.header_id IS 'Reference to change log header record';
COMMENT ON COLUMN audit.t_change_log_items.column_name IS 'Name of the column changed';
COMMENT ON COLUMN audit.t_change_log_items.old_value IS 'Previous value before change';
COMMENT ON COLUMN audit.t_change_log_items.new_value IS 'Updated value after change';

CREATE INDEX IF NOT EXISTS idx_t_data_change_log_items_header_id ON audit.t_change_log_items(header_id);
COMMENT ON INDEX audit.idx_t_data_change_log_items_header_id IS 'Index to optimize queries filtering by change log header ID';