using AuthService.Domain.Entitis;

namespace AuthService.Domain.Interfaces;

public interface IUserRepository
{

    // Metodos de consulta
    Task<User?> GetByIdAsync(string id);
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByUserName(string username);
    Task<User?> GetByPasswordResetTokenAsync(string token);
    Task<User?> CreateAsync(User user);
    Task<User?> UpdateAsync(User user);
    Task<User?> DeleteAsync(User user);

    Task<bool> ExistsByEmailAsync(string email);
    Task<bool> ExistsByUsernameAsync(string username);
    Task UdateUserRoleAsync(string userId, string roleId);









}