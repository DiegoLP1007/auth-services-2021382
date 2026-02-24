using System.ComponentModel.DataAnnotations;
 
namespace AuthService.Application.Dtos.Email;
 
public class ForgotPasswordDto
{
    [Required(ErrorMessage = "Email es obligatorio.")]
    [EmailAddress(ErrorMessage = "Email no es válido.")]
 
    public string Email { get; set; } = string.Empty;
}