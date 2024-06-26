﻿using GameReviewSite.BL.Abstract;
using GameReviewSite.Entities.Concrete;
using GameReviewSite.UI.Models;
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


    public async Task<IActionResult> ViewGames()
    {
        var games = await _gameService.GetGamesAsync();
        var isAdmin = User.Identity.IsAuthenticated && User.Identity.Name == "admin";
        ViewBag.IsAdmin = isAdmin;
        return View(games);
    }


    public async Task<IActionResult> Details(int id)
    {
        var game = await _gameService.GetGameDetailsAsync(id);
        if (game == null)
        {
            return NotFound();
        }
        return View(game);
    }


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

  
    [HttpGet]
    public async Task<IActionResult> Add()
    {
        var isAdmin = User.Identity.IsAuthenticated && User.Identity.Name == "admin";
        if (!isAdmin)
        {
            return RedirectToAction("AccessDenied", "Account");
        }

        var genres = await _gameService.GetGenresAsync();
        ViewBag.Genres = new SelectList(genres, "Id", "Name");

        return View();
    }


    [HttpPost]
    public async Task<IActionResult> Add(GameViewModel model)
    {
        var isAdmin = User.Identity.IsAuthenticated && User.Identity.Name == "admin";
        if (!isAdmin)
        {
            return RedirectToAction("AccessDenied", "Account");
        }

        if (!ModelState.IsValid)
        {
            var genres = await _gameService.GetGenresAsync();
            ViewBag.Genres = new SelectList(genres, "Id", "Name");
            return View(model);
        }

        var game = new Game
        {
            Name = model.Name,
            GameGenreId = model.GameGenreId,
            ReleaseYear = model.ReleaseYear,
        };

        if (model.ImageFile != null)
        {

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", model.ImageFile.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await model.ImageFile.CopyToAsync(stream);
            }
            game.Image = "/images/" + model.ImageFile.FileName;
        }

        await _gameService.AddGameAsync(game);
        return RedirectToAction(nameof(ViewGames));
    }


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
