using Microsoft.EntityFrameworkCore;
using PapaBlog.Data.Abstract;
using PapaBlog.Entities.Concrete;
using PapaBlog.Shared.Data.Concrete.EfCore;

namespace PapaBlog.Data.Concrete.EfCore.Repositories
{
    public class EfCommentRepository : EfEntityRepositoryBase<Comment>, ICommentRepository
    {
        public EfCommentRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
