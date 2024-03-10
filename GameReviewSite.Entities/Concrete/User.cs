using GameReviewSite.Entities.Abstract;

namespace GameReviewSite.Entities.Concrete
{
    public class User : AuditEntity
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }

        public virtual ICollection<UserRating> Ratings { get; set; }
        public virtual ICollection<GameComment> Comments { get; set; }
    }
}