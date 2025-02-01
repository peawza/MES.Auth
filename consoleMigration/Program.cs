// See https://aka.ms/new-console-template for more information
using Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((hostingContext, config) =>
    {
        // Load configuration from appsettings.json
        config.SetBasePath(AppContext.BaseDirectory);
        config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        config.AddEnvironmentVariables();
    })
    .ConfigureServices((context, services) =>
    {
        var configuration = context.Configuration;

        // Ensure connection strings are correctly loaded
        string applicationDbConnection = configuration.GetConnectionString("ApplicationDb") ??
            throw new ArgumentException("ApplicationDb connection string is missing.");
        string systemDbConnection = configuration.GetConnectionString("SystemDb") ??
            throw new ArgumentException("SystemDb connection string is missing.");

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(applicationDbConnection));
        services.AddDbContext<SystemDbContext>(options =>
            options.UseNpgsql(systemDbConnection));
    })
    .Build();

// Apply EF Migrations
ApplyMigration(builder.Services);

Console.WriteLine("Migrations Applied. Console App Running...");

await builder.RunAsync();

void ApplyMigration(IServiceProvider services)
{
    using var scope = services.CreateScope();
    var serviceProvider = scope.ServiceProvider;

    var applicationDbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
    if (applicationDbContext.Database.GetPendingMigrations().Any())
    {
        applicationDbContext.Database.Migrate();
        Console.WriteLine("ApplicationDbContext Migrations Applied.");
    }


    var systemDbContext = serviceProvider.GetRequiredService<SystemDbContext>();
    if (systemDbContext.Database.GetPendingMigrations().Any())
    {
        systemDbContext.Database.Migrate();
        Console.WriteLine("SystemDbContext Migrations Applied.");
    }
}
