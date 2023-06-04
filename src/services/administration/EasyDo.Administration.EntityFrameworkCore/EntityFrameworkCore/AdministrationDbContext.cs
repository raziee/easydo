using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.FeatureManagement;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.PermissionManagement;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement;
using Volo.Abp.SettingManagement.EntityFrameworkCore;

namespace EasyDo.Administration.EntityFrameworkCore
{
    /* This is your actual DbContext used on runtime.
     * It includes only your entities.
     * It does not include entities of the used modules, because each module has already
     * its own DbContext class. If you want to share some database tables with the used modules,
     * just create a structure like done for AppUser.
     *
     * Don't use this DbContext for database migrations since it does not contain tables of the
     * used modules (as explained above). See AdministrationMigrationsDbContext for migrations.
     */
    [ConnectionStringName(AdministrationDbProperties.ConnectionStringName)]
    public class AdministrationDbContext : AbpDbContext<AdministrationDbContext>,
        IPermissionManagementDbContext,
        ISettingManagementDbContext,
        IFeatureManagementDbContext,
        IAuditLoggingDbContext,
        IAdministrationDbContext
    {
        public AdministrationDbContext(DbContextOptions<AdministrationDbContext> options)
            : base(options)
        {

        }
        
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<FeatureValue> FeatureValues { get; set; }

        public DbSet<PermissionGrant> PermissionGrants { get; set; }
        public DbSet<Setting> Settings { get; set; }

        public DbSet<PermissionGroupDefinitionRecord> PermissionGroups { get; set; }

        public DbSet<PermissionDefinitionRecord> Permissions { get; set; }

        public DbSet<FeatureGroupDefinitionRecord> FeatureGroups { get; set; }

        public DbSet<FeatureDefinitionRecord> Features { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigureAdministration();
            builder.ConfigurePermissionManagement();
            builder.ConfigureSettingManagement();
            builder.ConfigureAuditLogging();
            builder.ConfigureFeatureManagement();
        }
    }
}