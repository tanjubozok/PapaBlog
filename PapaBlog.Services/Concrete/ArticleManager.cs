using AutoMapper;
using PapaBlog.Data.Abstract;
using PapaBlog.Dtos.Concrete.ArticleDtos;
using PapaBlog.Entities.Concrete;
using PapaBlog.Services.Abstract;
using PapaBlog.Services.Utilities;
using PapaBlog.Shared.Utilities.Results.Abstract;
using PapaBlog.Shared.Utilities.Results.ComplexTypes;
using PapaBlog.Shared.Utilities.Results.Concrete;
using System;
using System.Threading.Tasks;

namespace PapaBlog.Services.Concrete
{
    public class ArticleManager : IArticleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ArticleManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IDataResult<ArticleDto>> AddAsync(ArticleAddDto articleAddDto, string createdByName)
        {
            try
            {
                var article = _mapper.Map<Article>(articleAddDto);
                article.CreatedByName = createdByName;
                article.ModifiedByName = createdByName;
                article.UserId = 1;
                var addedArticle = await _unitOfWork.Articles.AddAsync(article);
                var result = await _unitOfWork.SaveAsync();
                if (result == 1)
                {
                    return new DataResult<ArticleDto>(ResultStatus.Success, Messages.Article.Add(), new ArticleDto
                    {
                        Article = addedArticle,
                        Message = Messages.Article.Add(),
                        ResultStatus = ResultStatus.Success
                    });
                }
                return new DataResult<ArticleDto>(ResultStatus.Error, Messages.Article.NotAdding(), new ArticleDto
                {
                    Article = null,
                    Message = Messages.Article.NotAdding(),
                    ResultStatus = ResultStatus.Error
                });
            }
            catch (Exception ex)
            {
                return new DataResult<ArticleDto>(ResultStatus.TryCatch, Messages.Generel.TryCatch(ex.Message), new ArticleDto
                {
                    Article = null,
                    Message = Messages.Generel.TryCatch(ex.Message),
                    ResultStatus = ResultStatus.TryCatch
                }, ex);
            }
        }

        public async Task<IDataResult<int>> CountAsync()
        {
            var articlesCount = await _unitOfWork.Articles.CountAsync();
            return articlesCount > -1
                ? new DataResult<int>(ResultStatus.Success, articlesCount)
                : new DataResult<int>(ResultStatus.Error, "Beklenmeyen bir durum oluştu.", -1);
        }

        public async Task<IDataResult<int>> CountByNonDeleteAsync()
        {
            var articlesCount = await _unitOfWork.Articles.CountAsync(x => !x.IsDeleted);
            return articlesCount > -1
                ? new DataResult<int>(ResultStatus.Success, articlesCount)
                : new DataResult<int>(ResultStatus.Error, "Beklenmeyen bir durum oluştu.", -1);
        }

        public async Task<IDataResult<ArticleDto>> DeleteAsync(int articleId, string modifiedByName)
        {
            try
            {
                if (await _unitOfWork.Articles.AnyAsync(x => x.Id == articleId))
                {
                    var deleteArticle = await _unitOfWork.Articles.GetAsync(x => x.Id == articleId);
                    deleteArticle.IsDeleted = true;
                    deleteArticle.ModifiedByName = modifiedByName;
                    deleteArticle.ModifiedDate = DateTime.Now;
                    var deletedArticle = await _unitOfWork.Articles.UpdateAsycn(deleteArticle);
                    var result = await _unitOfWork.SaveAsync();
                    if (result == 1)
                    {
                        return new DataResult<ArticleDto>(ResultStatus.Success, Messages.Article.Delete(), new ArticleDto
                        {
                            Article = deletedArticle,
                            Message = Messages.Article.Delete(),
                            ResultStatus = ResultStatus.Success
                        });
                    }
                    return new DataResult<ArticleDto>(ResultStatus.Error, Messages.Article.NotDeleting(), new ArticleDto
                    {
                        Article = null,
                        Message = Messages.Article.NotDeleting(),
                        ResultStatus = ResultStatus.Error
                    });
                }
                return new DataResult<ArticleDto>(ResultStatus.Error, Messages.Article.IdNotFound(), new ArticleDto
                {
                    Article = null,
                    Message = Messages.Article.IdNotFound(),
                    ResultStatus = ResultStatus.Error
                });
            }
            catch (Exception ex)
            {
                return new DataResult<ArticleDto>(ResultStatus.TryCatch, Messages.Generel.TryCatch(ex.Message), new ArticleDto
                {
                    Article = null,
                    Message = Messages.Generel.TryCatch(ex.Message),
                    ResultStatus = ResultStatus.TryCatch
                }, ex);
            }
        }

