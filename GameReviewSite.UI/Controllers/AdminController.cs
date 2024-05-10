using GameReviewSite.BL.Abstract;
using GameReviewSite.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly IGameService _gameService;
    private readonly IUserService _userService;

    public AdminController(IGameService gameService, IUserService userService)
    {
        _gameService = gameService;
        _userService = userService;
    }

    // Admin ana sayfası
    public async Task<IActionResult> Index()
    {
        var games = await _gameService.GetGamesAsync();
        return View(games);
    }

    // Kullanıcı listesini gösterir
    public async Task<IActionResult> UserList()
    {
        var users = await _userService.GetAllUsersAsync();
        return View(users);
    }

    // Yeni kullanıcı oluşturur
    [HttpGet]
    public IActionResult CreateUser()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(User user)
    {
        await _userService.AddUserAsync(user);
        return RedirectToAction("UserList");
    }

    // Kullanıcı bilgilerini günceller
    [HttpGet]
    public async Task<IActionResult> EditUser(int id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        return View(user);
    }

    [HttpPost]
    public async Task<IActionResult> EditUser(User user)
    {
        await _userService.UpdateUserAsync(user);
        return RedirectToAction("UserList");
    }

    // Kullanıcıyı siler
    [HttpPost]
    public async Task<IActionResult> DeleteUser(int id)
    {
        await _userService.DeleteUserAsync(id);
        return RedirectToAction("UserList");
    }

    // Rol atama işlevi için ekstra metotlar vs. (Bu kısım UserManager kullanımına bağlıdır ve özel bir implementasyon gerektirebilir)
}
