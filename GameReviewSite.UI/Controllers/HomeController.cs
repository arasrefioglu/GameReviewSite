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
            return View();
        }


    }
}
