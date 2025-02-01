using Application.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace Application
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<tb_UserInfo> UserInfos { get; set; }
        public DbSet<tb_PasswordHistory> PasswordHistories { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<tb_UserLogTrail> Userlogtrails { get; set; }

        public ApplicationDbContext(
                DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public static void InitialService(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(connectionString));
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.HasDefaultSchema("public");

            builder.Entity<ApplicationUser>().ToTable("tb_User", "public");

            builder.Entity<IdentityRole>().ToTable("tb_Role", "public");
            builder.Entity<ApplicationRole>().ToTable("tb_Role", "public");

            builder.Entity<IdentityUserClaim<string>>().ToTable("tb_UserClaim", "public");
            builder.Entity<IdentityUserRole<string>>().ToTable("tb_UserRole", "public");
            builder.Entity<IdentityUserLogin<string>>().ToTable("tb_UserLogin", "public");
            builder.Entity<IdentityUserToken<string>>().ToTable("tb_UserToken", "public");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("tb_RoleClaim", "public");

            builder.Entity<ApplicationUser>()
                .HasMany(t => t.PasswordHistories)
                .WithOne(t => t.User)
                .HasForeignKey("Id");
        }
    }
}
