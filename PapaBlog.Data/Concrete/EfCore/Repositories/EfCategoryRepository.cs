using Microsoft.EntityFrameworkCore;
using PapaBlog.Data.Abstract;
using PapaBlog.Entities.Concrete;
using PapaBlog.Shared.Data.Concrete.EfCore;

namespace PapaBlog.Data.Concrete.EfCore.Repositories
{
    public class EfCategoryRepository : EfEntityRepositoryBase<Category>, ICategoryRepository
    {
        public EfCategoryRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
