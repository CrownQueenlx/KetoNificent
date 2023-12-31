using Microsoft.AspNetCore.Mvc;
using KetoNificent.Models.User;
using KetoNificent.Services.User;

namespace KetoNificent.WebMVC.Controllers;

public class AccountController : Controller
{
    private readonly IUserService _userService;
    public AccountController(IUserService userService)
    {
        _userService = userService;
    }
    
    // Get Action for Register -> Returns the view to the user
    public IActionResult Register()
    {
        return View();
    }

    // Post Action for Register -> Whent the sure submits their data from the view
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(UserRegister model)
    {
        // First validate the request model, reject if invalid
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        // Try to register the user, reject if failed (example if name or email already exist)
        var registerResult = await _userService.RegisterUserAsync(model);
        if (registerResult != false)
        {
            //  Add error to page
            TempData["ErrorMsg"] = $"User cannot be registered as typed, please try again";
            return RedirectToAction("Register", model);
        }

        // Login the new user, redirect to home after
        UserLogin loginModel = new()
        {
            UserName = model.UserName,
            Password = model.Password
        };

        var regrResult = await _userService.LoginAsync(loginModel);
        return RedirectToAction("Index", "Home");
    }

    // Get Login
    public IActionResult Login()
    {
        return View();
    }

    // Post Login
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(UserLogin model)
    {
        var loginResult = await _userService.LoginAsync(model);
        if (loginResult == false)
        {
            // TODO: Add invalid password/username message
            return View(model);
        }
        return RedirectToAction("Index", "Home");
    }

    public async Task<IActionResult> Logout()
    {
        await _userService.LogoutAsync();
        return RedirectToAction("Index", "Home");
    }
}