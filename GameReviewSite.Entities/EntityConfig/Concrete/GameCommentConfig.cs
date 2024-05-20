using GameReviewSite.Entites.Entityconfig.Abstract;
using GameReviewSite.Entities.Concrete;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameReviewSite.Entities.EntityConfig.Concrete
{
    public class GameCommentConfig : AuditConfig<GameComment>
    {
        public override void Configure(EntityTypeBuilder<GameComment> builder)
        {
            base.Configure(builder);

            builder.Property(p => p.Comment).HasMaxLength(1000);

            builder.HasIndex(p => p.GameId);
            builder.HasIndex(p => p.UserId);
        }
    }
}
