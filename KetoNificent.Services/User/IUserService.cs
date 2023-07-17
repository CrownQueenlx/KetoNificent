using KetoNificent.Data.Entities;
using KetoNificent.Models.User;

namespace KetoNificent.Services.User;

public interface IUserService
{
    // Post
    Task<bool> RegisterUserAsync(UserRegister model);
    // Get
    Task<UserDetail> GetUserByIdAsync(int userId);
    Task<bool> LoginAsync(UserLogin model);
    Task LogoutAsync();
    // Put
    Task<UserEntity> UpdateUserByIdAsync(int userId);
    // Delete
    Task<bool> DeleteUserAsync();
}