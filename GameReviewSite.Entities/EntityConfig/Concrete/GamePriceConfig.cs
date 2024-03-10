using GameReviewSite.Entites.Entityconfig.Abstract;
using GameReviewSite.Entities.Concrete;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameReviewSite.Entities.EntityConfig.Concrete
{
    public class GamePriceConfig : AuditConfig<GamePrice>
    {
        public override void Configure(EntityTypeBuilder<GamePrice> builder)
        {
            base.Configure(builder);

            builder.HasIndex(p => p.GameId).IsUnique();
            builder.HasIndex(p => p.CurrencyTypeId).IsUnique();
            builder.HasIndex(p => p.GameWebsiteId).IsUnique();

        }
    }
}
