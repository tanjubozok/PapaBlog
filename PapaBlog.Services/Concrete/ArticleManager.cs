﻿using AutoMapper;
using PapaBlog.Data.Abstract;
using PapaBlog.Dtos.Concrete.ArticleDtos;
using PapaBlog.Entities.Concrete;
using PapaBlog.Services.Abstract;
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

        public async Task<IDataResult<ArticleDto>> Add(ArticleAddDto articleAddDto, string createdByName)
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
                    return new DataResult<ArticleDto>(ResultStatus.Success, $"{articleAddDto.Title} makalesi başarılı bir şekilde eklendi.", new ArticleDto
                    {
                        Article = addedArticle,
                        Message = "Makale başarıyla eklendi.",
                        ResultStatus = ResultStatus.Success
                    });
                }
                return new DataResult<ArticleDto>(ResultStatus.Error, "Makale eklenemedi.", new ArticleDto
                {
                    Article = null,
                    Message = "Makale eklenemedi.",
                    ResultStatus = ResultStatus.Error
                });
            }
            catch (Exception ex)
            {
                return new DataResult<ArticleDto>(ResultStatus.TryCatch, "try-catch", new ArticleDto
                {
                    Article = null,
                    Message = "try-catch : " + ex.Message,
                    ResultStatus = ResultStatus.TryCatch
                }, ex);
            }
        }

        public async Task<IResult> Delete(int articleId, string modifiedByName)
        {
            try
            {
                if (await _unitOfWork.Articles.AnyAsync(x => x.Id == articleId))
                {
                    var deleteArticle = await _unitOfWork.Articles.GetAsync(x => x.Id == articleId);
                    deleteArticle.IsDeleted = true;
                    deleteArticle.ModifiedByName = modifiedByName;
                    deleteArticle.ModifiedDate = DateTime.Now;
                    await _unitOfWork.Articles.UpdateAsycn(deleteArticle);
                    var result = await _unitOfWork.SaveAsync();
                    if (result == 1)
                    {
                        return new Result(ResultStatus.Success, $"{deleteArticle.Title} makalesi başarılı bir şekilde silindi.");
                    }
                    return new Result(ResultStatus.Error, "Makale silinemedi.");
                }
                return new Result(ResultStatus.Error, "Makale-id bulunamadı.");
            }
            catch (Exception ex)
            {
                return new Result(ResultStatus.TryCatch, "try-catch", ex);
            }
        }

        public async Task<IDataResult<ArticleDto>> Get(int articleId)
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
                    return new DataResult<ArticleDto>(ResultStatus.Error, "Makale bulunamadı.", new ArticleDto
                    {
                        Article = null,
                        Message = "Makale bulunamadı.",
                        ResultStatus = ResultStatus.Error
                    });
                }
                return new DataResult<ArticleDto>(ResultStatus.Error, "Makale-id bulunamadı.", new ArticleDto
                {
                    Article = null,
                    Message = "Makale-id bulunamadı.",
                    ResultStatus = ResultStatus.Error
                }, null);
            }
            catch (Exception ex)
            {
                return new DataResult<ArticleDto>(ResultStatus.TryCatch, "try-catch", new ArticleDto
                {
                    Article = null,
                    Message = "try-catch : " + ex.Message,
                    ResultStatus = ResultStatus.TryCatch
                }, ex);
            }
        }

        public async Task<IDataResult<ArticleListDto>> GetAll()
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
                return new DataResult<ArticleListDto>(ResultStatus.Error, "Makale bulunamadı.", new ArticleListDto
                {
                    Articles = null,
                    Message = "Makale bulunamadı.",
                    ResultStatus = ResultStatus.Error
                });
            }
            catch (Exception ex)
            {
                return new DataResult<ArticleListDto>(ResultStatus.TryCatch, "try-catch", new ArticleListDto
                {
                    Articles = null,
                    Message = "try-catch : " + ex.Message,
                    ResultStatus = ResultStatus.TryCatch
                }, ex);
            }
        }

        public async Task<IDataResult<ArticleListDto>> GetAllByCategory(int categoryId)
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
                    return new DataResult<ArticleListDto>(ResultStatus.Error, "Makale bulunamadı", new ArticleListDto
                    {
                        Articles = null,
                        Message = "Makale bulunamadı.",
                        ResultStatus = ResultStatus.Error
                    });
                }
                return new DataResult<ArticleListDto>(ResultStatus.Error, "Kategori-id bulunamadı", new ArticleListDto
                {
                    Articles = null,
                    Message = "Kategori - id bulunamadı",
                    ResultStatus = ResultStatus.Error
                });
            }
            catch (Exception ex)
            {
                return new DataResult<ArticleListDto>(ResultStatus.TryCatch, "try-catch", new ArticleListDto
                {
                    Articles = null,
                    Message = "try-catch : " + ex.Message,
                    ResultStatus = ResultStatus.TryCatch
                }, ex);
            }
        }

        public async Task<IDataResult<ArticleListDto>> GetAllByNonDeleted()
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
                return new DataResult<ArticleListDto>(ResultStatus.Success, "Makale bulunamadı.", new ArticleListDto
                {
                    Articles = null,
                    Message = "Makale bulunamadı.",
                    ResultStatus = ResultStatus.Error
                });
            }
            catch (Exception ex)
            {
                return new DataResult<ArticleListDto>(ResultStatus.TryCatch, "try-catch", new ArticleListDto
                {
                    Articles = null,
                    Message = "try-catch : " + ex.Message,
                    ResultStatus = ResultStatus.TryCatch
                }, ex);
            }
        }

        public async Task<IDataResult<ArticleListDto>> GetAllByNonDeletedAndActive()
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
                return new DataResult<ArticleListDto>(ResultStatus.Error, "Makale bulunamadı", new ArticleListDto
                {
                    Articles = null,
                    Message = "Makale bulunamadı.",
                    ResultStatus = ResultStatus.Error
                }, null);
            }
            catch (Exception ex)
            {
                return new DataResult<ArticleListDto>(ResultStatus.TryCatch, "try-catch", new ArticleListDto
                {
                    Articles = null,
                    Message = "try-catch : " + ex.Message,
                    ResultStatus = ResultStatus.TryCatch
                }, ex);
            }
        }

        public async Task<IResult> HardDelete(int articleId)
        {
            try
            {
                if (await _unitOfWork.Articles.AnyAsync(x => x.Id == articleId))
                {
                    var deletedArticle = await _unitOfWork.Articles.GetAsync(x => x.Id == articleId);
                    await _unitOfWork.Articles.DeleteAsync(deletedArticle);
                    var result = await _unitOfWork.SaveAsync();
                    if (result == 1)
                    {
                        return new Result(ResultStatus.Success, $"{deletedArticle.Title} makalesi başarılı bir şekilde veritabanından silindi.");
                    }
                    return new Result(ResultStatus.Error, "Makale veritabanından silinemedi.");
                }
                return new Result(ResultStatus.Error, "Makale-id bulunamadı.");
            }
            catch (Exception ex)
            {
                return new Result(ResultStatus.TryCatch, "try-catch", ex);
            }
        }

        public async Task<IDataResult<ArticleDto>> Update(ArticleUpdateDto articleUpdateDto, string modifiedByName)
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
                        return new DataResult<ArticleDto>(ResultStatus.Success, $"{articleUpdateDto.Title} makalesi başarılı bir şekilde güncellendi.", new ArticleDto
                        {
                            Article = updadetArticle,
                            ResultStatus = ResultStatus.Success
                        });
                    }
                    return new DataResult<ArticleDto>(ResultStatus.Error, "Makale güncellenemedi.", new ArticleDto
                    {
                        Article = null,
                        Message = "Makale güncellenemedi.",
                        ResultStatus = ResultStatus.Error
                    });
                }
                return new DataResult<ArticleDto>(ResultStatus.Error, "Makale-id bulunamadı.", new ArticleDto
                {
                    Article = null,
                    Message = "Makale-id bulunamadı.",
                    ResultStatus = ResultStatus.Error
                });
            }
            catch (Exception ex)
            {
                return new DataResult<ArticleDto>(ResultStatus.TryCatch, "try-catch", new ArticleDto
                {
                    Article = null,
                    Message = "try-catch : " + ex.Message,
                    ResultStatus = ResultStatus.TryCatch
                }, ex);
            }
        }
    }
}