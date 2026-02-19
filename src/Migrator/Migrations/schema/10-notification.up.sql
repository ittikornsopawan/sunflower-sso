CREATE SCHEMA IF NOT EXISTS notification;

CREATE TABLE IF NOT EXISTS notification.t_notifications (
    id UUID NOT NULL DEFAULT GEN_RANDOM_UUID() PRIMARY KEY,
    created_by UUID,
    created_at TIMESTAMP WITHOUT TIME ZONE NOT NULL DEFAULT CURRENT_TIMESTAMP,

    type VARCHAR(8) NOT NULL CHECK (type IN ('EMAIL', 'SMS', 'PUSH')),
    contact BYTEA NOT NULL,
    message BYTEA NOT NULL,
    status VARCHAR(16) NOT NULL DEFAULT 'PENDING' CHECK (status IN ('PENDING', 'SENT', 'FAILED')),
    retry_count INT NOT NULL DEFAULT 0
);
COMMENT ON TABLE notification.t_notifications IS 'Table to store notification records for Otp and other alerts';
COMMENT ON COLUMN notification.t_notifications.created_by IS 'User who created the notification record';
COMMENT ON COLUMN notification.t_notifications.created_at IS 'Timestamp when the notification record was created';
COMMENT ON COLUMN notification.t_notifications.type IS 'Type of notification: EMAIL, SMS, PUSH';
COMMENT ON COLUMN notification.t_notifications.contact IS 'Contact information (email address, phone number, etc.)';
COMMENT ON COLUMN notification.t_notifications.message IS 'Content of the notification message';
COMMENT ON COLUMN notification.t_notifications.status IS 'Status of the notification: PENDING, SENT, FAILED';
COMMENT ON COLUMN notification.t_notifications.retry_count IS 'Number of retry attempts for sending the notification';

CREATE INDEX idx_notifications_type ON notification.t_notifications(type);
COMMENT ON INDEX notification.idx_notifications_type IS 'Index to optimize queries filtering by notification type';

CREATE INDEX idx_notifications_status ON notification.t_notifications(status);
COMMENT ON INDEX notification.idx_notifications_status IS 'Index to optimize queries filtering by notification status';

CREATE TABLE IF NOT EXISTS notification.t_notification_templates (
    id UUID NOT NULL DEFAULT GEN_RANDOM_UUID() PRIMARY KEY,
    created_by UUID,
    created_at TIMESTAMP WITHOUT TIME ZONE NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_by UUID,
    updated_at TIMESTAMP WITHOUT TIME ZONE,

    is_active BOOLEAN NOT NULL DEFAULT FALSE,
    inactive_at TIMESTAMP WITHOUT TIME ZONE,
    inactive_by UUID REFERENCES authentication.t_users(id),

    is_deleted BOOLEAN NOT NULL DEFAULT FALSE,
    deleted_at TIMESTAMP WITHOUT TIME ZONE,
    deleted_by UUID REFERENCES authentication.t_users(id),

    key VARCHAR(64) NOT NULL,
    version VARCHAR(8) NOT NULL DEFAULT '1.0.0',
    name VARCHAR(64) NOT NULL UNIQUE,
    type VARCHAR(8) NOT NULL CHECK (type IN ('EMAIL', 'SMS', 'PUSH')),
    subject VARCHAR(128),
    is_html BOOLEAN NOT NULL DEFAULT FALSE,
    content TEXT NOT NULL,
    variables JSONB
);
COMMENT ON TABLE notification.t_notification_templates IS 'Table to store notification templates for various notification types';
COMMENT ON COLUMN notification.t_notification_templates.created_by IS 'User who created the template';
COMMENT ON COLUMN notification.t_notification_templates.created_at IS 'Timestamp when the template was created';
COMMENT ON COLUMN notification.t_notification_templates.updated_by IS 'User who last updated the template';
COMMENT ON COLUMN notification.t_notification_templates.updated_at IS 'Timestamp when the template was last updated';
COMMENT ON COLUMN notification.t_notification_templates.is_active IS 'Indicates if the template is active';
COMMENT ON COLUMN notification.t_notification_templates.inactive_at IS 'Timestamp when the template was deactivated';
COMMENT ON COLUMN notification.t_notification_templates.inactive_by IS 'User who deactivated the template';
COMMENT ON COLUMN notification.t_notification_templates.is_deleted IS 'Indicates if the template is deleted';
COMMENT ON COLUMN notification.t_notification_templates.deleted_at IS 'Timestamp when the template was deleted';
COMMENT ON COLUMN notification.t_notification_templates.deleted_by IS 'User who deleted the template';
COMMENT ON COLUMN notification.t_notification_templates.key IS 'Unique key identifier for the notification template';
COMMENT ON COLUMN notification.t_notification_templates.version IS 'Version of the notification template';
COMMENT ON COLUMN notification.t_notification_templates.name IS 'Unique name of the notification template';
COMMENT ON COLUMN notification.t_notification_templates.type IS 'Type of notification: EMAIL, SMS, PUSH';
COMMENT ON COLUMN notification.t_notification_templates.subject IS 'Subject of the notification (for EMAIL type)';
COMMENT ON COLUMN notification.t_notification_templates.is_html IS 'Indicates if the content is in HTML format';
COMMENT ON COLUMN notification.t_notification_templates.content IS 'Content of the notification template with placeholders for variables';
COMMENT ON COLUMN notification.t_notification_templates.variables IS 'JSONB object defining variables used in the template';

CREATE INDEX idx_notification_templates_type ON notification.t_notification_templates(type);
COMMENT ON INDEX notification.idx_notification_templates_type IS 'Index to optimize queries filtering by template type';