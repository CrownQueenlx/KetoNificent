using KetoNificent.Data.Entities;
using KetoNificent.Models.User;

namespace KetoNificent.Services.User;

public interface IUserService
{
    Task<bool> LoginAsync(UserLogin model);
    Task LogoutAsync();
    // Post
    Task<bool> RegisterUserAsync(UserRegister model);
    // Get methods are private in the UserService
    
    // Put
    Task<bool> UpdateUserByIdAsync(UserDetail request);
    // Delete
    Task<bool> DeleteUserAsync(int id);
}