        public async Task<IDataResult<ArticleDto>> GetAsync(int articleId)
        {
            try
            {
                if (await _unitOfWork.Articles.AnyAsync(x => x.Id == articleId))
                {
                    var article = await _unitOfWork.Articles.GetAsync(x => x.Id == articleId, x => x.Category, x => x.User);
                    if (article != null)
                    {
                        return new DataResult<ArticleDto>(ResultStatus.Success, new ArticleDto
                        {
                            Article = article,
                            ResultStatus = ResultStatus.Success
                        });
                    }
                    return new DataResult<ArticleDto>(ResultStatus.Error, Messages.Article.NotFound(isPlural: false), new ArticleDto
                    {
                        Article = null,
                        Message = Messages.Article.NotFound(isPlural: false),
                        ResultStatus = ResultStatus.Error
                    });
                }
                return new DataResult<ArticleDto>(ResultStatus.Error, Messages.Article.IdNotFound(), new ArticleDto
                {
                    Article = null,
                    Message = Messages.Article.IdNotFound(),
                    ResultStatus = ResultStatus.Error
                }, null);
            }
            catch (Exception ex)
            {
                return new DataResult<ArticleDto>(ResultStatus.TryCatch, Messages.Generel.TryCatch(ex.Message), new ArticleDto
                {
                    Article = null,
                    Message = Messages.Generel.TryCatch(ex.Message),
                    ResultStatus = ResultStatus.TryCatch
                }, ex);
            }
        }

        public async Task<IDataResult<ArticleListDto>> GetAllAsync()
        {
            try
            {
                var articles = await _unitOfWork.Articles.GetAllAsync(null, x => x.Category, x => x.User);
                if (articles != null)
                {
                    return new DataResult<ArticleListDto>(ResultStatus.Success, new ArticleListDto
                    {
                        Articles = articles,
                        ResultStatus = ResultStatus.Success
                    });
                }
                return new DataResult<ArticleListDto>(ResultStatus.Error, Messages.Article.NotFound(isPlural: true), new ArticleListDto
                {
                    Articles = null,
                    Message = Messages.Article.NotFound(isPlural: true),
                    ResultStatus = ResultStatus.Error
                });
            }
            catch (Exception ex)
            {
                return new DataResult<ArticleListDto>(ResultStatus.TryCatch, Messages.Generel.TryCatch(ex.Message), new ArticleListDto
                {
                    Articles = null,
                    Message = Messages.Generel.TryCatch(ex.Message),
                    ResultStatus = ResultStatus.TryCatch
                }, ex);
            }
        }

        public async Task<IDataResult<ArticleListDto>> GetAllByCategoryAsync(int categoryId)
        {
            try
            {
                if (await _unitOfWork.Categories.AnyAsync(x => x.Id == categoryId))
                {
                    var articles = await _unitOfWork.Articles.GetAllAsync(x => x.CategoryId == categoryId && x.IsActive && !x.IsDeleted, x => x.User, x => x.Category);
                    if (articles != null)
                    {
                        return new DataResult<ArticleListDto>(ResultStatus.Success, new ArticleListDto
                        {
                            Articles = articles,
                            ResultStatus = ResultStatus.Success
                        });
                    }
                    return new DataResult<ArticleListDto>(ResultStatus.Error, Messages.Article.NotFound(isPlural: true), new ArticleListDto
                    {
                        Articles = null,
                        Message = Messages.Article.NotFound(isPlural: true),
                        ResultStatus = ResultStatus.Error
                    });
                }
                return new DataResult<ArticleListDto>(ResultStatus.Error, Messages.Article.IdNotFound(), new ArticleListDto
                {
                    Articles = null,
                    Message = Messages.Article.IdNotFound(),
                    ResultStatus = ResultStatus.Error
                });
            }
            catch (Exception ex)
            {
                return new DataResult<ArticleListDto>(ResultStatus.TryCatch, Messages.Generel.TryCatch(ex.Message), new ArticleListDto
                {
                    Articles = null,
                    Message = Messages.Generel.TryCatch(ex.Message),
                    ResultStatus = ResultStatus.TryCatch
                }, ex);
            }
        }

        public async Task<IDataResult<ArticleListDto>> GetAllByNonDeletedAsync()
        {
            try
            {
                var articles = await _unitOfWork.Articles.GetAllAsync(x => !x.IsDeleted, x => x.User, x => x.Category);
                if (articles != null)
                {
                    return new DataResult<ArticleListDto>(ResultStatus.Success, new ArticleListDto
                    {
                        Articles = articles,
                        ResultStatus = ResultStatus.Success
                    });
                }
                return new DataResult<ArticleListDto>(ResultStatus.Success, Messages.Article.NotFound(isPlural: true), new ArticleListDto
                {
                    Articles = null,
                    Message = Messages.Article.NotFound(isPlural: true),
                    ResultStatus = ResultStatus.Error
                });
            }
            catch (Exception ex)
            {
                return new DataResult<ArticleListDto>(ResultStatus.TryCatch, Messages.Generel.TryCatch(ex.Message), new ArticleListDto
                {
                    Articles = null,
                    Message = Messages.Generel.TryCatch(ex.Message),
                    ResultStatus = ResultStatus.TryCatch
                }, ex);
            }
        }

