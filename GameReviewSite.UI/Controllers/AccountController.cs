using GameReviewSite.DAL.Context;
using GameReviewSite.Entities.Concrete;
using GameReviewSite.UI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Policy;

public class AccountController : Controller
{
    private readonly SignInManager<User> _signInManager;
    private readonly IRepository<User> _userRepository;
    public AccountController(SignInManager<User> signInManager, IRepository<User> userRepository)
    {
        _signInManager = signInManager;
        _userRepository = userRepository;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
    {
        if (ModelState.IsValid)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                var user = (await _userRepository.FindAsync(x => x.Email == model.Email)).FirstOrDefault();

                var claims = new List<Claim>
                {
                    new("userId", user.Id.ToString()),
                };

                await _signInManager.SignInWithClaimsAsync(user, model.RememberMe, claims);


                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }
        }
        return View(model);
    }

    [HttpGet]
    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}
