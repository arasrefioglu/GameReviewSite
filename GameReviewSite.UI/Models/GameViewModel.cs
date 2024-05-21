using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace GameReviewSite.UI.Models
{
    public class GameViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int GameGenreId { get; set; }

        [Required]
        public int ReleaseYear { get; set; }

        [Required]
        public IFormFile ImageFile { get; set; }
    }
}
