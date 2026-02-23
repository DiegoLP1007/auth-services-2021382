using System.ComponentModel.DataAnnotations;

namespace AuthService.Domain.Entitis;

public class Role
{
    [Key]
    [MaxLength(16)]
    public string Id { get; set; }

    [Required(ErrorMessage = "El nombre del rol es obligatorio.")]
    [MaxLength(100, ErrorMessage = "El nombre del rol no puede exceder los 100 caracteres.")]
    public string Name { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Relaciones con UserRole
    public ICollection<UserRole> UserRoles { get; set; }

}
