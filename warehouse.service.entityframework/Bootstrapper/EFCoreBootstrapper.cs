using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using warehouse.service.entityframework.Context;

namespace warehouse.service.entityframework.Bootstrapper;

public static class EFCoreBootstrapper
{
    public static IServiceCollection AddEntityFramework(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<WarehouseContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("SQLServerConnection"));            
        });

        return services;
    }

    public static void ApplyMigration(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<WarehouseContext>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<WarehouseContext>>();
        try
        {
            logger.LogInformation("Applying migration");
            context.Database.Migrate();
            logger.LogInformation("Migration applied");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while migrating the database");
            Console.WriteLine(ex.Message);
        }



    }
}
