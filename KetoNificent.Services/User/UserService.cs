using KetoNificent.Data.Entities;
using KetoNificent.Models.User;

namespace KetoNificent.Services.User;

public class UserService : IUserService
{
    public Task<bool> RegisterUserAsync(UserRegister model)
    {
        throw new NotImplementedException();
    }

    public Task<UserDetail> GetUserByIdAsync(int userId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> LoginAsync(UserLogin model)
    {
        throw new NotImplementedException();
    }

    public Task LogoutAsync()
    {
        throw new NotImplementedException();
    }

    public Task<UserEntity> UpdateUserByIdAsync(int userId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteUserAsync()
    {
        throw new NotImplementedException();
    }
}