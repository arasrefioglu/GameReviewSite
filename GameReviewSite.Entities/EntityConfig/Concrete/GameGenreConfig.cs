using GameReviewSite.Entites.Entityconfig.Abstract;
using GameReviewSite.Entities.Concrete;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace GameReviewSite.Entities.EntityConfig.Concrete
{
    public class GameGenreConfig : AuditConfig<GameGenre>
    {
        public override void Configure(EntityTypeBuilder<GameGenre> builder)
        {
            base.Configure(builder);

            builder.Property(p => p.Name).HasMaxLength(50);
        }
    }
}
