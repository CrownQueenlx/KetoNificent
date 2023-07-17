using KetoNificent.Data;
using KetoNificent.Data.Entities;
using KetoNificent.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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

    public async Task<bool> LoginAsync(UserLogin model)
    {
        // verifies the user exists by the username
        var userEntity = await _userManager.FindByNameAsync(model.UserName);
        if (userEntity is null)
            return false;

            // verifies the correct password was given
            var passwordHasher = new PasswordHasher<UserEntity>();
            var verifyPasswordResult = passwordHasher.VerifyHashedPassword(userEntity, userEntity.Password, model.Password);
            if (verifyPasswordResult == PasswordVerificationResult.Failed)
            return false;

            // finally, since user exists and password passes verification, sign in the user
            await _signInManager.SignInAsync(userEntity, true);
            return true;
    }

    public async Task LogoutAsync()
    {
        await _signInManager.SignOutAsync();
    }

    public async Task<bool> RegisterUserAsync(UserRegister model)
    {
        if (await GetUserByEmailAsync(model.Email) != null || await GetUserByUsernameAsync(model.Username) != null)
        return false;
        
        UserEntity entity = new()
        { 
            Email = model.Email,
            UserName = model.Username,
            DateCreated = DateTime.Now
        };

        var  passwordHasher = new PasswordHasher<UserEntity>();
        entity.Password = passwordHasher.HashPassword(entity, model.Password);

        var createResult = await _userManager.CreateAsync(entity);
        return createResult.Succeeded; 
        //abbreviation for the await _context.saveChangesAsync section
    }

    public async Task<bool> UpdateUserByIdAsync(UserDetail request)
    {
       var userEntity = await _context.Users.FindAsync(request.UserId);
       if (userEntity is null)
       return false;

        // update entity's properties
        userEntity.Name = request.Name;
        userEntity.Password = request.Password;

        var createResult = await _userManager.CreateAsync(userEntity);
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
        return await _context.Users.FirstOrDefaultAsync(user => user.Email.ToLower() == email.ToLower());
    }
    private async Task<UserEntity?> GetUserByUsernameAsync(string username)
    {
        return await _context.Users.FirstOrDefaultAsync(user => user.UserName.ToLower() == username.ToLower());
    }
}