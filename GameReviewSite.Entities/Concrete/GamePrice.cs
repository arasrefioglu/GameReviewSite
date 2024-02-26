using GameReviewSite.Entities.Abstract;

namespace GameReviewSite.Entities.Concrete

{
    public class GamePrice : AEntity
    {
        public int GameId { get; set; }

        public virtual Game Game { get; set; }
        public int GameWebsiteId { get; set; }

        public virtual GameWebsite Website { get; set; }
        public decimal Price { get; set; }
        public int CurrencyTypeId { get; set; }

        public virtual CurrencyType Currency { get; set; }
    }
}
