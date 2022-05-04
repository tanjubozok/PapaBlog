using Microsoft.EntityFrameworkCore;
using PapaBlog.Entities.Concrete;

namespace PapaBlog.Data.Concrete.EfCore.Contexts
{
    public class PapaBlogContext : DbContext
    {
        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.;Database=PapaBlogData;Trusted_Connection=True;");
        }
    }
}
