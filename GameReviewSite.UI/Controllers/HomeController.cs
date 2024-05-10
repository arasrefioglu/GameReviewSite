using GameReviewSite.BL.Abstract;
using GameReviewSite.Entities.Concrete;
using GameReviewSite.UI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace GameReviewSite.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGameService _gameService;

        public HomeController(IGameService gameService)
        {
            _gameService = gameService;
        }

        public async Task<IActionResult> Index()
        {
            var popularGames = await _gameService.GetPopularGamesAsync();
            var recentGames = await _gameService.GetRecentGamesAsync();
            ViewBag.PopularGames = popularGames;
            ViewBag.RecentGames = recentGames;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var errorViewModel = new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(errorViewModel);
        }
    }
}
