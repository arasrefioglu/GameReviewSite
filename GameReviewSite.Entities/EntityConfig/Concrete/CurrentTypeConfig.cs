using GameReviewSite.Entites.Entityconfig.Abstract;
using GameReviewSite.Entities.Concrete;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameReviewSite.Entities.EntityConfig.Concrete
{
    public class CurrencyTypeConfig : AuditConfig<CurrencyType>
    {
        public override void Configure(EntityTypeBuilder<CurrencyType> builder)
        {
            base.Configure(builder);

            builder.Property(p => p.Name).HasMaxLength(50);
            builder.Property(p => p.Symbol).HasMaxLength(1);
        }
    }
}


