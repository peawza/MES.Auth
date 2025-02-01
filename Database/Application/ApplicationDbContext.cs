using Application.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<tb_UserInfo> UserInfos { get; set; }
        public DbSet<tb_PasswordHistory> PasswordHistories { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }

        public ApplicationDbContext(
                DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public static void InitialService(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>().ToTable("tb_User", "dbo");

            builder.Entity<IdentityRole>().ToTable("tb_Role", "dbo");
            builder.Entity<ApplicationRole>().ToTable("tb_Role", "dbo");

            builder.Entity<IdentityUserClaim<string>>().ToTable("tb_UserClaim", "dbo");
            builder.Entity<IdentityUserRole<string>>().ToTable("tb_UserRole", "dbo");
            builder.Entity<IdentityUserLogin<string>>().ToTable("tb_UserLogin", "dbo");
            builder.Entity<IdentityUserToken<string>>().ToTable("tb_UserToken", "dbo");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("tb_RoleClaim", "dbo");

            builder.Entity<ApplicationUser>()
                .HasMany(t => t.PasswordHistories)
                .WithOne(t => t.User)
                .HasForeignKey("Id");
        }
    }
}