        public async Task<IDataResult<ArticleListDto>> GetAllByNonDeletedAndActiveAsync()
        {
            try
            {
                var articles = await _unitOfWork.Articles.GetAllAsync(x => !x.IsDeleted && x.IsActive, x => x.User, x => x.Category);
                if (articles != null)
                {
                    return new DataResult<ArticleListDto>(ResultStatus.Success, new ArticleListDto
                    {
                        Articles = articles,
                        ResultStatus = ResultStatus.Success
                    });
                }
                return new DataResult<ArticleListDto>(ResultStatus.Error, Messages.Article.NotFound(isPlural: true), new ArticleListDto
                {
                    Articles = null,
                    Message = Messages.Article.NotFound(isPlural: true),
                    ResultStatus = ResultStatus.Error
                }, null);
            }
            catch (Exception ex)
            {
                return new DataResult<ArticleListDto>(ResultStatus.TryCatch, Messages.Generel.TryCatch(ex.Message), new ArticleListDto
                {
                    Articles = null,
                    Message = Messages.Generel.TryCatch(ex.Message),
                    ResultStatus = ResultStatus.TryCatch
                }, ex);
            }
        }

        public async Task<IDataResult<ArticleUpdateDto>> GetArticleUpdateDtoAsycn(int articltId)
        {
            try
            {
                if (await _unitOfWork.Articles.AnyAsync(x => x.Id == articltId))
                {
                    var article = await _unitOfWork.Articles.GetAsync(x => x.Id == articltId);
                    var articleDtoUpdate = _mapper.Map<ArticleUpdateDto>(article);
                    return new DataResult<ArticleUpdateDto>(ResultStatus.Success, articleDtoUpdate);
                }
                return new DataResult<ArticleUpdateDto>(ResultStatus.Error, Messages.Article.IdNotFound(), null);
            }
            catch (Exception ex)
            {
                return new DataResult<ArticleUpdateDto>(ResultStatus.TryCatch, Messages.Generel.TryCatch(ex.Message), null, ex);
            }
        }

        public async Task<IResult> HardDeleteAsync(int articleId)
        {
            try
            {
                if (await _unitOfWork.Articles.AnyAsync(x => x.Id == articleId))
                {
                    var deletedArticle = await _unitOfWork.Articles.GetAsync(x => x.Id == articleId);
                    await _unitOfWork.Articles.DeleteAsync(deletedArticle);
                    var result = await _unitOfWork.SaveAsync();
                    if (result == 1)
                        return new Result(ResultStatus.Success, Messages.Article.HardDelete());
                    return new Result(ResultStatus.Error, Messages.Article.NotHardDelete());
                }
                return new Result(ResultStatus.Error, Messages.Article.IdNotFound());
            }
            catch (Exception ex)
            {
                return new Result(ResultStatus.TryCatch, Messages.Generel.TryCatch(ex.Message), ex);
            }
        }

        public async Task<IDataResult<ArticleDto>> UpdateAsync(ArticleUpdateDto articleUpdateDto, string modifiedByName)
        {
            try
            {
                if (await _unitOfWork.Articles.AnyAsync(x => x.Id == articleUpdateDto.Id))
                {
                    var article = _mapper.Map<Article>(articleUpdateDto);
                    article.ModifiedByName = modifiedByName;
                    var updadetArticle = await _unitOfWork.Articles.UpdateAsycn(article);
                    var result = await _unitOfWork.SaveAsync();
                    if (result == 1)
                    {
                        return new DataResult<ArticleDto>(ResultStatus.Success, Messages.Article.Update(), new ArticleDto
                        {
                            Article = updadetArticle,
                            ResultStatus = ResultStatus.Success
                        });
                    }
                    return new DataResult<ArticleDto>(ResultStatus.Error, Messages.Article.NotUpdating(), new ArticleDto
                    {
                        Article = null,
                        Message = Messages.Article.NotUpdating(),
                        ResultStatus = ResultStatus.Error
                    });
                }
                return new DataResult<ArticleDto>(ResultStatus.Error, Messages.Article.IdNotFound(), new ArticleDto
                {
                    Article = null,
                    Message = Messages.Article.IdNotFound(),
                    ResultStatus = ResultStatus.Error
                });
            }
            catch (Exception ex)
            {
                return new DataResult<ArticleDto>(ResultStatus.TryCatch, Messages.Generel.TryCatch(ex.Message), new ArticleDto
                {
                    Article = null,
                    Message = Messages.Generel.TryCatch(ex.Message),
                    ResultStatus = ResultStatus.TryCatch
                }, ex);
            }
        }
    }
}
