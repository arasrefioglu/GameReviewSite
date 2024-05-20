using GameReviewSite.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameReviewSite.Entities.Concrete
{
    public class User : IdentityUser
    {
        [NotMapped]
        public new Guid UserId { get; set; }
        public new Guid Id { get; set; }
        public bool IsAdmin { get; set; }
        public virtual ICollection<UserRating> Ratings { get; set; }
        public virtual ICollection<GameComment> Comments { get; set; }
    }
}
