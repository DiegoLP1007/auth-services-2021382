using System.ComponentModel.DataAnnotations;
namespace AuthService.Domain.Entitis;
public class UserEmail
{   
    [Key]
    [MaxLength(16)]
    public string Id { get; set; }

    [Required]
    [MaxLength(16)]
    public string UserId { get; set; }

    [Required]
    public bool EmailVerified { get; set; }

    [MaxLength(256)]
    public string? EmailVerificationToken { get; set; }

    public DateTime? EmailVerificationTokenExpiry { get; set; }

    [Required]
    public User User { get; set; } = null!;
}