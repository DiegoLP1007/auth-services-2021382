using AuthService.Domain.Entitis;
using AuthService.Domain.Interfaces;
using AuthService.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using AuthService.Application.Services;
 
namespace AuthService.Persistence.Repositories;
 
public class UserRepository(ApplicationDbContext context) : IUserRepository
{ 
    public async Task<User?> GetByIdAsync(string id)
    {
        //Sirve para cargar todos los datos relacionados con el usuario
       var user = await context.Users
       .Include(u => u.UserProfile)
       .Include(u => u.UserEmail)
       .Include(u => u.UserRoles)
       .Include(u => u.UserPasswordReset)
       .FirstOrDefaultAsync(u => u.Id == id);
 
       return user ?? throw new InvalidOperationException($"Usuario con id {id} no encontrado");
    }
 
    public async Task<User?> GetByEmailAsync(string email)
    {
        //Sirve para cargar todos los datos relacionados con el usuario
       var user = await context.Users
       .Include(u => u.UserProfile)
       .Include(u => u.UserEmail)
       .Include(u => u.UserRoles)
       .Include(u => u.UserPasswordReset)
       .FirstOrDefaultAsync(u => EF.Functions.Like(u.UserEmail.Email, email));
 
       return user ?? throw new InvalidOperationException($"Usuario con email {email} no encontrado");
    }
 
    public async Task<User?> GetByUserNameAsync(string username)
    {
        //Sirve para cargar todos los datos relacionados con el usuario
       var user = await context.Users
       .Include(u => u.UserProfile)
       .Include(u => u.UserEmail)
       .Include(u => u.UserRoles)
       .Include(u => u.UserPasswordReset)
       .FirstOrDefaultAsync(u => EF.Functions.Like(u.UserProfile.username, username));
 
       return user ?? throw new InvalidOperationException($"Usuario con username {username} no encontrado");
    }
    public async Task<User?> GetByEmailVerificationTokenAsync(string token)
    {
        var user = await context.Users
       .Include(u => u.UserProfile)
       .Include(u => u.UserEmail)
       .Include(u => u.UserRoles)
       .Include(u => u.UserPasswordReset)
       .FirstOrDefaultAsync(u => u.UserEmail != null && u.UserEmail.VerificationToken == token);
 
       return user;
    }
 
    public async Task<User?> GetByPasswordResetTokenAsync(string token)
    {
        var user = await context.Users
       .Include(u => u.UserProfile)
       .Include(u => u.UserEmail)
       .Include(u => u.UserRoles)
       .Include(u => u.UserPasswordReset)
       .FirstOrDefaultAsync(u => u.UserPasswordReset != null && u.UserPasswordReset.Token == token);
 
       return user;
    }
    public async Task<User> CreateAsync(User user)
    {
        context.Users.Add(user);
        await context.SaveChangesAsync();
        return await GetByIdAsync(user.Id);
    }
 
    public async Task<User> UpdateAsync(User user)
    {
        await context.SaveChangesAsync();
        return await GetByIdAsync(user.Id);
    }
 
    public async Task<User?> DeleteAsync(User user)
    {
        context.Users.Remove(user);
        await context.SaveChangesAsync();
        return user;
    }
 
    public async Task<bool> ExistsByEmailAsync(string email)
    {
        return await context.Users
       .AnyAsync(EF.Functions.Like(u.UserEmail.Email, email));
    }
 
    public async Task<bool> ExistsByUsernameAsync(string username)
    {
        return await context.Users
       .AnyAsync(EF.Functions.Like(u.UserProfile.Username, username));
    }
    public async Task UpdateUserRoleAsync(string userId, string roleId)
    {
        var existingRoles = await context.UserRoles
       .Where(ur => ur.UserId == userId)
       .ToListAsync();
       
       context.UserRoles.RemoveRange(existingRoles);
       
       var newUserRole = new UserRole
       {
        Id = UuidGenerator.GenerateUserId(),
        UserId = userId,
        RoleId = roleId,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
       };
       context.UserRoles.Add(newUserRole);
       await context.SaveChangesAsync();
    }
}