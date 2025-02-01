using Application.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public SystemDbContext(DbContextOptions<SystemDbContext> options)
            : base(options)
        {
        }

        public static void InitialService(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<SystemDbContext>(options =>
                options.UseSqlServer(connectionString));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

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
        }
    }
}
