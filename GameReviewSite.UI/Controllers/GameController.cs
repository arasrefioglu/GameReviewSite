using GameReviewSite.BL.Abstract;
using GameReviewSite.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        var isAdmin = User.Identity.IsAuthenticated && User.Identity.Name == "admin";
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
        var userId = User.FindFirstValue("userId");
        var gameComment = new GameComment
        {
            GameId = gameId,
            Comment = comment,
            UserId = Guid.Parse(userId)
        };
        await _gameService.AddCommentAsync(gameComment);
        return RedirectToAction("Details", new { id = gameId });
    }

    // Oyuna puan ekler
    [HttpPost]
    public async Task<IActionResult> AddRating(int gameId, int rating)
    {
        var userId = User.FindFirstValue("userId");
        var userRating = new UserRating
        {
            GameId = gameId,
            Rating = rating,
            UserId = Guid.Parse(userId)
        };
        await _gameService.AddRatingAsync(userRating);
        return RedirectToAction("Details", new { id = gameId });
    }

    // Yeni oyun ekleme sayfasını gösterir
    [HttpGet]
    public IActionResult Add()
    {
        var isAdmin = User.Identity.IsAuthenticated && User.Identity.Name == "admin";
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
        var isAdmin = User.Identity.IsAuthenticated && User.Identity.Name == "admin";
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

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var isAdmin = User.Identity.IsAuthenticated && User.Identity.Name == "admin";
        if (!isAdmin)
        {
            return RedirectToAction("AccessDenied", "Account");
        }

        var game = await _gameService.GetGameByIdAsync(id);
        if (game == null)
        {
            return NotFound();
        }

        // Load genres for the dropdown
        var genres = await _gameService.GetGenresAsync();
        ViewBag.Genres = new SelectList(genres, "Id", "Name");

        return View(game);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Game game, IFormFile imageFile)
    {
        var isAdmin = User.Identity.IsAuthenticated && User.Identity.Name == "admin";
        if (!isAdmin)
        {
            return RedirectToAction("AccessDenied", "Account");
        }

        var existingGame = await _gameService.GetGameByIdAsync(game.Id);
        if (existingGame == null)
        {
            return NotFound();
        }

        existingGame.Name = game.Name;
        existingGame.GameGenreId = game.GameGenreId;
        existingGame.ReleaseYear = game.ReleaseYear;

        if (imageFile != null)
        {
            // Save the new image file and update the Image property
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", imageFile.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }
            existingGame.Image = "/images/" + imageFile.FileName;
        }

        await _gameService.UpdateGameAsync(existingGame);
        return RedirectToAction(nameof(ViewGames));
    }


    // Oyun silme sayfasını gösterir
    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var isAdmin = User.Identity.IsAuthenticated && User.Identity.Name == "admin";
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
