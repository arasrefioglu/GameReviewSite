using GameReviewSite.Entities.Abstract;

namespace GameReviewSite.Entities.Concrete
{
    public class GameGenre : AuditEntity
    {
        public string Name { get; set; }

        public virtual ICollection<Game> Games { get; set; }
    }
}
