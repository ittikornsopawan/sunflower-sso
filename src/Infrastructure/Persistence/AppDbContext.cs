using Domain.Entities;
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

    }
}
