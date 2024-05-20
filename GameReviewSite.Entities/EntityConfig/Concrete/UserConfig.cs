using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GameReviewSite.Entities.Concrete;

namespace GameReviewSite.Entities.EntityConfig.Concrete
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {

            builder.Property(u => u.IsAdmin);  
            builder.HasMany(u => u.Ratings).WithOne(r => r.User).HasForeignKey(r => r.UserId);
            builder.HasMany(u => u.Comments).WithOne(c => c.User).HasForeignKey(c => c.UserId);
        }
    }
}
