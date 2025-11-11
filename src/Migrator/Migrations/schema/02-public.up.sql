CREATE EXTENSION IF NOT EXISTS postgis;
CREATE SCHEMA IF NOT EXISTS public;
CREATE TABLE IF NOT EXISTS m_parameters (
    id UUID NOT NULL DEFAULT GEN_RANDOM_UUID() PRIMARY KEY,
    created_by UUID,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_by UUID,
    updated_at TIMESTAMP,
    is_active BOOLEAN NOT NULL DEFAULT FALSE,
    inactive_at TIMESTAMP,
    inactive_by UUID,
    is_deleted BOOLEAN NOT NULL DEFAULT FALSE,
    deleted_at TIMESTAMP,
    deleted_by UUID,
    effective_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    expires_at TIMESTAMP CHECK (
        expires_at IS NULL
        OR expires_at > effective_at
    ),
    category VARCHAR(128),
    key VARCHAR(128) NOT NULL,
    language VARCHAR(16),
    value TEXT
);
CREATE INDEX IF NOT EXISTS idx_m_parameters_key ON m_parameters(key);
COMMENT ON TABLE m_parameters IS 'Stores system parameters, configuration settings, or key-value pairs for various modules';
COMMENT ON COLUMN m_parameters.id IS 'Primary key of the table, UUID';
COMMENT ON COLUMN m_parameters.created_by IS 'User who created the record';
COMMENT ON COLUMN m_parameters.created_at IS 'Timestamp when the record was created';
COMMENT ON COLUMN m_parameters.updated_by IS 'User who last updated the record';
COMMENT ON COLUMN m_parameters.updated_at IS 'Timestamp of last update';
COMMENT ON COLUMN m_parameters.is_active IS 'Indicates if the parameter is active';
COMMENT ON COLUMN m_parameters.inactive_at IS 'Timestamp when parameter became inactive';
COMMENT ON COLUMN m_parameters.inactive_by IS 'User who set inactive';
COMMENT ON COLUMN m_parameters.is_deleted IS 'Indicates if the parameter is deleted';
COMMENT ON COLUMN m_parameters.deleted_at IS 'Timestamp when parameter was deleted';
COMMENT ON COLUMN m_parameters.deleted_by IS 'User who deleted the record';
COMMENT ON COLUMN m_parameters.effective_at IS 'Effective start timestamp';
COMMENT ON COLUMN m_parameters.expires_at IS 'Expiration timestamp';
COMMENT ON COLUMN m_parameters.category IS 'Category of the parameter';
COMMENT ON COLUMN m_parameters.key IS 'Parameter key';
COMMENT ON COLUMN m_parameters.language IS 'Language of the parameter value';
COMMENT ON COLUMN m_parameters.value IS 'Parameter value';
CREATE TABLE IF NOT EXISTS m_error_handlers (
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
    status_code VARCHAR(8) NOT NULL,
    code VARCHAR(8) NOT NULL,
    message VARCHAR(128) NOT NULL,
    language VARCHAR(16)
);
COMMENT ON TABLE m_error_handlers IS 'Stores system parameters, configuration settings, or key-value pairs for various modules';
COMMENT ON COLUMN m_error_handlers.id IS 'Primary key of the table, UUID';
COMMENT ON COLUMN m_error_handlers.created_by IS 'User who created the record';
COMMENT ON COLUMN m_error_handlers.created_at IS 'Timestamp when the record was created';
COMMENT ON COLUMN m_error_handlers.updated_by IS 'User who last updated the record';
COMMENT ON COLUMN m_error_handlers.updated_at IS 'Timestamp of last update';
COMMENT ON COLUMN m_error_handlers.is_active IS 'Indicates if the parameter is active';
COMMENT ON COLUMN m_error_handlers.inactive_at IS 'Timestamp when parameter became inactive';
COMMENT ON COLUMN m_error_handlers.inactive_by IS 'User who set inactive';
COMMENT ON COLUMN m_error_handlers.is_deleted IS 'Indicates if the parameter is deleted';
COMMENT ON COLUMN m_error_handlers.deleted_at IS 'Timestamp when parameter was deleted';
COMMENT ON COLUMN m_error_handlers.deleted_by IS 'User who deleted the record';
COMMENT ON COLUMN m_error_handlers.status_code IS 'Http Status Code';
COMMENT ON COLUMN m_error_handlers.code IS 'Business Error Code';
COMMENT ON COLUMN m_error_handlers.message IS 'Business Error Message';
COMMENT ON COLUMN m_error_handlers.language IS 'Language of the parameter value';
CREATE INDEX IF NOT EXISTS idx_m_error_handlers_code_language ON m_error_handlers(code, language);
COMMENT ON INDEX idx_m_error_handlers_code_language IS 'Index to optimize queries filtering by error code and language.';
CREATE TABLE IF NOT EXISTS t_addresses (
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
    effective_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    expires_at TIMESTAMP CHECK (
        expires_at IS NULL
        OR expires_at > effective_at
    ),
    type VARCHAR(16) NOT NULL,
    address BYTEA NOT NULL,
    address_additional BYTEA,
    country_code VARCHAR(32) NOT NULL,
    country_name VARCHAR(128),
    state VARCHAR(128),
    city VARCHAR(128),
    district VARCHAR(128),
    sub_district VARCHAR(128),
    postal_code VARCHAR(32),
    geofence_area GEOGRAPHY(POLYGON, 4326),
    geofence_center GEOGRAPHY(POINT, 4326),
    geofence_radius_meters INT
);
COMMENT ON TABLE t_addresses IS 'Stores addresses, administrative information, and optional geofence data';
COMMENT ON COLUMN t_addresses.id IS 'Primary key of the table';
COMMENT ON COLUMN t_addresses.created_by IS 'User who created the address';
COMMENT ON COLUMN t_addresses.created_at IS 'Timestamp when the address was created';
COMMENT ON COLUMN t_addresses.updated_by IS 'User who last updated the address';
COMMENT ON COLUMN t_addresses.updated_at IS 'Timestamp of last update';
COMMENT ON COLUMN t_addresses.is_active IS 'Indicates if the address is active';
COMMENT ON COLUMN t_addresses.inactive_at IS 'Timestamp when address became inactive';
COMMENT ON COLUMN t_addresses.inactive_by IS 'User who set inactive';
COMMENT ON COLUMN t_addresses.is_deleted IS 'Indicates if the address is deleted';
COMMENT ON COLUMN t_addresses.deleted_at IS 'Timestamp when the address was deleted';
COMMENT ON COLUMN t_addresses.deleted_by IS 'User who deleted the record';
COMMENT ON COLUMN t_addresses.effective_at IS 'Effective start timestamp';
COMMENT ON COLUMN t_addresses.expires_at IS 'Expiration timestamp';
COMMENT ON COLUMN t_addresses.type IS 'Type of address';
COMMENT ON COLUMN t_addresses.address IS 'Primary address data (binary)';
COMMENT ON COLUMN t_addresses.address_additional IS 'Additional address details (binary)';
COMMENT ON COLUMN t_addresses.country_code IS 'ISO country code';
COMMENT ON COLUMN t_addresses.country_name IS 'Country name';
COMMENT ON COLUMN t_addresses.state IS 'State or province';
COMMENT ON COLUMN t_addresses.city IS 'City';
COMMENT ON COLUMN t_addresses.district IS 'District';
COMMENT ON COLUMN t_addresses.sub_district IS 'Sub-district';
COMMENT ON COLUMN t_addresses.postal_code IS 'Postal code';
COMMENT ON COLUMN t_addresses.geofence_area IS 'Geofence polygon (optional)';
COMMENT ON COLUMN t_addresses.geofence_center IS 'Geofence center point';
COMMENT ON COLUMN t_addresses.geofence_radius_meters IS 'Radius in meters around geofence center';
CREATE INDEX IF NOT EXISTS idx_t_addresses_country_code_state_city ON t_addresses(country_code, state, city);
CREATE INDEX IF NOT EXISTS idx_t_addresses_geofence_area ON t_addresses USING GIST(geofence_area);
COMMENT ON INDEX idx_t_addresses_country_code_state_city IS 'Index to optimize queries filtering by country code, state, and city.';
COMMENT ON INDEX idx_t_addresses_geofence_area IS 'GIST index to optimize spatial queries on geofence areas.';
CREATE TABLE IF NOT EXISTS t_contacts (
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
    channel VARCHAR(16) NOT NULL CHECK (
        channel in ('MOBILE', 'EMAIL', 'FAX', 'SOCIAL_MEDIA')
    ),
    contact VARCHAR(128) NOT NULL,
    contact_name VARCHAR(512) NOT NULL,
    available JSONB,
    remark TEXT
);
COMMENT ON TABLE t_contacts IS 'Stores contact information (mobile, email, fax, social media) and availability info';
COMMENT ON COLUMN t_contacts.id IS 'Primary key of the table';
COMMENT ON COLUMN t_contacts.created_by IS 'User who created the contact';
COMMENT ON COLUMN t_contacts.created_at IS 'Timestamp when contact was created';
COMMENT ON COLUMN t_contacts.updated_by IS 'User who last updated the contact';
COMMENT ON COLUMN t_contacts.updated_at IS 'Timestamp of last update';
COMMENT ON COLUMN t_contacts.is_active IS 'Indicates if the contact is active';
COMMENT ON COLUMN t_contacts.inactive_at IS 'Timestamp when contact became inactive';
COMMENT ON COLUMN t_contacts.inactive_by IS 'User who set inactive';
COMMENT ON COLUMN t_contacts.is_deleted IS 'Indicates if the contact is deleted';
COMMENT ON COLUMN t_contacts.deleted_at IS 'Timestamp when contact was deleted';
COMMENT ON COLUMN t_contacts.deleted_by IS 'User who deleted the record';
COMMENT ON COLUMN t_contacts.channel IS 'Contact channel: MOBILE, EMAIL, FAX, SOCIAL_MEDIA';
COMMENT ON COLUMN t_contacts.contact IS 'Contact value';
COMMENT ON COLUMN t_contacts.contact_name IS 'Name associated with contact';
COMMENT ON COLUMN t_contacts.available IS 'JSONB storing availability info';
COMMENT ON COLUMN t_contacts.remark IS 'Additional notes';
CREATE INDEX IF NOT EXISTS idx_t_contacts_channel_contact ON t_contacts(channel, contact);
COMMENT ON INDEX idx_t_contacts_channel_contact IS 'Index to optimize queries filtering by contact channel and contact value.';
CREATE TABLE IF NOT EXISTS t_files (
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
    effective_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    expires_at TIMESTAMP CHECK (
        expires_at IS NULL
        OR expires_at > effective_at
    ),
    usage_type VARCHAR(32) CHECK (usage_type IN ('DOCUMENT', 'IMAGE', 'VIDEO')),
    file_path VARCHAR(512),
    file_name VARCHAR(128),
    file_size BIGINT,
    file_size_unit VARCHAR(2) DEFAULT 'B' CHECK (file_size_unit in ('B', 'KB', 'MB', 'GB')),
    file_dimension VARCHAR(16),
    file_extension VARCHAR(16),
    mime_type VARCHAR(128),
    description TEXT,
    storage_provider VARCHAR(64),
    storage_bucket VARCHAR(256),
    storage_key TEXT
);
COMMENT ON TABLE t_files IS 'Stores file metadata such as documents, images, videos, and related storage information';
COMMENT ON COLUMN t_files.id IS 'Primary key of the table';
COMMENT ON COLUMN t_files.created_by IS 'User who uploaded the file';
COMMENT ON COLUMN t_files.created_at IS 'Timestamp when the file was created';
COMMENT ON COLUMN t_files.updated_by IS 'User who last updated the file';
COMMENT ON COLUMN t_files.updated_at IS 'Timestamp of last update';
COMMENT ON COLUMN t_files.is_active IS 'Indicates if the file is active';
COMMENT ON COLUMN t_files.inactive_at IS 'Timestamp when the file became inactive';
COMMENT ON COLUMN t_files.inactive_by IS 'User who set inactive';
COMMENT ON COLUMN t_files.is_deleted IS 'Indicates if the file is deleted';
COMMENT ON COLUMN t_files.deleted_at IS 'Timestamp when the file was deleted';
COMMENT ON COLUMN t_files.deleted_by IS 'User who deleted the file';
COMMENT ON COLUMN t_files.effective_at IS 'Effective start timestamp';
COMMENT ON COLUMN t_files.expires_at IS 'Expiration timestamp';
COMMENT ON COLUMN t_files.usage_type IS 'File usage type: DOCUMENT, IMAGE, VIDEO';
COMMENT ON COLUMN t_files.file_path IS 'Path of the file';
COMMENT ON COLUMN t_files.file_name IS 'File name';
COMMENT ON COLUMN t_files.file_size IS 'File size';
COMMENT ON COLUMN t_files.file_size_unit IS 'File size unit: B, KB, MB, GB';
COMMENT ON COLUMN t_files.file_dimension IS 'File dimension, e.g., width x height for images';
COMMENT ON COLUMN t_files.file_extension IS 'File extension';
COMMENT ON COLUMN t_files.mime_type IS 'File MIME type';
COMMENT ON COLUMN t_files.description IS 'Additional description';
COMMENT ON COLUMN t_files.storage_provider IS 'Storage provider name';
COMMENT ON COLUMN t_files.storage_bucket IS 'Storage bucket name';
COMMENT ON COLUMN t_files.storage_key IS 'Storage object key';
CREATE INDEX IF NOT EXISTS idx_t_files_file_name_file_extension ON t_files(file_name, file_extension);
COMMENT ON INDEX idx_t_files_file_name_file_extension IS 'Index to optimize queries filtering by file name and extension.';
CREATE TABLE IF NOT EXISTS t_personal_info (
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
    sid BYTEA,
    prefix_name BYTEA,
    first_name BYTEA NOT NULL,
    middle_name BYTEA,
    last_name BYTEA NOT NULL,
    nick_name BYTEA,
    gender BYTEA,
    date_of_birth BYTEA
);
COMMENT ON TABLE t_personal_info IS 'Stores encrypted personal information of users or employees.';
COMMENT ON COLUMN t_personal_info.id IS 'Primary key unique identifier for each personal information record.';
COMMENT ON COLUMN t_personal_info.created_by IS 'Reference to the user who created the record.';
COMMENT ON COLUMN t_personal_info.created_at IS 'Timestamp when the record was created.';
COMMENT ON COLUMN t_personal_info.updated_by IS 'Reference to the user who last updated the record.';
COMMENT ON COLUMN t_personal_info.updated_at IS 'Timestamp when the record was last updated.';
COMMENT ON COLUMN t_personal_info.is_active IS 'Indicates whether the record is currently active.';
COMMENT ON COLUMN t_personal_info.inactive_at IS 'Timestamp when the record was marked as inactive.';
COMMENT ON COLUMN t_personal_info.inactive_by IS 'Reference to the user who marked the record inactive.';
COMMENT ON COLUMN t_personal_info.is_deleted IS 'Indicates whether the record has been soft-deleted.';
COMMENT ON COLUMN t_personal_info.deleted_at IS 'Timestamp when the record was marked as deleted.';
COMMENT ON COLUMN t_personal_info.deleted_by IS 'Reference to the user who deleted the record.';
COMMENT ON COLUMN t_personal_info.prefix_name IS 'Encrypted prefix or title of the person (e.g., Mr., Ms., Dr.).';
COMMENT ON COLUMN t_personal_info.first_name IS 'Encrypted first name of the person.';
COMMENT ON COLUMN t_personal_info.middle_name IS 'Encrypted middle name of the person, if applicable.';
COMMENT ON COLUMN t_personal_info.last_name IS 'Encrypted last name or family name of the person.';
COMMENT ON COLUMN t_personal_info.nick_name IS 'Encrypted nickname of the person, if any.';
COMMENT ON COLUMN t_personal_info.gender IS 'Encrypted gender information (e.g., Male, Female, Other).';
COMMENT ON COLUMN t_personal_info.date_of_birth IS 'Encrypted date of birth of the person.';
CREATE UNIQUE INDEX IF NOT EXISTS uq_t_personal_info_first_name_middle_name_last_name ON t_personal_info(first_name, middle_name, last_name);
COMMENT ON INDEX uq_t_personal_info_first_name_middle_name_last_name IS 'Ensures uniqueness of the combination of first name, middle name, and last name in encrypted form.';
CREATE TABLE IF NOT EXISTS t_personal_contacts (
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
    personal_id UUID NOT NULL REFERENCES t_personal_info(id),
    contact_id UUID NOT NULL REFERENCES t_contacts(id)
);
COMMENT ON TABLE t_personal_contacts IS 'Links personal information records with contact details.';
COMMENT ON COLUMN t_personal_contacts.id IS 'Primary key unique identifier for each personal contact record.';
COMMENT ON COLUMN t_personal_contacts.created_by IS 'Reference to the user who created the record.';
COMMENT ON COLUMN t_personal_contacts.created_at IS 'Timestamp when the record was created.';
COMMENT ON COLUMN t_personal_contacts.updated_by IS 'Reference to the user who last updated the record.';
COMMENT ON COLUMN t_personal_contacts.updated_at IS 'Timestamp when the record was last updated.';
COMMENT ON COLUMN t_personal_contacts.is_active IS 'Indicates whether the contact record is currently active.';
COMMENT ON COLUMN t_personal_contacts.inactive_at IS 'Timestamp when the contact record was marked as inactive.';
COMMENT ON COLUMN t_personal_contacts.inactive_by IS 'Reference to the user who marked the record as inactive.';
COMMENT ON COLUMN t_personal_contacts.is_deleted IS 'Indicates whether the contact record has been soft-deleted.';
COMMENT ON COLUMN t_personal_contacts.deleted_at IS 'Timestamp when the record was marked as deleted.';
COMMENT ON COLUMN t_personal_contacts.deleted_by IS 'Reference to the user who deleted the record.';
COMMENT ON COLUMN t_personal_contacts.personal_id IS 'Reference to the personal information record (t_personal_info.id).';
COMMENT ON COLUMN t_personal_contacts.contact_id IS 'Reference to the contact detail record (t_contacts.id).';
CREATE TABLE IF NOT EXISTS t_personal_addresses (
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
    personal_id UUID NOT NULL REFERENCES t_personal_info(id),
    address_id UUID NOT NULL REFERENCES t_addresses(id)
);
COMMENT ON TABLE t_personal_addresses IS 'Links a person record to one or multiple address records.';
COMMENT ON COLUMN t_personal_addresses.id IS 'Primary key unique identifier for each personal-address link record.';
COMMENT ON COLUMN t_personal_addresses.created_by IS 'Reference to the user who created the record.';
COMMENT ON COLUMN t_personal_addresses.created_at IS 'Timestamp when the record was created.';
COMMENT ON COLUMN t_personal_addresses.updated_by IS 'Reference to the user who last updated the record.';
COMMENT ON COLUMN t_personal_addresses.updated_at IS 'Timestamp when the record was last updated.';
COMMENT ON COLUMN t_personal_addresses.is_active IS 'Indicates whether the address link is currently active.';
COMMENT ON COLUMN t_personal_addresses.inactive_at IS 'Timestamp when the address link was marked as inactive.';
COMMENT ON COLUMN t_personal_addresses.inactive_by IS 'Reference to the user who marked the address link as inactive.';
COMMENT ON COLUMN t_personal_addresses.is_deleted IS 'Indicates whether the record has been soft-deleted.';
COMMENT ON COLUMN t_personal_addresses.deleted_at IS 'Timestamp when the record was marked as deleted.';
COMMENT ON COLUMN t_personal_addresses.deleted_by IS 'Reference to the user who deleted the record.';
COMMENT ON COLUMN t_personal_addresses.personal_id IS 'Reference to the person record (t_personal_info.id).';
COMMENT ON COLUMN t_personal_addresses.address_id IS 'Reference to the address record (t_addresses.id).';