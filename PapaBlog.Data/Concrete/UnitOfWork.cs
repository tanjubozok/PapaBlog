using PapaBlog.Data.Abstract;
using PapaBlog.Data.Concrete.EfCore.Contexts;
using PapaBlog.Data.Concrete.EfCore.Repositories;
using System.Threading.Tasks;

namespace PapaBlog.Data.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PapaBlogContext _dbContext;

        private EfArticleRepository _efArticleRepository;
        private EfCategoryRepository _efCategoryRepository;
        private EfCommentRepository _efCommentRepository;

        public UnitOfWork(PapaBlogContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IArticleRepository Articles => _efArticleRepository ?? new EfArticleRepository(_dbContext);
        public ICategoryRepository Categories => _efCategoryRepository ?? new EfCategoryRepository(_dbContext);
        public ICommentRepository Comments => _efCommentRepository ?? new EfCommentRepository(_dbContext);

        public async Task<int> SaveAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await _dbContext.DisposeAsync();
        }
    }
}