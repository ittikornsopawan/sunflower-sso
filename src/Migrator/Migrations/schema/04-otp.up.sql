CREATE SCHEMA IF NOT EXISTS otp;
COMMENT ON SCHEMA otp IS 'Schema for managing one-time passwords (OTPs) for multi-factor authentication and verification processes.';

CREATE TABLE IF NOT EXISTS otp.t_otp
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

    expires_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP + INTERVAL '15 minutes',

    ref_code VARCHAR(16) NOT NULL,
    otp VARCHAR(8) NOT NULL,
    verify_count INT NOT NULL DEFAULT 0
);
COMMENT ON TABLE otp.t_otp IS 'Stores OTP records for verification with reference code.';
COMMENT ON COLUMN otp.t_otp.id IS 'Primary key for OTP record.';
COMMENT ON COLUMN otp.t_otp.created_by IS 'User who generated the OTP (references authentication.t_users.id).';
COMMENT ON COLUMN otp.t_otp.created_at IS 'Timestamp when the OTP was created.';
COMMENT ON COLUMN otp.t_otp.updated_by IS 'User who last updated the OTP record (references authentication.t_users.id).';
COMMENT ON COLUMN otp.t_otp.updated_at IS 'Timestamp when the OTP was last updated.';
COMMENT ON COLUMN otp.t_otp.is_active IS 'Indicates if the OTP is currently active.';
COMMENT ON COLUMN otp.t_otp.inactive_at IS 'Timestamp when the OTP became inactive.';
COMMENT ON COLUMN otp.t_otp.inactive_by IS 'User who marked the OTP inactive.';
COMMENT ON COLUMN otp.t_otp.is_deleted IS 'Indicates if the OTP record is deleted.';
COMMENT ON COLUMN otp.t_otp.deleted_at IS 'Timestamp when the OTP record was deleted.';
COMMENT ON COLUMN otp.t_otp.deleted_by IS 'User who deleted the OTP record.';
COMMENT ON COLUMN otp.t_otp.expires_at IS 'Expiration timestamp of the OTP.';
COMMENT ON COLUMN otp.t_otp.ref_code IS 'Reference code for which the OTP was generated.';
COMMENT ON COLUMN otp.t_otp.otp IS 'The one-time password value.';
COMMENT ON COLUMN otp.t_otp.verify_count IS 'Number of verification attempts made for this OTP.';

CREATE INDEX IF NOT EXISTS idx_t_otp_code ON otp.t_otp(ref_code);
COMMENT ON INDEX otp.idx_t_otp_code IS 'Index to optimize queries filtering by reference code.';

CREATE TABLE IF NOT EXISTS otp.t_otp_logs
(
    id UUID NOT NULL DEFAULT GEN_RANDOM_UUID() PRIMARY KEY,
    created_by UUID NOT NULL REFERENCES authentication.t_users(id),
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,

    otp_id UUID NOT NULL REFERENCES otp.t_otp(id),
    count_no INT NOT NULL DEFAULT 1,

    context VARCHAR(32) NOT NULL CHECK (context IN ('LOGIN', 'CONFIRM', 'RESET_PASSWORD', 'OTHER')),
    ip_address INET,
    device_id TEXT,
    device_os TEXT,

    location_name TEXT,
    latitude TEXT,
    longitude TEXT,

    geofence_center GEOGRAPHY(POINT, 4326),
    geofence_radius_meters INT,

    result VARCHAR(16) NOT NULL CHECK (result IN ('SUCCESS','FAILED','EXPIRED')),
    remark TEXT
);
COMMENT ON TABLE otp.t_otp_logs IS 'Logs for OTP verification attempts, including context, device, location, and result.';
COMMENT ON COLUMN otp.t_otp_logs.id IS 'Primary key for the OTP log record.';
COMMENT ON COLUMN otp.t_otp_logs.created_by IS 'User who performed the OTP verification attempt (references authentication.t_users.id).';
COMMENT ON COLUMN otp.t_otp_logs.created_at IS 'Timestamp when the OTP log record was created.';
COMMENT ON COLUMN otp.t_otp_logs.otp_id IS 'Reference to the OTP record (otp.t_otp.id) being verified.';
COMMENT ON COLUMN otp.t_otp_logs.count_no IS 'Sequential count number for multiple attempts on the same OTP.';
COMMENT ON COLUMN otp.t_otp_logs.context IS 'Context for the OTP check: LOGIN, CONFIRM, RESET_PASSWORD, OTHER.';
COMMENT ON COLUMN otp.t_otp_logs.ip_address IS 'IP address from which the OTP verification was attempted.';
COMMENT ON COLUMN otp.t_otp_logs.device_id IS 'Identifier of the device used for OTP verification.';
COMMENT ON COLUMN otp.t_otp_logs.device_os IS 'Operating system of the device used for OTP verification.';
COMMENT ON COLUMN otp.t_otp_logs.location_name IS 'Human-readable name of the location where OTP was verified.';
COMMENT ON COLUMN otp.t_otp_logs.latitude IS 'Latitude of the location where OTP was verified.';
COMMENT ON COLUMN otp.t_otp_logs.longitude IS 'Longitude of the location where OTP was verified.';
COMMENT ON COLUMN otp.t_otp_logs.geofence_center IS 'Geographical point (latitude, longitude) for geofence verification (optional).';
COMMENT ON COLUMN otp.t_otp_logs.geofence_radius_meters IS 'Radius in meters used for geofence verification.';
COMMENT ON COLUMN otp.t_otp_logs.result IS 'Result of the OTP verification: SUCCESS, FAILED, EXPIRED.';
COMMENT ON COLUMN otp.t_otp_logs.remark IS 'Additional remarks or notes regarding the OTP verification attempt.';