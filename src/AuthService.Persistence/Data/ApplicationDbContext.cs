using AuthService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Persistence.Data;
public class ApplicationDbContext : DbContext
{
    // MÉTODO CONSTRUCTOR
   public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : base(options)
{
}
 
   public DbSet<User> Users { get; set; }
public DbSet<UserProfile> UserProfiles { get; set; }
public DbSet<Role> Roles { get; set; }
public DbSet<UserRole> UserRoles { get; set; }
public DbSet<UserEmail> UserEmails { get; set; }
public DbSet<UserPasswordReset> UserPasswordResets { get; set; }
 
    protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);
    
    foreach (var entity in modelBuilder.Model.GetEntityTypes())
    {
        var tableName = entity.GetTableName();
        if (!string.IsNullOrEmpty(tableName))
        {
            entity.SetTableName(ToSnakeCase(tableName));
        }
        
        foreach (var property in entity.GetProperties())
        {
            var columnName = property.GetColumnName();
            if (!string.IsNullOrEmpty(columnName))
            {
                property.SetColumnName(ToSnakeCase(columnName));
            }
     }
}

modelBuilder.Entity<User>(entity =>
        {
            // Llave primaria
            entity.HasKey(e => e.Id);

            // Índices únicos para búsquedas rápidas y evitar duplicados
            entity.HasIndex(e => e.Email).IsUnique();
            entity.HasIndex(e => e.Username).IsUnique();

            // Relación 1:1 con UserProfile
            entity.HasOne(e => e.UserProfile)
                .WithOne(p => p.User)
                .HasForeignKey<UserProfile>(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Si se borra el User, se borra su Profile

            // Relación 1:N con UserRoles (un usuario puede tener varios roles)
            entity.HasMany(e => e.UserRoles)
                .WithOne(ur => ur.User)
                .HasForeignKey(ur => ur.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relación 1:1 con UserEmail
            entity.HasOne(e => e.UserEmail)
                .WithOne(ue => ue.User)
                .HasForeignKey<UserEmail>(ue => ue.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relación 1:1 con UserPasswordReset
            entity.HasOne(e => e.UserPasswordReset)
                .WithOne(upr => upr.User)
                .HasForeignKey<UserPasswordReset>(upr => upr.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Configuración para UserRole (tabla intermedia muchos a muchos)
modelBuilder.Entity<UserRole>(entity =>
{
    entity.HasKey(e => e.Id);
		// Un usuario no puede tener el mismo rol asignado dos veces
    entity.HasIndex(e => new { e.UserId, e.RoleId }).IsUnique();
});

modelBuilder.Entity<Role>(entity =>
{
    entity.HasKey(e => e.Id);
    // Los nombres de rol deben ser únicos
    entity.HasIndex(e => e.Name).IsUnique();
});

    /// Ejemplo: "UserProfile" → "user_profile"
    private static string ToSnakeCase(string input)
    {
        if (string.IsNullOrEmpty(input))
            return input;

        return string.Concat(
            input.Select((x, i) => i > 0 && char.IsUpper(x) 
                ? "_" + x.ToString().ToLower() 
                : x.ToString().ToLower())
        );
    }
}
}