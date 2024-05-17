using GameReviewSite.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

public class DebugController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;

    public DebugController(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IActionResult> TestHash()
    {
        var user = new User { UserName = "userxx@user.com", Email = "userxx@user.com" };
        var password = "Q1w2e3r4.";
        var result = await _userManager.CreateAsync(user, password);

        if (result.Succeeded)
        {
            return Content("Hash: " + user.PasswordHash);
        }

        return Content("Failed to create user");
    }
}
