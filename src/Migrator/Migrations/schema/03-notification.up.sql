CREATE SCHEMA IF NOT EXISTS notification;
COMMENT ON SCHEMA notification IS 'Schema for managing notifications including push, email, and SMS.';

CREATE TABLE IF NOT EXISTS notification.t_push_notifications
(
    id UUID NOT NULL DEFAULT GEN_RANDOM_UUID() PRIMARY KEY,
    created_by UUID NOT NULL REFERENCES authentication.t_users(id),
    created_at TIMESTAMP WITHOUT TIME ZONE NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_by UUID REFERENCES authentication.t_users(id),
    updated_at TIMESTAMP WITHOUT TIME ZONE,

    row_status VARCHAR(16) NOT NULL DEFAULT 'ACTIVE' CHECK (row_status IN ('ACTIVE', 'INACTIVE', 'DELETED', 'REVOKED')),

    type VARCHAR(16) NOT NULL CHECK (type IN ('EMAIL', 'SMS', 'PUSH')),
    message TEXT NOT NULL,
    user_id UUID REFERENCES authentication.t_users(id),
    contact_id UUID REFERENCES t_contacts(id),
    delivery_status VARCHAR(32) DEFAULT 'PENDING' CHECK (delivery_status IN ('PENDING','SENT','DELIVERED','FAILED')),
    metadata JSONB
);
COMMENT ON TABLE notification.t_push_notifications IS 'Stores push, email, and SMS notifications.';
COMMENT ON COLUMN notification.t_push_notifications.id IS 'Primary key for the notification record.';
COMMENT ON COLUMN notification.t_push_notifications.created_by IS 'User who created the notification (references authentication.t_users.id).';
COMMENT ON COLUMN notification.t_push_notifications.created_at IS 'Timestamp when the notification record was created.';
COMMENT ON COLUMN notification.t_push_notifications.updated_by IS 'User who last updated the notification record (references authentication.t_users.id).';
COMMENT ON COLUMN notification.t_push_notifications.updated_at IS 'Timestamp when the notification record was last updated.';
COMMENT ON COLUMN notification.t_push_notifications.row_status IS 'Status of the notification record (ACTIVE, INACTIVE, DELETED).';
COMMENT ON COLUMN notification.t_push_notifications.type IS 'Type of notification: EMAIL, SMS, PUSH.';
COMMENT ON COLUMN notification.t_push_notifications.message IS 'The content of the notification.';
COMMENT ON COLUMN notification.t_push_notifications.user_id IS 'Reference to the user who will receive the notification (authentication.t_users.id).';
COMMENT ON COLUMN notification.t_push_notifications.contact_id IS 'Reference to the contact (t_contacts.id) for SMS or email.';
COMMENT ON COLUMN notification.t_push_notifications.delivery_status IS 'Status of the notification: PENDING, SENT, DELIVERED, FAILED.';
COMMENT ON COLUMN notification.t_push_notifications.metadata IS 'Additional JSON metadata for the notification (e.g., platform info, push token, headers).';

CREATE INDEX IF NOT EXISTS idx_t_push_notifications_user_id_contact_id ON notification.t_push_notifications(user_id, contact_id);
COMMENT ON INDEX notification.idx_t_push_notifications_user_id_contact_id IS 'Index to optimize queries filtering by user_id and contact_id.';