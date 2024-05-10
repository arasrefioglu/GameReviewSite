using GameReviewSite.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameReviewSite.Entities.Concrete
{


    public class User : IdentityUser
    {
        public bool IsAdmin { get; set; }
        public virtual ICollection<UserRating> Ratings { get; set; }
        public virtual ICollection<GameComment> Comments { get; set; }

        [NotMapped] // EF Core bu özelliği veritabanına eklemez
        public string Password { get; set; }
    }


}
