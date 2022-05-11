using System;
using System.Threading.Tasks;

namespace PapaBlog.Data.Abstract
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        //IRepository<T> GetRepository<T>() where T : class;
        //int SaveChanges();

        IArticleRepository Articles { get; }
        ICategoryRepository Categories { get; }
        ICommentRepository Comments { get; }
        Task<int> SaveAsync();
    }
}
