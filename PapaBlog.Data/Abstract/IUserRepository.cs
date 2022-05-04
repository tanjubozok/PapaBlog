using PapaBlog.Entities.Concrete;
using PapaBlog.Shared.Data.Abstract;

namespace PapaBlog.Data.Abstract
{
    public interface IUserRepository : IEntityRepository<User>
    {
    }
}
