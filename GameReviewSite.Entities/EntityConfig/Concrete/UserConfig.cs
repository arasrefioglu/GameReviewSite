using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GameReviewSite.Entities.Concrete;

namespace GameReviewSite.Entities.EntityConfig.Concrete
{
    // Eğer IdentityUser içindeki özellikleri değiştirmek istemiyorsanız,
    // AuditConfig yerine EntityTypeConfiguration<User> kullanabilirsiniz.
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Base IdentityUser'ın konfigürasyonlarını kullan.
            // AuditConfig<User> burada kullanılmıyor çünkü IdentityUser zaten bir Id sahibi.

            // IdentityUser için Email ve UserName zaten var, bu yüzden yeniden konfigüre etmeye gerek yok.
            // Yalnızca ek özelliklerinizi konfigüre edin.
            builder.Property(u => u.IsAdmin);  // Eklediğiniz özel alan için configuration yapabilirsiniz.

            // Eğer IdentityUser içinde Email zaten benzersiz olarak ayarlandıysa, bu index'i eklemenize gerek yok.
            // Eğer eklemek isterseniz, IdentityUser'daki özelliğin adını kullanmalısınız. 
            // builder.HasIndex(u => u.NormalizedEmail).IsUnique(); 

            // IdentityUser, şifreyi 'PasswordHash' adı altında saklar.
            // Burada şifre için açıkça bir alan belirtmenize gerek yok.
            // Eğer Password gibi bir alanınız varsa ve bu alanı kullanmak istiyorsanız, ekleyebilirsiniz.

            // İlişkileri ve diğer konfigürasyonları ekleyin.
            // Ratings ve Comments koleksiyonları için configuration yapabilirsiniz.
            builder.HasMany(u => u.Ratings).WithOne(r => r.User).HasForeignKey(r => r.UserId);
            builder.HasMany(u => u.Comments).WithOne(c => c.User).HasForeignKey(c => c.UserId);
        }
    }
}
