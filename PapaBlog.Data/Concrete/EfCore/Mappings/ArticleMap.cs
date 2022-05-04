using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PapaBlog.Entities.Concrete;
using System;

namespace PapaBlog.Data.Concrete.EfCore.Mappings
{
    public class ArticleMap : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.ToTable("Articles");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Title).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Content).HasColumnType("nvarchar(max)").IsRequired();
            builder.Property(x => x.Date).IsRequired();
            builder.Property(x => x.SeoAuther).HasMaxLength(50).IsRequired();
            builder.Property(x => x.SeoDescription).HasMaxLength(150).IsRequired();
            builder.Property(x => x.SeoTags).HasMaxLength(70).IsRequired();
            builder.Property(x => x.CommentCount).IsRequired();
            builder.Property(x => x.ViewsCount).IsRequired();
            builder.Property(x => x.Thumbnail).HasMaxLength(250).IsRequired();
            builder.Property(x => x.CreatedByName).HasMaxLength(50).IsRequired();
            builder.Property(x => x.ModifiedByName).HasMaxLength(50).IsRequired();
            builder.Property(x => x.CreatedDate).IsRequired();
            builder.Property(x => x.ModifiedDate).IsRequired();
            builder.Property(x => x.IsActive).IsRequired();
            builder.Property(x => x.IsDeleted).IsRequired();
            builder.Property(x => x.Note).HasMaxLength(500);

            builder.HasOne<Category>(x => x.Category).WithMany(x => x.Articles).HasForeignKey(x => x.CategoryId);
            builder.HasOne<User>(x => x.User).WithMany(x => x.Articles).HasForeignKey(x => x.UserId);

            builder.HasData(
                new Article
                {
                    Id = 1,
                    Title = "Test Article",
                    Content = "Test Article Content",
                    Date = DateTime.Now,
                    SeoAuther = "Test SeoAuther",
                    SeoDescription = "Test SeoDescription",
                    SeoTags = "Test SeoTags",
                    CommentCount = 120,
                    ViewsCount = 10,
                    Thumbnail = "default.jpg",
                    CreatedByName = "Test CreatedByName",
                    ModifiedByName = "Test ModifiedByName",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    IsActive = true,
                    IsDeleted = false,
                    Note = "Test Note",
                    CategoryId = 1,
                    UserId = 1
                },
                new Article
                {
                    Id = 2,
                    Title = "Test Article",
                    Content = "Test Article Content",
                    Date = DateTime.Now,
                    SeoAuther = "Test SeoAuther",
                    SeoDescription = "Test SeoDescription",
                    SeoTags = "Test SeoTags",
                    CommentCount = 220,
                    ViewsCount = 20,
                    Thumbnail = "default.jpg",
                    CreatedByName = "Test CreatedByName",
                    ModifiedByName = "Test ModifiedByName",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    IsActive = true,
                    IsDeleted = false,
                    Note = "Test Note",
                    CategoryId = 2,
                    UserId = 1
                },
                new Article
                {
                    Id = 3,
                    Title = "Test Article",
                    Content = "Test Article Content",
                    Date = DateTime.Now,
                    SeoAuther = "Test SeoAuther",
                    SeoDescription = "Test SeoDescription",
                    SeoTags = "Test SeoTags",
                    CommentCount = 320,
                    ViewsCount = 30,
                    Thumbnail = "default.jpg",
                    CreatedByName = "Test CreatedByName",
                    ModifiedByName = "Test ModifiedByName",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    IsActive = true,
                    IsDeleted = false,
                    Note = "Test Note",
                    CategoryId = 3,
                    UserId = 1
                });
        }
    }
}