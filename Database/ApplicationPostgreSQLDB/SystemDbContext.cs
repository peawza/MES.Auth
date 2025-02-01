using Application.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public class SystemDbContext : DbContext
    {
        public DbSet<tb_Application> Applications { get; set; }
        public DbSet<tb_MenuSetting> MenuSettings { get; set; }
        public DbSet<tb_MenuName> MenuNames { get; set; }
        public DbSet<tb_Screen> Screens { get; set; }
        public DbSet<tb_ScreenName> ScreenNames { get; set; }
        public DbSet<tb_ScreenPermission> ScreenPermissions { get; set; }
        public DbSet<tb_Permission> Permissions { get; set; }
        public DbSet<tb_PermissionName> PermissionNames { get; set; }
        public DbSet<tb_LocalizedMessages> LocalizedMessages { get; set; }
        public DbSet<tb_LocalizedResources> LocalizedResources { get; set; }

        public SystemDbContext(DbContextOptions<SystemDbContext> options)
            : base(options)
        {
        }

        public static void InitialService(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<SystemDbContext>(options =>
                options.UseNpgsql(connectionString));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("public");

            modelBuilder.Entity<tb_Screen>()
                .HasMany(e => e.ScreenNames)
                .WithOne(e => e.Screen)
                .HasForeignKey("AppCode", "ScreenId");

            modelBuilder.Entity<tb_Screen>()
                .HasMany(e => e.MenuSettings)
                .WithOne(e => e.Screen)
                .HasForeignKey("AppCode", "ScreenId");

            modelBuilder.Entity<tb_Screen>()
                .HasMany(e => e.ScreenPermissions)
                .WithOne(e => e.Screen)
                .HasForeignKey("AppCode", "ScreenId");

            modelBuilder.Entity<tb_Permission>()
                .HasMany(e => e.PermissionNames)
                .WithOne(e => e.Permission)
                .HasForeignKey("PermissionCode");

            modelBuilder.Entity<tb_Permission>()
               .HasMany(e => e.ScreenPermissions)
               .WithOne(e => e.Permission)
               .HasForeignKey("PermissionCode");

            modelBuilder.Entity<tb_MenuSetting>()
               .HasMany(e => e.MenuNames)
               .WithOne(e => e.MenuSetting)
               .HasForeignKey("AppCode", "MenuId");

            //modelBuilder.Entity<tb_LocalizedMessages>()
            //    .HasKey(e => new { e.MessageCode, e.MessageType });
            modelBuilder.Entity<tb_LocalizedMessages>().ToTable("tb_LocalizedMessages");
            modelBuilder.Entity<tb_LocalizedResources>().ToTable("tb_LocalizedResources");
            //modelBuilder.Entity<tb_LocalizedResources>()
            //    .HasKey(e => new { e.ScreenCode, e.ObjectID });
        }
    }
}
