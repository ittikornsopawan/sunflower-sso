using Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    //audit
    public DbSet<t_change_logs> t_change_logs { get; set; }
    public DbSet<t_request_logs> t_request_logs { get; set; }
    public DbSet<t_change_log_items> t_change_log_items { get; set; }

    //authentication
    public DbSet<t_user_authentications> t_user_authentications { get; set; }
    public DbSet<t_user_open_authentication> t_user_open_authentication { get; set; }
    public DbSet<t_user_referrer_mappings> t_user_referrer_mappings { get; set; }
    public DbSet<t_users> t_users { get; set; }

    //author
    public DbSet<t_user_attribute_mappings> t_user_attribute_mappings { get; set; }
    public DbSet<t_policies> t_policies { get; set; }
    public DbSet<t_policy_attribute_mappings> t_policy_attribute_mappings { get; set; }
    public DbSet<m_attributes> m_attributes { get; set; }
    public DbSet<t_policy_decision_logs> t_policy_decision_logs { get; set; }

    //key
    public DbSet<m_key_types> m_key_types { get; set; }
    public DbSet<m_keys> m_keys { get; set; }
    public DbSet<m_algorithms> m_algorithms { get; set; }

    //notification
    public DbSet<t_push_notifications> t_push_notifications { get; set; }

    //otp
    public DbSet<t_otp_logs> t_otp_logs { get; set; }
    public DbSet<t_otp> t_otp { get; set; }

    //profile
    public DbSet<m_user_profiles> m_user_profiles { get; set; }
    public DbSet<t_personal_contacts> t_personal_contacts { get; set; }
    public DbSet<m_parameters> m_parameters { get; set; }
    public DbSet<m_error_handlers> m_error_handlers { get; set; }
    public DbSet<t_addresses> t_addresses { get; set; }
    public DbSet<t_contacts> t_contacts { get; set; }
    public DbSet<t_files> t_files { get; set; }
    public DbSet<t_personal_info> t_personal_info { get; set; }
    public DbSet<t_personal_addresses> t_personal_addresses { get; set; }

    //session
    public DbSet<t_session_policies> t_session_policies { get; set; }
    public DbSet<t_session_attributes> t_session_attributes { get; set; }
    public DbSet<t_sessions> t_sessions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<t_change_logs>(entity =>
        {
            entity.ToTable("t_change_logs", "audit");

            entity.HasKey(e => e.id);
            entity.Property(e => e.id).HasColumnName("id");
            entity.Property(e => e.createdById).HasColumnName("created_by");
            entity.Property(e => e.createdAt).HasColumnName("created_at");
            entity.Property(e => e.tableName).HasColumnName("table_name");
            entity.Property(e => e.recordId).HasColumnName("record_id");
            entity.Property(e => e.operation).HasColumnName("operation");
            entity.Property(e => e.requestId).HasColumnName("request_id");
        });

        modelBuilder.Entity<t_change_log_items>(entity =>
        {
            entity.ToTable("t_change_log_items", "audit");

            entity.HasKey(e => e.id);
            entity.Property(e => e.id).HasColumnName("id");
            entity.Property(e => e.createdById).HasColumnName("created_by");
            entity.Property(e => e.createdAt).HasColumnName("created_at");
            entity.Property(e => e.headerId).HasColumnName("header_id");
            entity.Property(e => e.columnName).HasColumnName("column_name");
            entity.Property(e => e.oldValue).HasColumnName("old_value");
            entity.Property(e => e.newValue).HasColumnName("new_value");
        });

        modelBuilder.Entity<t_request_logs>(entity =>
        {
            entity.ToTable("t_request_logs", "audit");

            entity.HasKey(e => e.id);
            entity.Property(e => e.id).HasColumnName("id");
            entity.Property(e => e.createdById).HasColumnName("created_by");
            entity.Property(e => e.createdAt).HasColumnName("created_at");
            entity.Property(e => e.endpoint).HasColumnName("endpoint");
            entity.Property(e => e.method).HasColumnName("method");
            entity.Property(e => e.requestPayload).HasColumnName("request_payload");
            entity.Property(e => e.responsePayload).HasColumnName("response_payload");
            entity.Property(e => e.statusCode).HasColumnName("status_code");
            entity.Property(e => e.ipAddress).HasColumnName("ip_address");
            entity.Property(e => e.userAgent).HasColumnName("user_agent");
            entity.Property(e => e.durationMs).HasColumnName("duration_ms");
        });

        modelBuilder.Entity<t_user_authentications>(entity =>
        {
            entity.ToTable("t_user_authentications", "authentication");

            entity.HasKey(e => e.id);
            entity.Property(e => e.id).HasColumnName("id");
            entity.Property(e => e.createdById).HasColumnName("created_by");
            entity.Property(e => e.createdAt).HasColumnName("created_at");
            entity.Property(e => e.updatedById).HasColumnName("updated_by");
            entity.Property(e => e.updatedAt).HasColumnName("updated_at");
            entity.Property(e => e.isActive).HasColumnName("is_active");
            entity.Property(e => e.inactiveAt).HasColumnName("inactive_at");
            entity.Property(e => e.inactiveById).HasColumnName("inactive_by");
            entity.Property(e => e.isDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.deletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.deletedById).HasColumnName("deleted_by");
            entity.Property(e => e.effectiveAt).HasColumnName("effective_at");
            entity.Property(e => e.expiresAt).HasColumnName("expires_at");
            entity.Property(e => e.userId).HasColumnName("user_id");
            entity.Property(e => e.isTemporary).HasColumnName("is_temporary");
            entity.Property(e => e.algorithmId).HasColumnName("algorithm_id");
            entity.Property(e => e.algorithmKeys).HasColumnName("algorithm_keys");
            entity.Property(e => e.passwordHash).HasColumnName("password_hash");
        });

        modelBuilder.Entity<t_user_open_authentication>(entity =>
        {
            entity.ToTable("t_user_open_authentication", "authentication");

            entity.HasKey(e => e.id);
            entity.Property(e => e.id).HasColumnName("id");
            entity.Property(e => e.createdById).HasColumnName("created_by");
            entity.Property(e => e.createdAt).HasColumnName("created_at");
            entity.Property(e => e.updatedById).HasColumnName("updated_by");
            entity.Property(e => e.updatedAt).HasColumnName("updated_at");
            entity.Property(e => e.isActive).HasColumnName("is_active");
            entity.Property(e => e.inactiveAt).HasColumnName("inactive_at");
            entity.Property(e => e.inactiveById).HasColumnName("inactive_by");
            entity.Property(e => e.isDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.deletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.deletedById).HasColumnName("deleted_by");
            entity.Property(e => e.provider).HasColumnName("provider");
            entity.Property(e => e.providerName).HasColumnName("provider_name");
            entity.Property(e => e.providerUserId).HasColumnName("provider_user_id");
            entity.Property(e => e.userId).HasColumnName("user_id");
            entity.Property(e => e.email).HasColumnName("email");
            entity.Property(e => e.displayName).HasColumnName("display_name");
            entity.Property(e => e.profilePictureUrl).HasColumnName("profile_picture_url");
            entity.Property(e => e.accessToken).HasColumnName("access_token");
            entity.Property(e => e.refreshToken).HasColumnName("refresh_token");
            entity.Property(e => e.tokenExpiresAt).HasColumnName("token_expires_at");
            entity.Property(e => e.rawResponse).HasColumnName("raw_response");
        });

        modelBuilder.Entity<t_user_referrer_mappings>(entity =>
        {
            entity.ToTable("t_user_referrer_mappings", "authentication");

            entity.HasKey(e => e.id);
            entity.Property(e => e.id).HasColumnName("id");
            entity.Property(e => e.createdById).HasColumnName("created_by");
            entity.Property(e => e.createdAt).HasColumnName("created_at");
            entity.Property(e => e.userId).HasColumnName("user_id");
            entity.Property(e => e.referrerId).HasColumnName("referrer_id");
        });

        modelBuilder.Entity<t_users>(entity =>
        {
            entity.ToTable("t_users", "authentication");

            entity.HasKey(e => e.id);
            entity.Property(e => e.id).HasColumnName("id");
            entity.Property(e => e.createdById).HasColumnName("created_by");
            entity.Property(e => e.createdAt).HasColumnName("created_at");
            entity.Property(e => e.updatedById).HasColumnName("updated_by");
            entity.Property(e => e.updatedAt).HasColumnName("updated_at");
            entity.Property(e => e.isActive).HasColumnName("is_active");
            entity.Property(e => e.inactiveAt).HasColumnName("inactive_at");
            entity.Property(e => e.inactiveById).HasColumnName("inactive_by");
            entity.Property(e => e.isDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.deletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.deletedById).HasColumnName("deleted_by");
            entity.Property(e => e.code).HasColumnName("code");
            entity.Property(e => e.username).HasColumnName("username");
            entity.Property(e => e.authenticationType).HasColumnName("authentication_type");
        });

        modelBuilder.Entity<m_attributes>(entity =>
        {
            entity.ToTable("m_attributes", "author");

            entity.HasKey(e => e.id);
            entity.Property(e => e.id).HasColumnName("id");
            entity.Property(e => e.createdById).HasColumnName("created_by");
            entity.Property(e => e.createdAt).HasColumnName("created_at");
            entity.Property(e => e.updatedById).HasColumnName("updated_by");
            entity.Property(e => e.updatedAt).HasColumnName("updated_at");
            entity.Property(e => e.isActive).HasColumnName("is_active");
            entity.Property(e => e.inactiveAt).HasColumnName("inactive_at");
            entity.Property(e => e.inactiveById).HasColumnName("inactive_by");
            entity.Property(e => e.isDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.deletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.deletedById).HasColumnName("deleted_by");
            entity.Property(e => e.isParameter).HasColumnName("is_parameter");
            entity.Property(e => e.isRequired).HasColumnName("is_required");
            entity.Property(e => e.isDisplay).HasColumnName("is_display");
            entity.Property(e => e.category).HasColumnName("category");
            entity.Property(e => e.keyGroup).HasColumnName("key_group");
            entity.Property(e => e.name).HasColumnName("name");
            entity.Property(e => e.key).HasColumnName("key");
            entity.Property(e => e.dataType).HasColumnName("data_type");
            entity.Property(e => e.title).HasColumnName("title");
            entity.Property(e => e.description).HasColumnName("description");
        });

        modelBuilder.Entity<t_policies>(entity =>
        {
            entity.ToTable("t_policies", "author");

            entity.HasKey(e => e.id);
            entity.Property(e => e.id).HasColumnName("id");
            entity.Property(e => e.createdById).HasColumnName("created_by");
            entity.Property(e => e.createdAt).HasColumnName("created_at");
            entity.Property(e => e.updatedById).HasColumnName("updated_by");
            entity.Property(e => e.updatedAt).HasColumnName("updated_at");
            entity.Property(e => e.isActive).HasColumnName("is_active");
            entity.Property(e => e.inactiveAt).HasColumnName("inactive_at");
            entity.Property(e => e.inactiveById).HasColumnName("inactive_by");
            entity.Property(e => e.isDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.deletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.deletedById).HasColumnName("deleted_by");
            entity.Property(e => e.name).HasColumnName("name");
            entity.Property(e => e.description).HasColumnName("description");
            entity.Property(e => e.code).HasColumnName("code");
            entity.Property(e => e.effect).HasColumnName("effect");
            entity.Property(e => e.action).HasColumnName("action");
            entity.Property(e => e.resource).HasColumnName("resource");
            entity.Property(e => e.conditionLogic).HasColumnName("condition_logic");
        });

        modelBuilder.Entity<t_policy_attribute_mappings>(entity =>
        {
            entity.ToTable("t_policy_attribute_mappings", "author");

            entity.HasKey(e => e.id);
            entity.Property(e => e.id).HasColumnName("id");
            entity.Property(e => e.createdById).HasColumnName("created_by");
            entity.Property(e => e.createdAt).HasColumnName("created_at");
            entity.Property(e => e.updatedById).HasColumnName("updated_by");
            entity.Property(e => e.updatedAt).HasColumnName("updated_at");
            entity.Property(e => e.isActive).HasColumnName("is_active");
            entity.Property(e => e.inactiveAt).HasColumnName("inactive_at");
            entity.Property(e => e.inactiveById).HasColumnName("inactive_by");
            entity.Property(e => e.isDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.deletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.deletedById).HasColumnName("deleted_by");
            entity.Property(e => e.policyId).HasColumnName("policy_id");
            entity.Property(e => e.attributeId).HasColumnName("attribute_id");
            entity.Property(e => e.@operator).HasColumnName("operator");
            entity.Property(e => e.expectedValue).HasColumnName("expected_value");
            entity.Property(e => e.logicGroup).HasColumnName("logic_group");
        });

        modelBuilder.Entity<t_policy_decision_logs>(entity =>
        {
            entity.ToTable("t_policy_decision_logs", "author");

            entity.HasKey(e => e.id);
            entity.Property(e => e.id).HasColumnName("id");
            entity.Property(e => e.createdById).HasColumnName("created_by");
            entity.Property(e => e.createdAt).HasColumnName("created_at");
            entity.Property(e => e.userId).HasColumnName("user_id");
            entity.Property(e => e.policyId).HasColumnName("policy_id");
            entity.Property(e => e.resource).HasColumnName("resource");
            entity.Property(e => e.action).HasColumnName("action");
            entity.Property(e => e.decision).HasColumnName("decision");
            entity.Property(e => e.evaluatedAttributes).HasColumnName("evaluated_attributes");
            entity.Property(e => e.reason).HasColumnName("reason");
        });

        modelBuilder.Entity<t_user_attribute_mappings>(entity =>
        {
            entity.ToTable("t_user_attribute_mappings", "author");

            entity.HasKey(e => e.id);
            entity.Property(e => e.id).HasColumnName("id");
            entity.Property(e => e.createdById).HasColumnName("created_by");
            entity.Property(e => e.createdAt).HasColumnName("created_at");
            entity.Property(e => e.updatedById).HasColumnName("updated_by");
            entity.Property(e => e.updatedAt).HasColumnName("updated_at");
            entity.Property(e => e.isActive).HasColumnName("is_active");
            entity.Property(e => e.inactiveAt).HasColumnName("inactive_at");
            entity.Property(e => e.inactiveById).HasColumnName("inactive_by");
            entity.Property(e => e.isDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.deletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.deletedById).HasColumnName("deleted_by");
            entity.Property(e => e.userId).HasColumnName("user_id");
            entity.Property(e => e.attributeId).HasColumnName("attribute_id");
            entity.Property(e => e.value).HasColumnName("value");
        });

        modelBuilder.Entity<m_algorithms>(entity =>
        {
            entity.ToTable("m_algorithms", "key");

            entity.HasKey(e => e.id);
            entity.Property(e => e.id).HasColumnName("id");
            entity.Property(e => e.createdById).HasColumnName("created_by");
            entity.Property(e => e.createdAt).HasColumnName("created_at");
            entity.Property(e => e.updatedById).HasColumnName("updated_by");
            entity.Property(e => e.updatedAt).HasColumnName("updated_at");
            entity.Property(e => e.isActive).HasColumnName("is_active");
            entity.Property(e => e.inactiveAt).HasColumnName("inactive_at");
            entity.Property(e => e.inactiveById).HasColumnName("inactive_by");
            entity.Property(e => e.isDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.deletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.deletedById).HasColumnName("deleted_by");
            entity.Property(e => e.effectiveAt).HasColumnName("effective_at");
            entity.Property(e => e.expiresAt).HasColumnName("expires_at");
            entity.Property(e => e.name).HasColumnName("name");
            entity.Property(e => e.algorithm).HasColumnName("algorithm");
            entity.Property(e => e.keyRequired).HasColumnName("key_required");
        });

        modelBuilder.Entity<m_key_types>(entity =>
        {
            entity.ToTable("m_key_types", "key");

            entity.HasKey(e => e.id);
            entity.Property(e => e.id).HasColumnName("id");
            entity.Property(e => e.createdById).HasColumnName("created_by");
            entity.Property(e => e.createdAt).HasColumnName("created_at");
            entity.Property(e => e.updatedById).HasColumnName("updated_by");
            entity.Property(e => e.updatedAt).HasColumnName("updated_at");
            entity.Property(e => e.isActive).HasColumnName("is_active");
            entity.Property(e => e.inactiveAt).HasColumnName("inactive_at");
            entity.Property(e => e.inactiveById).HasColumnName("inactive_by");
            entity.Property(e => e.isDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.deletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.deletedById).HasColumnName("deleted_by");
            entity.Property(e => e.effectiveAt).HasColumnName("effective_at");
            entity.Property(e => e.expiresAt).HasColumnName("expires_at");
            entity.Property(e => e.name).HasColumnName("name");
            entity.Property(e => e.title).HasColumnName("title");
            entity.Property(e => e.description).HasColumnName("description");
        });

        modelBuilder.Entity<m_keys>(entity =>
        {
            entity.ToTable("m_keys", "key");

            entity.HasKey(e => e.id);
            entity.Property(e => e.id).HasColumnName("id");
            entity.Property(e => e.createdById).HasColumnName("created_by");
            entity.Property(e => e.createdAt).HasColumnName("created_at");
            entity.Property(e => e.updatedById).HasColumnName("updated_by");
            entity.Property(e => e.updatedAt).HasColumnName("updated_at");
            entity.Property(e => e.isActive).HasColumnName("is_active");
            entity.Property(e => e.inactiveAt).HasColumnName("inactive_at");
            entity.Property(e => e.inactiveById).HasColumnName("inactive_by");
            entity.Property(e => e.isDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.deletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.deletedById).HasColumnName("deleted_by");
            entity.Property(e => e.effectiveAt).HasColumnName("effective_at");
            entity.Property(e => e.expiresAt).HasColumnName("expires_at");
            entity.Property(e => e.typeId).HasColumnName("type_id");
            entity.Property(e => e.key).HasColumnName("key");
        });

        modelBuilder.Entity<t_push_notifications>(entity =>
        {
            entity.ToTable("t_push_notifications", "notification");

            entity.HasKey(e => e.id);
            entity.Property(e => e.id).HasColumnName("id");
            entity.Property(e => e.createdById).HasColumnName("created_by");
            entity.Property(e => e.createdAt).HasColumnName("created_at");
            entity.Property(e => e.updatedById).HasColumnName("updated_by");
            entity.Property(e => e.updatedAt).HasColumnName("updated_at");
            entity.Property(e => e.isActive).HasColumnName("is_active");
            entity.Property(e => e.inactiveAt).HasColumnName("inactive_at");
            entity.Property(e => e.inactiveById).HasColumnName("inactive_by");
            entity.Property(e => e.isDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.deletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.deletedById).HasColumnName("deleted_by");
            entity.Property(e => e.type).HasColumnName("type");
            entity.Property(e => e.message).HasColumnName("message");
            entity.Property(e => e.userId).HasColumnName("user_id");
            entity.Property(e => e.contactId).HasColumnName("contact_id");
            entity.Property(e => e.deliveryStatus).HasColumnName("delivery_status");
            entity.Property(e => e.metadata).HasColumnName("metadata");
        });

        modelBuilder.Entity<t_otp>(entity =>
        {
            entity.ToTable("t_otp", "otp");

            entity.HasKey(e => e.id);
            entity.Property(e => e.id).HasColumnName("id");
            entity.Property(e => e.createdById).HasColumnName("created_by");
            entity.Property(e => e.createdAt).HasColumnName("created_at");
            entity.Property(e => e.updatedById).HasColumnName("updated_by");
            entity.Property(e => e.updatedAt).HasColumnName("updated_at");
            entity.Property(e => e.isActive).HasColumnName("is_active");
            entity.Property(e => e.inactiveAt).HasColumnName("inactive_at");
            entity.Property(e => e.inactiveById).HasColumnName("inactive_by");
            entity.Property(e => e.isDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.deletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.deletedById).HasColumnName("deleted_by");
            entity.Property(e => e.expiresAt).HasColumnName("expires_at");
            entity.Property(e => e.refCode).HasColumnName("ref_code");
            entity.Property(e => e.otp).HasColumnName("otp");
            entity.Property(e => e.verifyCount).HasColumnName("verify_count");
        });

        modelBuilder.Entity<t_otp_logs>(entity =>
        {
            entity.ToTable("t_otp_logs", "otp");

            entity.HasKey(e => e.id);
            entity.Property(e => e.id).HasColumnName("id");
            entity.Property(e => e.createdById).HasColumnName("created_by");
            entity.Property(e => e.createdAt).HasColumnName("created_at");
            entity.Property(e => e.otpId).HasColumnName("otp_id");
            entity.Property(e => e.countNo).HasColumnName("count_no");
            entity.Property(e => e.context).HasColumnName("context");
            entity.Property(e => e.ipAddress).HasColumnName("ip_address");
            entity.Property(e => e.deviceId).HasColumnName("device_id");
            entity.Property(e => e.deviceOs).HasColumnName("device_os");
            entity.Property(e => e.locationName).HasColumnName("location_name");
            entity.Property(e => e.latitude).HasColumnName("latitude");
            entity.Property(e => e.longitude).HasColumnName("longitude");
            entity.Property(e => e.geofenceCenter).HasColumnName("geofence_center");
            entity.Property(e => e.geofenceRadiusMeters).HasColumnName("geofence_radius_meters");
            entity.Property(e => e.result).HasColumnName("result");
            entity.Property(e => e.remark).HasColumnName("remark");
        });

        modelBuilder.Entity<m_user_profiles>(entity =>
        {
            entity.ToTable("m_user_profiles", "profile");

            entity.HasKey(e => e.id);
            entity.Property(e => e.id).HasColumnName("id");
            entity.Property(e => e.createdById).HasColumnName("created_by");
            entity.Property(e => e.createdAt).HasColumnName("created_at");
            entity.Property(e => e.updatedById).HasColumnName("updated_by");
            entity.Property(e => e.updatedAt).HasColumnName("updated_at");
            entity.Property(e => e.isActive).HasColumnName("is_active");
            entity.Property(e => e.inactiveAt).HasColumnName("inactive_at");
            entity.Property(e => e.inactiveById).HasColumnName("inactive_by");
            entity.Property(e => e.isDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.deletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.deletedById).HasColumnName("deleted_by");
            entity.Property(e => e.userId).HasColumnName("user_id");
            entity.Property(e => e.personalId).HasColumnName("personal_id");
        });

        modelBuilder.Entity<m_error_handlers>(entity =>
        {
            entity.ToTable("m_error_handlers", "public");

            entity.HasKey(e => e.id);
            entity.Property(e => e.id).HasColumnName("id");
            entity.Property(e => e.createdById).HasColumnName("created_by");
            entity.Property(e => e.createdAt).HasColumnName("created_at");
            entity.Property(e => e.updatedById).HasColumnName("updated_by");
            entity.Property(e => e.updatedAt).HasColumnName("updated_at");
            entity.Property(e => e.isActive).HasColumnName("is_active");
            entity.Property(e => e.inactiveAt).HasColumnName("inactive_at");
            entity.Property(e => e.inactiveById).HasColumnName("inactive_by");
            entity.Property(e => e.isDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.deletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.deletedById).HasColumnName("deleted_by");
            entity.Property(e => e.statusCode).HasColumnName("status_code");
            entity.Property(e => e.code).HasColumnName("code");
            entity.Property(e => e.message).HasColumnName("message");
            entity.Property(e => e.language).HasColumnName("language"); // language เป็น keyword จึงใช้ languageCode
        });

        modelBuilder.Entity<m_parameters>(entity =>
        {
            entity.ToTable("m_parameters", "public");

            entity.HasKey(e => e.id);
            entity.Property(e => e.id).HasColumnName("id");
            entity.Property(e => e.createdById).HasColumnName("created_by");
            entity.Property(e => e.createdAt).HasColumnName("created_at");
            entity.Property(e => e.updatedById).HasColumnName("updated_by");
            entity.Property(e => e.updatedAt).HasColumnName("updated_at");
            entity.Property(e => e.isActive).HasColumnName("is_active");
            entity.Property(e => e.inactiveAt).HasColumnName("inactive_at");
            entity.Property(e => e.inactiveById).HasColumnName("inactive_by");
            entity.Property(e => e.isDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.deletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.deletedById).HasColumnName("deleted_by");
            entity.Property(e => e.effectiveAt).HasColumnName("effective_at");
            entity.Property(e => e.expiresAt).HasColumnName("expires_at");
            entity.Property(e => e.category).HasColumnName("category");
            entity.Property(e => e.key).HasColumnName("key");
            entity.Property(e => e.title).HasColumnName("title");
            entity.Property(e => e.description).HasColumnName("description");
            entity.Property(e => e.language).HasColumnName("language");
            entity.Property(e => e.value).HasColumnName("value");

            entity.Ignore(e => e.code);
        });

        modelBuilder.Entity<t_addresses>(entity =>
        {
            entity.ToTable("t_addresses", "public");

            entity.HasKey(e => e.id);
            entity.Property(e => e.id).HasColumnName("id");
            entity.Property(e => e.createdById).HasColumnName("created_by");
            entity.Property(e => e.createdAt).HasColumnName("created_at");
            entity.Property(e => e.updatedById).HasColumnName("updated_by");
            entity.Property(e => e.updatedAt).HasColumnName("updated_at");
            entity.Property(e => e.isActive).HasColumnName("is_active");
            entity.Property(e => e.inactiveAt).HasColumnName("inactive_at");
            entity.Property(e => e.inactiveById).HasColumnName("inactive_by");
            entity.Property(e => e.isDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.deletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.deletedById).HasColumnName("deleted_by");
            entity.Property(e => e.effectiveAt).HasColumnName("effective_at");
            entity.Property(e => e.expiresAt).HasColumnName("expires_at");
            entity.Property(e => e.type).HasColumnName("type"); // type เป็น keyword จึงใช้ typeName
            entity.Property(e => e.address).HasColumnName("address");
            entity.Property(e => e.addressAdditional).HasColumnName("address_additional");
            entity.Property(e => e.countryCode).HasColumnName("country_code");
            entity.Property(e => e.countryName).HasColumnName("country_name");
            entity.Property(e => e.state).HasColumnName("state");
            entity.Property(e => e.city).HasColumnName("city");
            entity.Property(e => e.district).HasColumnName("district");
            entity.Property(e => e.subDistrict).HasColumnName("sub_district");
            entity.Property(e => e.postalCode).HasColumnName("postal_code");
            entity.Property(e => e.geofenceArea).HasColumnName("geofence_area");
            entity.Property(e => e.geofenceCenter).HasColumnName("geofence_center");
            entity.Property(e => e.geofenceRadiusMeters).HasColumnName("geofence_radius_meters");
        });

        modelBuilder.Entity<t_contacts>(entity =>
        {
            entity.ToTable("t_contacts", "public");

            entity.HasKey(e => e.id);
            entity.Property(e => e.id).HasColumnName("id");
            entity.Property(e => e.createdById).HasColumnName("created_by");
            entity.Property(e => e.createdAt).HasColumnName("created_at");
            entity.Property(e => e.updatedById).HasColumnName("updated_by");
            entity.Property(e => e.updatedAt).HasColumnName("updated_at");
            entity.Property(e => e.isActive).HasColumnName("is_active");
            entity.Property(e => e.inactiveAt).HasColumnName("inactive_at");
            entity.Property(e => e.inactiveById).HasColumnName("inactive_by");
            entity.Property(e => e.isDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.deletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.deletedById).HasColumnName("deleted_by");
            entity.Property(e => e.channel).HasColumnName("channel");
            entity.Property(e => e.contact).HasColumnName("contact");
            entity.Property(e => e.contactName).HasColumnName("contact_name");
            entity.Property(e => e.available).HasColumnName("available");
            entity.Property(e => e.remark).HasColumnName("remark");
        });

        modelBuilder.Entity<t_files>(entity =>
        {
            entity.ToTable("t_files", "public");

            entity.HasKey(e => e.id);
            entity.Property(e => e.id).HasColumnName("id");
            entity.Property(e => e.createdById).HasColumnName("created_by");
            entity.Property(e => e.createdAt).HasColumnName("created_at");
            entity.Property(e => e.updatedById).HasColumnName("updated_by");
            entity.Property(e => e.updatedAt).HasColumnName("updated_at");
            entity.Property(e => e.isActive).HasColumnName("is_active");
            entity.Property(e => e.inactiveAt).HasColumnName("inactive_at");
            entity.Property(e => e.inactiveById).HasColumnName("inactive_by");
            entity.Property(e => e.isDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.deletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.deletedById).HasColumnName("deleted_by");
            entity.Property(e => e.effectiveAt).HasColumnName("effective_at");
            entity.Property(e => e.expiresAt).HasColumnName("expires_at");
            entity.Property(e => e.usageType).HasColumnName("usage_type");
            entity.Property(e => e.filePath).HasColumnName("file_path");
            entity.Property(e => e.fileName).HasColumnName("file_name");
            entity.Property(e => e.fileSize).HasColumnName("file_size");
            entity.Property(e => e.fileSizeUnit).HasColumnName("file_size_unit");
            entity.Property(e => e.fileDimension).HasColumnName("file_dimension");
            entity.Property(e => e.fileExtension).HasColumnName("file_extension");
            entity.Property(e => e.mimeType).HasColumnName("mime_type");
            entity.Property(e => e.description).HasColumnName("description");
            entity.Property(e => e.storageProvider).HasColumnName("storage_provider");
            entity.Property(e => e.storageBucket).HasColumnName("storage_bucket");
            entity.Property(e => e.storageKey).HasColumnName("storage_key");
        });

        modelBuilder.Entity<t_personal_addresses>(entity =>
        {
            entity.ToTable("t_personal_addresses", "public");

            entity.HasKey(e => e.id);
            entity.Property(e => e.id).HasColumnName("id");
            entity.Property(e => e.createdById).HasColumnName("created_by");
            entity.Property(e => e.createdAt).HasColumnName("created_at");
            entity.Property(e => e.updatedById).HasColumnName("updated_by");
            entity.Property(e => e.updatedAt).HasColumnName("updated_at");
            entity.Property(e => e.isActive).HasColumnName("is_active");
            entity.Property(e => e.inactiveAt).HasColumnName("inactive_at");
            entity.Property(e => e.inactiveById).HasColumnName("inactive_by");
            entity.Property(e => e.isDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.deletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.deletedById).HasColumnName("deleted_by");
            entity.Property(e => e.personalId).HasColumnName("personal_id");
            entity.Property(e => e.addressId).HasColumnName("address_id");
        });

        modelBuilder.Entity<t_personal_contacts>(entity =>
        {
            entity.ToTable("t_personal_contacts", "public");

            entity.HasKey(e => e.id);
            entity.Property(e => e.id).HasColumnName("id");
            entity.Property(e => e.createdById).HasColumnName("created_by");
            entity.Property(e => e.createdAt).HasColumnName("created_at");
            entity.Property(e => e.updatedById).HasColumnName("updated_by");
            entity.Property(e => e.updatedAt).HasColumnName("updated_at");
            entity.Property(e => e.isActive).HasColumnName("is_active");
            entity.Property(e => e.inactiveAt).HasColumnName("inactive_at");
            entity.Property(e => e.inactiveById).HasColumnName("inactive_by");
            entity.Property(e => e.isDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.deletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.deletedById).HasColumnName("deleted_by");
            entity.Property(e => e.personalId).HasColumnName("personal_id");
            entity.Property(e => e.contactId).HasColumnName("contact_id");
        });

        modelBuilder.Entity<t_personal_info>(entity =>
        {
            entity.ToTable("t_personal_info", "public");

            entity.HasKey(e => e.id);
            entity.Property(e => e.id).HasColumnName("id");
            entity.Property(e => e.createdById).HasColumnName("created_by");
            entity.Property(e => e.createdAt).HasColumnName("created_at");
            entity.Property(e => e.updatedById).HasColumnName("updated_by");
            entity.Property(e => e.updatedAt).HasColumnName("updated_at");
            entity.Property(e => e.isActive).HasColumnName("is_active");
            entity.Property(e => e.inactiveAt).HasColumnName("inactive_at");
            entity.Property(e => e.inactiveById).HasColumnName("inactive_by");
            entity.Property(e => e.isDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.deletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.deletedById).HasColumnName("deleted_by");
            entity.Property(e => e.sid).HasColumnName("sid");
            entity.Property(e => e.prefixName).HasColumnName("prefix_name");
            entity.Property(e => e.firstName).HasColumnName("first_name");
            entity.Property(e => e.middleName).HasColumnName("middle_name");
            entity.Property(e => e.lastName).HasColumnName("last_name");
            entity.Property(e => e.nickName).HasColumnName("nick_name");
            entity.Property(e => e.gender).HasColumnName("gender");
            entity.Property(e => e.dateOfBirth).HasColumnName("date_of_birth");
        });

        modelBuilder.Entity<t_session_attributes>(entity =>
        {
            entity.ToTable("t_session_attributes", "session");

            entity.HasKey(e => e.id);
            entity.Property(e => e.id).HasColumnName("id");
            entity.Property(e => e.createdById).HasColumnName("created_by");
            entity.Property(e => e.createdAt).HasColumnName("created_at");
            entity.Property(e => e.updatedById).HasColumnName("updated_by");
            entity.Property(e => e.updatedAt).HasColumnName("updated_at");
            entity.Property(e => e.isActive).HasColumnName("is_active");
            entity.Property(e => e.inactiveAt).HasColumnName("inactive_at");
            entity.Property(e => e.inactiveById).HasColumnName("inactive_by");
            entity.Property(e => e.isDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.deletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.deletedById).HasColumnName("deleted_by");
            entity.Property(e => e.sessionId).HasColumnName("session_id");
            entity.Property(e => e.attributeId).HasColumnName("attribute_id");
            entity.Property(e => e.values).HasColumnName("values");
        });

        modelBuilder.Entity<t_session_policies>(entity =>
        {
            entity.ToTable("t_session_policies", "session");

            entity.HasKey(e => e.id);
            entity.Property(e => e.id).HasColumnName("id");
            entity.Property(e => e.createdById).HasColumnName("created_by");
            entity.Property(e => e.createdAt).HasColumnName("created_at");
            entity.Property(e => e.updatedById).HasColumnName("updated_by");
            entity.Property(e => e.updatedAt).HasColumnName("updated_at");
            entity.Property(e => e.isActive).HasColumnName("is_active");
            entity.Property(e => e.inactiveAt).HasColumnName("inactive_at");
            entity.Property(e => e.inactiveById).HasColumnName("inactive_by");
            entity.Property(e => e.isDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.deletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.deletedById).HasColumnName("deleted_by");
            entity.Property(e => e.sessionId).HasColumnName("session_id");
            entity.Property(e => e.policyId).HasColumnName("policy_id");
            entity.Property(e => e.values).HasColumnName("values");
        });

        modelBuilder.Entity<t_sessions>(entity =>
        {
            entity.ToTable("t_sessions", "session");

            entity.HasKey(e => e.id);
            entity.Property(e => e.id).HasColumnName("id");
            entity.Property(e => e.createdById).HasColumnName("created_by");
            entity.Property(e => e.createdAt).HasColumnName("created_at");
            entity.Property(e => e.updatedById).HasColumnName("updated_by");
            entity.Property(e => e.updatedAt).HasColumnName("updated_at");
            entity.Property(e => e.isActive).HasColumnName("is_active");
            entity.Property(e => e.inactiveAt).HasColumnName("inactive_at");
            entity.Property(e => e.inactiveById).HasColumnName("inactive_by");
            entity.Property(e => e.isRevoked).HasColumnName("is_revoked");
            entity.Property(e => e.revokedAt).HasColumnName("revoked_at");
            entity.Property(e => e.revokedBy).HasColumnName("revoked_by");
            entity.Property(e => e.revokedReason).HasColumnName("revoked_reason");
            entity.Property(e => e.expiresAt).HasColumnName("expires_at");
            entity.Property(e => e.userId).HasColumnName("user_id");
            entity.Property(e => e.code).HasColumnName("code");
            entity.Property(e => e.accessToken).HasColumnName("access_token");
            entity.Property(e => e.accessTokenExpiresAt).HasColumnName("access_token_expires_at");
            entity.Property(e => e.refreshAccessToken).HasColumnName("refresh_access_token");
            entity.Property(e => e.refreshAccessTokenExpiresAt).HasColumnName("refresh_access_token_expires_at");
            entity.Property(e => e.payload).HasColumnName("payload");
        });
    }
}
