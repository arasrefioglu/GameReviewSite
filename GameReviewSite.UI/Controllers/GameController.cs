using GameReviewSite.BL.Abstract;
using GameReviewSite.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;


[Authorize]
public class GameController : Controller
{
    private readonly IGameService _gameService;

    public GameController(IGameService gameService)
    {
        _gameService = gameService;
    }

    // Oyun listesini gösterir
    public async Task<IActionResult> Index()
    {
        var games = await _gameService.GetGamesAsync();
        return View(games);
    }

    // Oyun detaylarını gösterir
    public async Task<IActionResult> Details(int id)
    {
        var game = await _gameService.GetGameByIdAsync(id);
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
        var gameComment = new GameComment { GameId = gameId, Comment = comment, UserId = userId }; // UserId artık string olarak kullanılıyor.
        await _gameService.AddCommentAsync(gameComment);
        return RedirectToAction("Details", new { id = gameId });
    }


    // Oyuna puan ekler
    [HttpPost]
    public async Task<IActionResult> AddRating(int gameId, int rating)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var userRating = new UserRating { GameId = gameId, Rating = rating, UserId = userId }; // UserId artık string türünde olduğu için int dönüşümüne gerek yok.
        await _gameService.AddRatingAsync(userRating);
        return RedirectToAction("Details", new { id = gameId });
    }


    // Yeni oyun ekleme sayfasını gösterir
    [Authorize(Roles = "Admin")]
    public IActionResult Add()
    {
        return View();
    }

    // Yeni oyun ekler
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Add(Game game)
    {
        if (!ModelState.IsValid)
        {
            return View(game);
        }

        await _gameService.AddGameAsync(game);
        return RedirectToAction(nameof(Index));
    }

    // Oyun düzenleme sayfasını gösterir
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(int id)
    {
        var game = await _gameService.GetGameByIdAsync(id);
        if (game == null)
        {
            return NotFound();
        }
        return View(game);
    }

    // Oyun düzenler
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Edit(Game game)
    {
        if (!ModelState.IsValid)
        {
            return View(game);
        }

        await _gameService.UpdateGameAsync(game);
        return RedirectToAction(nameof(Index));
    }

    // Oyun siler
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var game = await _gameService.GetGameByIdAsync(id);
        if (game == null)
        {
            return NotFound();
        }

        await _gameService.DeleteGameAsync(id);
        return RedirectToAction(nameof(Index));
    }

}
