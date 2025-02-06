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

        public static void InitialService(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<SystemDbContext>(options =>
                options.UseNpgsql(connectionString));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("public");


        }
    }
}
