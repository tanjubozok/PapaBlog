using PapaBlog.Entities.Concrete;
using PapaBlog.Shared.Data.Abstract;
using System.Threading.Tasks;

namespace PapaBlog.Data.Abstract
{
    public interface ICategoryRepository : IEntityRepository<Category>
    {
        Task<Category> GetById(int categoryId);
    }
}
