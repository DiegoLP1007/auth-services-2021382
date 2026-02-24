using System.ComponentModel.DataAnnotations;
 
namespace AuthService.Application.Dtos.Email;
 
public class ResetPasswordDto
{
    [Required(ErrorMessage = "Email es obligatorio.")]
 
    public string Email { get; set; } = string.Empty;
    [Required(ErrorMessage = "Token es obligatorio.")]
 
    public string ResetToken { get; set; } = string.Empty;
    [Required(ErrorMessage = "Nueva contraseña es obligatoria.")]
    [MinLength(8, ErrorMessage = "La nueva contraseña debe tener al menos 8 caracteres.")]
   
    public string NewPassword { get; set; } = string.Empty;
}