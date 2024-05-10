using GameReviewSite.Entites.Entityconfig.Abstract;
using GameReviewSite.Entities.Concrete;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameReviewSite.Entities.EntityConfig.Concrete
{
    public class GameConfig : AuditConfig<Game>
    {
        public override void Configure(EntityTypeBuilder<Game> builder)
        {
            base.Configure(builder);

            builder.Property(p => p.Name).HasMaxLength(50);
            builder.HasIndex(p => p.GameGenreId).IsUnique();
        }
    }
}
