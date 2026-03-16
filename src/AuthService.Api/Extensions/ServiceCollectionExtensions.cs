using AuthService.Persistence.Data;
using Microsoft.EntityFrameworkCore;

public static class ServiceCollectionExtensions
{
       public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Configure PostgreSQL database
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
                   .UseSnakeCaseNamingConvention());

				// Aquí también se registran los repositorios
        // services.AddScoped<IUserRepository, UserRepository>();
        
        // Configure health checks
        services.AddHealthChecks();public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
{
    services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
                   .UseSnakeCaseNamingConvention());

    services.AddHealthChecks();

    return services;
}

        return services;
    }
}