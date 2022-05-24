using PapaBlog.Shared.Utilities.Results.Abstract;
using System.Threading.Tasks;

namespace PapaBlog.Services.Abstract
{
    public interface ICommentService
    {
        Task<IDataResult<int>> CountAsync();
        Task<IDataResult<int>> CountByNonDeleteAsync();
    }
}
