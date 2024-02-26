using GameReviewSite.Entities.Abstract;

namespace GameReviewSite.Entities.Concrete
{
    public class UserRating : AEntity
    {
        public int GameId { get; set; }

        public virtual Game Game { get; set; }
        public int UserId { get; set; }

        public virtual User User { get; set; }
        public int Rating { get; set; }

    }
}
