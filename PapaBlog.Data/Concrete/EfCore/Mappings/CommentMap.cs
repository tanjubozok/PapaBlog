using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PapaBlog.Entities.Concrete;
using System;

namespace PapaBlog.Data.Concrete.EfCore.Mappings
{
    public class CommentMap : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("Comments");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Text).HasMaxLength(1000).IsRequired();
            builder.Property(x => x.CreatedByName).HasMaxLength(50).IsRequired();
            builder.Property(x => x.ModifiedByName).HasMaxLength(50).IsRequired();
            builder.Property(x => x.CreatedDate).IsRequired();
            builder.Property(x => x.ModifiedDate).IsRequired();
            builder.Property(x => x.IsActive).IsRequired();
            builder.Property(x => x.IsDeleted).IsRequired();
            builder.Property(x => x.Note).HasMaxLength(500);

            builder.HasOne<Article>(x => x.Article).WithMany(x => x.Comments).HasForeignKey(x => x.ArticleId);

            //builder.HasData(
            //    new Comment
            //    {
            //        Id = 1,
            //        ArticleId = 1,
            //        Text = "This is a comment",
            //        CreatedByName = "Admin",
            //        ModifiedByName = "Admin",
            //        CreatedDate = DateTime.Now,
            //        ModifiedDate = DateTime.Now,
            //        IsActive = true,
            //        IsDeleted = false,
            //        Note = "This is a note"
            //    },
            //    new Comment
            //    {
            //        Id = 2,
            //        ArticleId = 2,
            //        Text = "This is a comment",
            //        CreatedByName = "Admin",
            //        ModifiedByName = "Admin",
            //        CreatedDate = DateTime.Now,
            //        ModifiedDate = DateTime.Now,
            //        IsActive = true,
            //        IsDeleted = false,
            //        Note = "This is a note"
            //    },
            //    new Comment
            //    {
            //        Id = 3,
            //        ArticleId = 3,
            //        Text = "This is a comment",
            //        CreatedByName = "Admin",
            //        ModifiedByName = "Admin",
            //        CreatedDate = DateTime.Now,
            //        ModifiedDate = DateTime.Now,
            //        IsActive = true,
            //        IsDeleted = false,
            //        Note = "This is a note"
            //    });
        }
    }
}