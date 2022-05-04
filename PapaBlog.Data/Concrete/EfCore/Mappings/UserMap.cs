using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PapaBlog.Entities.Concrete;
using System;
using System.Text;

namespace PapaBlog.Data.Concrete.EfCore.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Email).HasMaxLength(50).IsRequired();
            builder.HasIndex(x => x.Email).IsUnique();
            builder.Property(x => x.UserName).HasMaxLength(50).IsRequired();
            builder.HasIndex(x => x.UserName).IsUnique();
            builder.Property(x => x.PasswordHash).HasColumnType("varbinary(500)").IsRequired();
            builder.Property(x => x.Description).HasMaxLength(500);
            builder.Property(x => x.FirstName).HasMaxLength(50).IsRequired();
            builder.Property(x => x.LastName).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Picture).HasMaxLength(250);
            builder.Property(x => x.CreatedByName).HasMaxLength(50).IsRequired();
            builder.Property(x => x.ModifiedByName).HasMaxLength(50).IsRequired();
            builder.Property(x => x.CreatedDate).IsRequired();
            builder.Property(x => x.ModifiedDate).IsRequired();
            builder.Property(x => x.IsActive).IsRequired();
            builder.Property(x => x.IsDeleted).IsRequired();
            builder.Property(x => x.Note).HasMaxLength(500);

            builder.HasOne<Role>(x => x.Role).WithMany(x => x.Users).HasForeignKey(x => x.RoleId);

            builder.HasData(new User
            {
                Id = 1,
                RoleId = 1,
                FirstName = "Tanju",
                LastName = "BO",
                UserName = "tanjubo",
                Email = "tanjubo@gmail.com",
                IsActive = true,
                IsDeleted = false,
                CreatedByName = "Admin",
                CreatedDate = DateTime.Now,
                ModifiedByName = "Admin",
                ModifiedDate = DateTime.Now,
                Description = "Ilk admin kullanıcısı",
                Note = "Admin kullanıcısı",
                Picture = "https://www.gravatar.com/avatar/205e460b479e2e5b48aec07710c08d50?s=200&r=pg&d=mm",
                PasswordHash = Encoding.ASCII.GetBytes("0192023a7bbd73250516f069df18b500")//admin123
            });
        }
    }
}
