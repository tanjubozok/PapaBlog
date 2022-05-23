using Microsoft.EntityFrameworkCore;
using PapaBlog.Data.Abstract;
using PapaBlog.Data.Concrete.EfCore.Contexts;
using PapaBlog.Entities.Concrete;
using PapaBlog.Shared.Data.Concrete.EfCore;
using System.Threading.Tasks;

namespace PapaBlog.Data.Concrete.EfCore.Repositories
{
    public class EfCategoryRepository : EfEntityRepositoryBase<Category>, ICategoryRepository
    {
        public EfCategoryRepository(DbContext dbContext) : base(dbContext)
        {
        }

        private PapaBlogContext PapaBlogContext {
            get {
                return _dbContext as PapaBlogContext;
            }
        }

        public async Task<Category> GetById(int categoryId)
        {
            return await PapaBlogContext.Categories.SingleOrDefaultAsync(x => x.Id == categoryId);
        }
    }
}