using AuthService.Api.Extensions;
using AuthService.Persistence.Data;

// Agrega antes de var app =builder.Build()

// CONFIGURACIÓN DE SERVICIOS POR MEDIO DE MÉTODOS DE EXTENSIÓN
builder.Services.AddApplicationServices(builder.Configuration);

// ---------------------------------------------------------------------
// Agrega antes de app.run()
// Initialize database and seed data
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

    try
    {
        logger.LogInformation("Checking database connection...");

        // Ensure database is created (similar to Sequelize sync in Node.js)
        await context.Database.EnsureCreatedAsync();

        logger.LogInformation("Database ready. Running seed data...");
        await DataSeeder.SeedAsync(context);

        logger.LogInformation("Database initialization completed successfully");
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An error occurred while initializing the database");
        throw; // Re-throw to stop the application
    }
    }