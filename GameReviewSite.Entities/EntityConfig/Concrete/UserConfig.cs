using GameReviewSite.Entites.Entityconfig.Abstract;
using GameReviewSite.Entities.Concrete;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameReviewSite.Entities.EntityConfig.Concrete
{
    public class UserConfig : AuditConfig<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);

            builder.Property(p => p.Username).HasMaxLength(50);
        }
    }
}
