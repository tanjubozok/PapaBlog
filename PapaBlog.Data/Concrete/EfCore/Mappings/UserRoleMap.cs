using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PapaBlog.Entities.Concrete;

namespace PapaBlog.Data.Concrete.EfCore.Mappings
{
    public class UserRoleMap : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.HasKey(r => new { r.UserId, r.RoleId });

            builder.ToTable("AspNetUserRoles");
        }
    }
}
