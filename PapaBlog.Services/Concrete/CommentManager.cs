using PapaBlog.Data.Abstract;
using PapaBlog.Services.Abstract;
using PapaBlog.Shared.Utilities.Results.Abstract;
using PapaBlog.Shared.Utilities.Results.ComplexTypes;
using PapaBlog.Shared.Utilities.Results.Concrete;
using System.Threading.Tasks;

namespace PapaBlog.Services.Concrete
{
    public class CommentManager : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CommentManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IDataResult<int>> CountAsync()
        {
            var commentCount = await _unitOfWork.Comments.CountAsync();
            return commentCount > -1
                ? new DataResult<int>(ResultStatus.Success, commentCount)
                : new DataResult<int>(ResultStatus.Success, "Beklenmeyen bir hata oluştu.", -1);
        }

        public async Task<IDataResult<int>> CountByNonDeleteAsync()
        {
            var commentCount = await _unitOfWork.Comments.CountAsync(x => !x.IsDeleted);
            return commentCount > -1
                ? new DataResult<int>(ResultStatus.Success, commentCount)
                : new DataResult<int>(ResultStatus.Success, "Beklenmeyen bir hata oluştu.", -1);
        }
    }
}
