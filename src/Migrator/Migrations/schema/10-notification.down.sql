DROP INDEX IF EXISTS notification.idx_notification_templates_type;
DROP TABLE IF EXISTS notification.t_notification_templates;

DROP INDEX IF EXISTS notification.idx_notifications_status;
DROP INDEX IF EXISTS notification.idx_notifications_type;
DROP TABLE IF EXISTS notification.t_notifications;

DROP SCHEMA IF EXISTS notification CASCADE;