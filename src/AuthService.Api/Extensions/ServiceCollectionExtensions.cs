using AuthService.Domain.Entitis;
using AuthService.Persistence.Data;
using AuthService.Domain.Constants;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection service,
IConfiguration configuration)
{
    services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(configuration.GetConnectionString("DefaultConnection)")
    .UseSnakeCaseNamingConvention()));

    services.AddHealthChecks();

    return services;
}
}