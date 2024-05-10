using GameReviewSite.BL.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize(Roles = "User")]
public class UserController : Controller
{
    private readonly IGameService _gameService;

    public UserController(IGameService gameService)
    {
        _gameService = gameService;
    }

    // Kullanıcıya ait oyun listesini gösterir
    public async Task<IActionResult> Index()
    {
        var games = await _gameService.GetGamesAsync();
        return View(games);
    }

    // Burada kullanıcıya özel daha fazla metod ekleyebilirsiniz (örn. Kullanıcı profili, kullanıcı yorumları vs.)
}
