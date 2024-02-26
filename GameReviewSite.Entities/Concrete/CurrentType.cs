using GameReviewSite.Entities.Abstract;

namespace GameReviewSite.Entities.Concrete
{
    public class CurrencyType : AEntity
    {
        public string Name { get; set; }
        public string Symbol { get; set; }

        public virtual ICollection<GamePrice> Prices { get; set; }
    }
}
