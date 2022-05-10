using PapaBlog.Dtos.Concrete.ArticleDtos;
using PapaBlog.Shared.Utilities.Results.Abstract;
using System.Threading.Tasks;

namespace PapaBlog.Services.Abstract
{
    public interface IArticleService
    {
        Task<IDataResult<ArticleDto>> Get(int articleId);
        Task<IDataResult<ArticleUpdateDto>> GetArticleUpdateDto(int articltId);
        Task<IDataResult<ArticleListDto>> GetAll();
        Task<IDataResult<ArticleListDto>> GetAllByNonDeleted();
        Task<IDataResult<ArticleListDto>> GetAllByNonDeletedAndActive();
        Task<IDataResult<ArticleListDto>> GetAllByCategory(int categoryId);
        Task<IDataResult<ArticleDto>> Add(ArticleAddDto articleAddDto, string createdByName);
        Task<IDataResult<ArticleDto>> Update(ArticleUpdateDto articleUpdateDto, string modifiedByName);
        Task<IDataResult<ArticleDto>> Delete(int articleId, string modifiedByName);
        Task<IResult> HardDelete(int articleId);
    }
}