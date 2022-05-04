using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PapaBlog.Entities.Concrete;
using System;

namespace PapaBlog.Data.Concrete.EfCore.Mappings
{
    public class CategoryMap : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Name).HasMaxLength(70).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(500);
            builder.Property(x => x.CreatedByName).HasMaxLength(50).IsRequired();
            builder.Property(x => x.ModifiedByName).HasMaxLength(50).IsRequired();
            builder.Property(x => x.CreatedDate).IsRequired();
            builder.Property(x => x.ModifiedDate).IsRequired();
            builder.Property(x => x.IsActive).IsRequired();
            builder.Property(x => x.IsDeleted).IsRequired();
            builder.Property(x => x.Note).HasMaxLength(500);

            builder.HasData(
                new Category
                {
                    Id = 1,
                    Name = "C#",
                    Description = "C#",
                    CreatedByName = "System",
                    ModifiedByName = "System",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    IsActive = true,
                    IsDeleted = false,
                    Note = "C#"
                },
                new Category
                {
                    Id = 2,
                    Name = "Java",
                    Description = "Java",
                    CreatedByName = "System",
                    ModifiedByName = "System",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    IsActive = true,
                    IsDeleted = false,
                    Note = "Java"
                },
                new Category
                {
                    Id = 3,
                    Name = "Python",
                    Description = "Python",
                    CreatedByName = "System",
                    ModifiedByName = "System",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    IsActive = true,
                    IsDeleted = false,
                    Note = "Python"
                });
        }
    }
}
