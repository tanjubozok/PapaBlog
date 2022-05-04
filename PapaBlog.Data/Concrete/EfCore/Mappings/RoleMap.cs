using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PapaBlog.Entities.Concrete;
using System;

namespace PapaBlog.Data.Concrete.EfCore.Mappings
{
    public class RoleMap : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Name).HasMaxLength(30).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(250).IsRequired();
            builder.Property(x => x.CreatedByName).HasMaxLength(50).IsRequired();
            builder.Property(x => x.ModifiedByName).HasMaxLength(50).IsRequired();
            builder.Property(x => x.CreatedDate).IsRequired();
            builder.Property(x => x.ModifiedDate).IsRequired();
            builder.Property(x => x.IsActive).IsRequired();
            builder.Property(x => x.IsDeleted).IsRequired();
            builder.Property(x => x.Note).HasMaxLength(500);

            builder.HasData(
                new Role
                {
                    Id = 1,
                    Name = "Admin",
                    Description = "Admin",
                    CreatedByName = "Admin",
                    ModifiedByName = "Admin",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    IsActive = true,
                    IsDeleted = false,
                    Note = "Admin"
                },
                new Role
                {
                    Id = 2,
                    Name = "User",
                    Description = "User",
                    CreatedByName = "Admin",
                    ModifiedByName = "Admin",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    IsActive = true,
                    IsDeleted = false,
                    Note = "User"
                });
        }
    }
}