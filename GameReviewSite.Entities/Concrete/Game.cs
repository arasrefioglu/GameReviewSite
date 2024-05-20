using GameReviewSite.Entities.Abstract;

namespace GameReviewSite.Entities.Concrete
{
    public class Game : AuditEntity
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public int ReleaseYear { get; set; }
        public double EditorRating { get; set; }

        public int GameGenreId { get; set; }

        public virtual GameGenre Genre { get; set; }

        public virtual ICollection<UserRating>? Ratings { get; set; }
        public virtual ICollection<GameComment>? Comments { get; set; }

    }
}
