using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;


public static class PersistanceServiceRegistration
{
    public static IServiceCollection AddPersistanceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<CleanDatabaseContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("CleanDatabaseConnection")));
        return services;
    }
}