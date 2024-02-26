using GameReviewSite.Entities.Abstract;

namespace GameReviewSite.Entities.Concrete
{
    public class GameWebsite : AEntity
    {
        public string Name { get; set; }
        public string Url { get; set; }

        public virtual ICollection<GamePrice> Prices { get; set; }
    }
}
