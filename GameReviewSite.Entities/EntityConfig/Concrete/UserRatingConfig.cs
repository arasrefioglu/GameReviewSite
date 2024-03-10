using GameReviewSite.Entites.Entityconfig.Abstract;
using GameReviewSite.Entities.Concrete;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameReviewSite.Entities.EntityConfig.Concrete
{
    public class UserRatingConfig : AuditConfig<UserRating>
    {
        public override void Configure(EntityTypeBuilder<UserRating> builder)
        {
            base.Configure(builder);
            builder.HasIndex(p => p.GameId).IsUnique();
            builder.HasIndex(p => p.UserId).IsUnique();
        }
    }
}
