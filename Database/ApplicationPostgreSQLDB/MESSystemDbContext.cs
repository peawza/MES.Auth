
using Application.Models.MesSystem;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public class MESSystemDbContext : DbContext
    {
        public virtual DbSet<mes_Department> Department { get; set; }
        public virtual DbSet<mes_GroupPermission> GroupPermission { get; set; }
        public virtual DbSet<mes_Module> Module { get; set; }
        public virtual DbSet<mes_SubModule> SubModule { get; set; }
        public virtual DbSet<mes_Permission> Permission { get; set; }
        public virtual DbSet<mes_Role> Role { get; set; }
        public virtual DbSet<mes_UserRoles> UserRoles { get; set; }
        public virtual DbSet<mes_Screen> Screen { get; set; }

        public virtual DbSet<mes_ScreenFunction> ScreenFunction { get; set; }


        public MESSystemDbContext(DbContextOptions<MESSystemDbContext> options)
            : base(options)
        {
        }


        public static void InitialService(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<MESSystemDbContext>(options =>
                options.UseNpgsql(connectionString));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("mes");
            modelBuilder.Entity<mes_Department>().ToTable("mes_Department");
            modelBuilder.Entity<mes_GroupPermission>().ToTable("mes_GroupPermission");
            modelBuilder.Entity<mes_Module>().ToTable("mes_Module");
            modelBuilder.Entity<mes_SubModule>().ToTable("mes_SubModule");
            modelBuilder.Entity<mes_Permission>().ToTable("mes_Permission");
            modelBuilder.Entity<mes_Role>().ToTable("mes_Role");
            modelBuilder.Entity<mes_UserRoles>().ToTable("mes_UserRoles");
            modelBuilder.Entity<mes_Screen>().ToTable("mes_Screen");
            modelBuilder.Entity<mes_ScreenFunction>().ToTable("mes_ScreenFunction");

        }
    }
}
