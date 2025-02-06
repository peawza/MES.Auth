using Master.Libs.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public class SystemDbContext : DbContext
    {


        public SystemDbContext(DbContextOptions<SystemDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<tb_Department> Department { get; set; }
        public virtual DbSet<tb_GroupPermission> GroupPermission { get; set; }
        public virtual DbSet<tb_Module> Module { get; set; }
        public virtual DbSet<tb_Permission> tb_Permission { get; set; }
        public virtual DbSet<tb_Role> tb_Role { get; set; }
        public virtual DbSet<tb_Screen> tb_Screen { get; set; }
        public virtual DbSet<tb_ScreenFunction> tb_ScreenFunction { get; set; }


        public static void InitialService(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<SystemDbContext>(options =>
                options.UseNpgsql(connectionString));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("mes");


        }
    }
}
