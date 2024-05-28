using GameReviewSite.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameReviewSite.Entities.Concrete
{
    public class User : IdentityUser <Guid>
    {
        public bool IsAdmin { get; set; }
        public virtual ICollection<UserRating> Ratings { get; set; }
        public virtual ICollection<GameComment> Comments { get; set; }
    }
}
