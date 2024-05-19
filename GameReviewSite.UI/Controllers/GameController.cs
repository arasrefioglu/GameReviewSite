using GameReviewSite.BL.Abstract;
using GameReviewSite.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

public class GameController : Controller
{
    private readonly IGameService _gameService;

    public GameController(IGameService gameService)
    {
        _gameService = gameService;
    }

    // Oyun listesini gösterir
    public async Task<IActionResult> ViewGames()
    {
        var games = await _gameService.GetGamesAsync();
        var isAdmin = User.Identity.IsAuthenticated && User.Identity.Name == "admin@admin.com";
        ViewBag.IsAdmin = isAdmin;
        return View(games);
    }

    // Oyun detaylarını gösterir
    public async Task<IActionResult> Details(int id)
    {
        var game = await _gameService.GetGameDetailsAsync(id);
        if (game == null)
        {
            return NotFound();
        }
        return View(game);
    }

    // Oyuna yorum ekler
    [HttpPost]
    public async Task<IActionResult> AddComment(int gameId, string comment)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var gameComment = new GameComment { GameId = gameId, Comment = comment, UserId = userId };
        await _gameService.AddCommentAsync(gameComment);
        return RedirectToAction("Details", new { id = gameId });
    }

    // Oyuna puan ekler
    [HttpPost]
    public async Task<IActionResult> AddRating(int gameId, int rating)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var userRating = new UserRating { GameId = gameId, Rating = rating, UserId = userId };
        await _gameService.AddRatingAsync(userRating);
        return RedirectToAction("Details", new { id = gameId });
    }

    // Yeni oyun ekleme sayfasını gösterir
    [HttpGet]
    public IActionResult Add()
    {
        var isAdmin = User.Identity.IsAuthenticated && User.Identity.Name == "admin@admin.com";
        if (!isAdmin)
        {
            return RedirectToAction("AccessDenied", "Account");
        }
        return View();
    }

    // Yeni oyun ekler
    [HttpPost]
    public async Task<IActionResult> Add(Game game)
    {
        var isAdmin = User.Identity.IsAuthenticated && User.Identity.Name == "admin@admin.com";
        if (!isAdmin)
        {
            return RedirectToAction("AccessDenied", "Account");
        }

        if (!ModelState.IsValid)
        {
            return View(game);
        }

        await _gameService.AddGameAsync(game);
        return RedirectToAction(nameof(ViewGames));
    }

    // Oyun düzenleme sayfasını gösterir
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var isAdmin = User.Identity.IsAuthenticated && User.Identity.Name == "admin@admin.com";
        if (!isAdmin)
        {
            return RedirectToAction("AccessDenied", "Account");
        }

        var game = await _gameService.GetGameByIdAsync(id);
        if (game == null)
        {
            return NotFound();
        }
        return View(game);
    }

    // Oyun düzenler
    [HttpPost]
    public async Task<IActionResult> Edit(Game game)
    {
        var isAdmin = User.Identity.IsAuthenticated && User.Identity.Name == "admin@admin.com";
        if (!isAdmin)
        {
            return RedirectToAction("AccessDenied", "Account");
        }

        if (!ModelState.IsValid)
        {
            return View(game);
        }

        await _gameService.UpdateGameAsync(game);
        return RedirectToAction(nameof(ViewGames));
    }

    // Oyun silme sayfasını gösterir
    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var isAdmin = User.Identity.IsAuthenticated && User.Identity.Name == "admin@admin.com";
        if (!isAdmin)
        {
            return RedirectToAction("AccessDenied", "Account");
        }

        var game = await _gameService.GetGameByIdAsync(id);
        if (game == null)
        {
            return NotFound();
        }

        await _gameService.DeleteGameAsync(id);
        return RedirectToAction(nameof(ViewGames));
    }
}
