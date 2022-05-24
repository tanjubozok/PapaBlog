using PapaBlog.Dtos.Concrete.ArticleDtos;
using PapaBlog.Shared.Utilities.Results.Abstract;
using System.Threading.Tasks;

namespace PapaBlog.Services.Abstract
{
    public interface IArticleService
    {
        Task<IDataResult<ArticleDto>> GetAsync(int articleId);
        Task<IDataResult<ArticleUpdateDto>> GetArticleUpdateDtoAsycn(int articltId);
        Task<IDataResult<ArticleListDto>> GetAllAsync();
        Task<IDataResult<ArticleListDto>> GetAllByNonDeletedAsync();
        Task<IDataResult<ArticleListDto>> GetAllByNonDeletedAndActiveAsync();
        Task<IDataResult<ArticleListDto>> GetAllByCategoryAsync(int categoryId);
        Task<IDataResult<ArticleDto>> AddAsync(ArticleAddDto articleAddDto, string createdByName);
        Task<IDataResult<ArticleDto>> UpdateAsync(ArticleUpdateDto articleUpdateDto, string modifiedByName);
        Task<IDataResult<ArticleDto>> DeleteAsync(int articleId, string modifiedByName);
        Task<IResult> HardDeleteAsync(int articleId);
        Task<IDataResult<int>> CountAsync();
        Task<IDataResult<int>> CountByNonDeleteAsync();
    }
}