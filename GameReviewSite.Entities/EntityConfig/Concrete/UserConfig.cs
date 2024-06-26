﻿using Microsoft.AspNetCore.Identity;
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
            builder.HasMany(u => u.Ratings).WithOne(r => r.User).HasPrincipalKey(r => r.Id);
            builder.HasMany(u => u.Comments).WithOne(c => c.User).HasPrincipalKey(c => c.Id);
        }
    }
}
