namespace AuthService.Application.DTOs.Email;
public class EmailResponseDto
{
    public bool Succes { get; set; }
    public string Message { get; set; } = string.Empty;
}