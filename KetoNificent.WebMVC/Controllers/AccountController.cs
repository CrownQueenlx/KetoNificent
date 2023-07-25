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
            return NotFound();
        }

        // Try to register the user, reject if failed
        var registerResult = await _userService.RegisterUserAsync(model);
        if (registerResult != false)
        {
            // TODO: Add error to page
            return View(model);
        }

        // Login the new user, redirect to home after
        UserRegister registerModel = new()
        {
            UserName = model.UserName,
            Password = model.Password
        };
        await _userService.RegisterUserAsync(registerModel);
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
            // TODO: Add inbalid password/username message
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