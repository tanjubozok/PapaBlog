using AutoMapper;
using PapaBlog.Data.Abstract;
using PapaBlog.Dtos.Concrete.CategoryDtos;
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
    public class CategoryManager : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IDataResult<CategoryDto>> AddAsync(CategoryAddDto categoryAddDto, string createByName)
        {
            try
            {
                var category = _mapper.Map<Category>(categoryAddDto);
                category.ModifiedByName = createByName;
                category.CreatedByName = createByName;
                var addedCategory = await _unitOfWork.Categories.AddAsync(category);
                var result = await _unitOfWork.SaveAsync();
                if (result == 1)
                {
                    return new DataResult<CategoryDto>(ResultStatus.Success, Messages.Category.Add(addedCategory.Name), new CategoryDto
                    {
                        Category = addedCategory,
                        Message = Messages.Category.Add(addedCategory.Name),
                        ResultStatus = ResultStatus.Success
                    });
                }
                return new DataResult<CategoryDto>(ResultStatus.Error, Messages.Category.NotAdding(addedCategory.Name), new CategoryDto
                {
                    Category = null,
                    Message = Messages.Category.NotAdding(addedCategory.Name),
                    ResultStatus = ResultStatus.Error
                });
            }
            catch (Exception ex)
            {
                return new DataResult<CategoryDto>(ResultStatus.TryCatch, Messages.Generel.TryCatch(ex.Message), new CategoryDto
                {
                    Category = null,
                    Message = Messages.Generel.TryCatch(ex.Message),
                    ResultStatus = ResultStatus.TryCatch
                }, ex);
            }
        }

        public async Task<IDataResult<int>> CountAsync()
        {
            var categoriesCount = await _unitOfWork.Categories.CountAsync();
            return categoriesCount > -1
                ? new DataResult<int>(ResultStatus.Success, categoriesCount)
                : new DataResult<int>(ResultStatus.Success, "Beklenmeyen bir hata oluştu.", -1);
        }

        public async Task<IDataResult<int>> CountByNonDeleteAsync()
        {
            var categoriesCount = await _unitOfWork.Categories.CountAsync(x => !x.IsDeleted);
            return categoriesCount > -1
                ? new DataResult<int>(ResultStatus.Success, categoriesCount)
                : new DataResult<int>(ResultStatus.Success, "Beklenmeyen bir hata oluştu.", -1);
        }

        public async Task<IDataResult<CategoryDto>> DeleteAsync(int categoryId, string modifiedByName)
        {
            try
            {
                if (await _unitOfWork.Categories.AnyAsync(x => x.Id == categoryId))
                {
                    var category = await _unitOfWork.Categories.GetAsync(x => x.Id == categoryId);
                    if (category != null)
                    {
                        category.IsDeleted = true;
                        category.ModifiedByName = modifiedByName;
                        category.ModifiedDate = DateTime.Now;
                        var deletedCategory = await _unitOfWork.Categories.UpdateAsycn(category);
                        var result = await _unitOfWork.SaveAsync();
                        if (result == 1)
                        {
                            return new DataResult<CategoryDto>(ResultStatus.Success, Messages.Category.Delete(deletedCategory.Name), new CategoryDto
                            {
                                Category = category,
                                Message = Messages.Category.Delete(deletedCategory.Name),
                                ResultStatus = ResultStatus.Success
                            });
                        }
                        return new DataResult<CategoryDto>(ResultStatus.Error, Messages.Category.NotDeleting(deletedCategory.Name), new CategoryDto
                        {
                            Category = null,
                            Message = Messages.Category.NotDeleting(deletedCategory.Name),
                            ResultStatus = ResultStatus.Error
                        });
                    }
                    return new DataResult<CategoryDto>(ResultStatus.Error, Messages.Category.NotFound(isPlural: false), new CategoryDto
                    {
                        Category = null,
                        Message = Messages.Category.NotFound(isPlural: false),
                        ResultStatus = ResultStatus.Error
                    });
                }
                return new DataResult<CategoryDto>(ResultStatus.Error, Messages.Category.IdNotFound(), new CategoryDto
                {
                    Category = null,
                    Message = Messages.Category.IdNotFound(),
                    ResultStatus = ResultStatus.Error
                });
            }
            catch (Exception ex)
            {
                return new DataResult<CategoryDto>(ResultStatus.TryCatch, Messages.Generel.TryCatch(ex.Message), new CategoryDto
                {
                    Category = null,
                    Message = Messages.Generel.TryCatch(ex.Message),
                    ResultStatus = ResultStatus.TryCatch
                }, ex);
            }
        }

        public async Task<IDataResult<CategoryDto>> GetAsync(int categoryId)
        {
            try
            {
                if (await _unitOfWork.Categories.AnyAsync(x => x.Id == categoryId))
                {
                    var getCategory = await _unitOfWork.Categories.GetAsync(x => x.Id == categoryId);
                    if (getCategory != null)
                    {
                        return new DataResult<CategoryDto>(ResultStatus.Success, new CategoryDto
                        {
                            Category = getCategory,
                            ResultStatus = ResultStatus.Success
                        });
                    }
                    return new DataResult<CategoryDto>(ResultStatus.Error, Messages.Category.NotFound(isPlural: false), new CategoryDto
                    {
                        Category = null,
                        Message = Messages.Category.NotFound(isPlural: false),
                        ResultStatus = ResultStatus.Error
                    });
                }
                return new DataResult<CategoryDto>(ResultStatus.Error, Messages.Category.IdNotFound(), new CategoryDto
                {
                    Category = null,
                    Message = Messages.Category.IdNotFound(),
                    ResultStatus = ResultStatus.Error
                }, null);
            }
            catch (Exception ex)
            {
                return new DataResult<CategoryDto>(ResultStatus.TryCatch, Messages.Generel.TryCatch(ex.Message), new CategoryDto
                {
                    Category = null,
                    Message = Messages.Generel.TryCatch(ex.Message),
                    ResultStatus = ResultStatus.TryCatch
                }, ex);
            }
        }

        public async Task<IDataResult<CategoryListDto>> GetAllAsync()
        {
            try
            {
                var categories = await _unitOfWork.Categories.GetAllAsync();
                if (categories.Count > 0)
                {
                    return new DataResult<CategoryListDto>(ResultStatus.Success, new CategoryListDto
                    {
                        Categories = categories,
                        ResultStatus = ResultStatus.Success
                    });
                }
                return new DataResult<CategoryListDto>(ResultStatus.Error, Messages.Category.NotFound(isPlural: true), new CategoryListDto
                {
                    Categories = null,
                    Message = Messages.Category.NotFound(isPlural: true),
                    ResultStatus = ResultStatus.Error
                });
            }
            catch (Exception ex)
            {
                return new DataResult<CategoryListDto>(ResultStatus.TryCatch, Messages.Generel.TryCatch(ex.Message), new CategoryListDto
                {
                    Categories = null,
                    Message = Messages.Generel.TryCatch(ex.Message),
                    ResultStatus = ResultStatus.TryCatch
                }, ex);
            }
        }

        public async Task<IDataResult<CategoryListDto>> GetAllByNonDeletedAsync()
        {
            try
            {
                var categories = await _unitOfWork.Categories.GetAllAsync(x => !x.IsDeleted);
                if (categories.Count > 0)
                {
                    return new DataResult<CategoryListDto>(ResultStatus.Success, new CategoryListDto
                    {
                        Categories = categories,
                        ResultStatus = ResultStatus.Success
                    });
                }
                return new DataResult<CategoryListDto>(ResultStatus.Error, Messages.Category.NotFound(isPlural: true), new CategoryListDto
                {
                    Categories = null,
                    Message = Messages.Category.NotFound(isPlural: true),
                    ResultStatus = ResultStatus.Error
                });
            }
            catch (Exception ex)
            {
                return new DataResult<CategoryListDto>(ResultStatus.TryCatch, Messages.Generel.TryCatch(ex.Message), new CategoryListDto
                {
                    Categories = null,
                    Message = Messages.Generel.TryCatch(ex.Message),
                    ResultStatus = ResultStatus.TryCatch
                }, ex);
            }
        }

        public async Task<IDataResult<CategoryListDto>> GetAllByNonDeletedAndActiveAsync()
        {
            try
            {
                var categories = await _unitOfWork.Categories.GetAllAsync(x => !x.IsDeleted && x.IsActive);
                if (categories.Count > 0)
                {
                    return new DataResult<CategoryListDto>(ResultStatus.Success, new CategoryListDto
                    {
                        Categories = categories,
                        ResultStatus = ResultStatus.Success
                    });
                }
                return new DataResult<CategoryListDto>(ResultStatus.Error, Messages.Category.NotFound(isPlural: true), new CategoryListDto
                {
                    Categories = null,
                    Message = Messages.Category.NotFound(isPlural: true),
                    ResultStatus = ResultStatus.Error
                });
            }
            catch (Exception ex)
            {
                return new DataResult<CategoryListDto>(ResultStatus.TryCatch, Messages.Generel.TryCatch(ex.Message), new CategoryListDto
                {
                    Categories = null,
                    Message = Messages.Generel.TryCatch(ex.Message),
                    ResultStatus = ResultStatus.TryCatch
                }, ex);
            }
        }

        public async Task<IDataResult<CategoryUpdateDto>> GetCategoryUpdateDtoAsync(int categoryId)
        {
            try
            {
                if (await _unitOfWork.Categories.AnyAsync(x => x.Id == categoryId))
                {
                    var category = await _unitOfWork.Categories.GetAsync(x => x.Id == categoryId);
                    var categoryUpdateDto = _mapper.Map<CategoryUpdateDto>(category);
                    return new DataResult<CategoryUpdateDto>(ResultStatus.Success, categoryUpdateDto);
                }
                return new DataResult<CategoryUpdateDto>(ResultStatus.Error, Messages.Category.NotFound(isPlural: true), null);
            }
            catch (Exception ex)
            {
                return new DataResult<CategoryUpdateDto>(ResultStatus.TryCatch, Messages.Generel.TryCatch(ex.Message), null, ex);
            }
        }

        public async Task<IResult> HardDeleteAsync(int categoryId)
        {
            try
            {
                if (await _unitOfWork.Categories.AnyAsync(x => x.Id == categoryId))
                {
                    var category = await _unitOfWork.Categories.GetAsync(x => x.Id == categoryId);
                    if (category != null)
                    {
                        await _unitOfWork.Categories.DeleteAsync(category);
                        var result = await _unitOfWork.SaveAsync();
                        if (result == 1)
                        {
                            return new Result(ResultStatus.Success, Messages.Category.HardDelete(category.Name));
                        }
                        return new Result(ResultStatus.Error, Messages.Category.NotHardDelete(category.Name));
                    }
                    return new Result(ResultStatus.Error, Messages.Category.NotFound(isPlural: false));
                }
                return new Result(ResultStatus.Error, Messages.Category.IdNotFound());
            }
            catch (Exception ex)
            {
                return new Result(ResultStatus.TryCatch, Messages.Generel.TryCatch(ex.Message), ex);
            }
        }

        public async Task<IDataResult<CategoryDto>> UpdateAsync(CategoryUpdateDto categoryUpdateDto, string modifiedByName)
        {
            try
            {
                if (await _unitOfWork.Categories.AnyAsync(x => x.Id == categoryUpdateDto.Id))
                {
                    var oldCategory = await _unitOfWork.Categories.GetAsync(x => x.Id == categoryUpdateDto.Id);
                    var category = _mapper.Map<CategoryUpdateDto, Category>(categoryUpdateDto, oldCategory);
                    category.ModifiedByName = modifiedByName;
                    var updadetCategory = await _unitOfWork.Categories.UpdateAsycn(category);
                    var result = await _unitOfWork.SaveAsync();
                    if (result == 1)
                    {
                        return new DataResult<CategoryDto>(ResultStatus.Success, Messages.Category.Update(updadetCategory.Name), new CategoryDto
                        {
                            Category = updadetCategory,
                            Message = Messages.Category.Update(updadetCategory.Name),
                            ResultStatus = ResultStatus.Success
                        });
                    }
                    return new DataResult<CategoryDto>(ResultStatus.Error, Messages.Category.NotUpdating(updadetCategory.Name), new CategoryDto
                    {
                        Category = null,
                        Message = Messages.Category.NotUpdating(updadetCategory.Name),
                        ResultStatus = ResultStatus.Error
                    });
                }
                return new DataResult<CategoryDto>(ResultStatus.Error, Messages.Category.NotFound(isPlural: false), new CategoryDto
                {
                    Category = null,
                    Message = Messages.Category.NotFound(isPlural: false),
                    ResultStatus = ResultStatus.Error
                });
            }
            catch (Exception ex)
            {
                return new DataResult<CategoryDto>(ResultStatus.TryCatch, Messages.Generel.TryCatch(ex.Message), new CategoryDto
                {
                    Category = null,
                    Message = Messages.Generel.TryCatch(ex.Message),
                    ResultStatus = ResultStatus.TryCatch
                }, ex);
            }
        }
    }
}
