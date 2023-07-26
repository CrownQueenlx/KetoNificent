using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using KetoNificent.Data;
using KetoNificent.Data.Entities;
using KetoNificent.Models.User;

namespace KetoNificent.Services.User;

public class UserService : IUserService
{
    private readonly AppDbContext _context;
    private readonly UserManager<UserEntity> _userManager;
    private readonly SignInManager<UserEntity> _signInManager;

    public UserService(
        AppDbContext context,
        UserManager<UserEntity> userManager,
        SignInManager<UserEntity> signInManager)
    {
        _context = context;
        _userManager = userManager;
        _signInManager = signInManager;
    }


    public async Task<bool> RegisterUserAsync(UserRegister model)
    {
        //check if user exists so they will not be registered twice
        if (await UserExistsAsync(model.Email, model.UserName))
            return false;

        UserEntity entity = new()
        {
            UserName = model.UserName,
            Email = model.Email,
            DateCreated = DateTime.Now
        };

        var createResult = await _userManager.CreateAsync(entity, model.Password);
        return createResult.Succeeded;
        //abbreviation for the await _context.saveChangesAsync section
    }
    public async Task<bool> LoginAsync(UserLogin model)
    {
        // verifies the user exists by the username
        var userEntity = await _userManager.FindByNameAsync(model.UserName);
        if (userEntity is null)
            return false;

        // verifies the correct password was given
        var isValidPassword = await _userManager.CheckPasswordAsync(userEntity, model.Password);
        if (!isValidPassword)
            return false;

        // if password is null deny acess
        if (model.Password == null)
            return false;

        // finally, since user exists and password passes verification, sign in the user
        await _signInManager.SignInAsync(userEntity, true);
            return true;
    }

    public async Task LogoutAsync() => await _signInManager.SignOutAsync();

    public async Task<bool> UpdateUserByIdAsync(UserDetailVM request)
    {
        var userEntity = await _context.Users.FindAsync(request.UserId);
        if (userEntity is null)
            return false;

        // update entity's properties
        userEntity.Name = request.Name;

        var createResult = await _userManager.CreateAsync(userEntity, request.Password);
        return createResult.Succeeded;
    }

    public async Task<bool> DeleteUserAsync(int id)
    {
        var userEntity = await _context.Users.FindAsync(id);
        // remove the ingredient from the dbcontext and assert that one change was saved
        _context.Users.Remove(userEntity);
        return false;
    }

    private async Task<UserEntity?> GetUserByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(user => user.Email!.ToLower() == email.ToLower());
    }
    private async Task<UserEntity?> GetUserByUsernameAsync(string username)
    {
        return await _context.Users.FirstOrDefaultAsync(user => user.UserName!.ToLower() == username.ToLower());
    }
    private async Task<bool> UserExistsAsync(string email, string username)
    {
        var normalizedEmail = _userManager.NormalizeEmail(email);
        var NormalizedUsername = _userManager.NormalizeName(username);

        return await _context.Users.AnyAsync(y =>
        y.NormalizedEmail == normalizedEmail ||
        y.NormalizedUserName == NormalizedUsername
        );
    }
}