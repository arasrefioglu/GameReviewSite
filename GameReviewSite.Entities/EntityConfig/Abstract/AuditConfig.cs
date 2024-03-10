using GameReviewSite.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameReviewSite.Entites.Entityconfig.Abstract
{
    public abstract class AuditConfig<T> : IEntityTypeConfiguration<T> where T : AuditEntity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(p => p.Id);
        }
    }
}


