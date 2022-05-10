using AutoMapper;
using PapaBlog.Data.Abstract;
using PapaBlog.Dtos.Concrete.CategoryDtos;
using PapaBlog.Entities.Concrete;
using PapaBlog.Services.Abstract;
using PapaBlog.Shared.Utilities.Results.Abstract;
using PapaBlog.Shared.Utilities.Results.ComplexTypes;
using PapaBlog.Shared.Utilities.Results.Concrete;
using System;
using System.Threading.Tasks;
using System.Xml.Linq;

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

        public async Task<IDataResult<CategoryDto>> Add(CategoryAddDto categoryAddDto, string createByName)
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
                    return new DataResult<CategoryDto>(ResultStatus.Success, $"{categoryAddDto.Name} kategorisi başarılı bir şekilde eklendi.", new CategoryDto
                    {
                        Category = addedCategory,
                        Message = "Kategori başarıyla eklendi.",
                        ResultStatus = ResultStatus.Success
                    });
                }
                return new DataResult<CategoryDto>(ResultStatus.Error, "Kategori eklenemedi.", new CategoryDto
                {
                    Category = null,
                    Message = "Kategori eklenemedi.",
                    ResultStatus = ResultStatus.Error
                });
            }
            catch (Exception ex)
            {
                return new DataResult<CategoryDto>(ResultStatus.TryCatch, "try-catch", new CategoryDto
                {
                    Category = null,
                    Message = ex.Message,
                    ResultStatus = ResultStatus.TryCatch
                }, ex);
            }
        }

        public async Task<IDataResult<CategoryDto>> Delete(int categoryId, string modifiedByName)
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
                            return new DataResult<CategoryDto>(ResultStatus.Success, $"{category.Name} kategorisi başarılı bir şekilde silindi.", new CategoryDto
                            {
                                Category = category,
                                Message = $"{category.Name} kategorisi başarılı bir şekilde silindi.",
                                ResultStatus = ResultStatus.Success
                            });
                        }
                        return new DataResult<CategoryDto>(ResultStatus.Error, "Kategori silinemedi.", new CategoryDto
                        {
                            Category = null,
                            Message = "Kategori silinemdi",
                            ResultStatus = ResultStatus.Error
                        });
                    }
                    return new DataResult<CategoryDto>(ResultStatus.Error, "Kategori bulunamadı.", new CategoryDto
                    {
                        Category = null,
                        Message = "Kategori bulunamadı.",
                        ResultStatus = ResultStatus.Error
                    });
                }
                return new DataResult<CategoryDto>(ResultStatus.Error, "Kategori-id bulunamadı.", new CategoryDto
                {
                    Category = null,
                    Message = "Kategori-id bulunamadı.",
                    ResultStatus = ResultStatus.Error
                });
            }
            catch (Exception ex)
            {
                return new DataResult<CategoryDto>(ResultStatus.TryCatch, "try-catch", new CategoryDto
                {
                    Category = null,
                    Message = "try-catch : " + ex.Message,
                    ResultStatus = ResultStatus.TryCatch
                }, ex);
            }
        }

        public async Task<IDataResult<CategoryDto>> Get(int categoryId)
        {
            try
            {
                if (await _unitOfWork.Categories.AnyAsync(x => x.Id == categoryId))
                {
                    var getCategory = await _unitOfWork.Categories.GetAsync(x => x.Id == categoryId, x => x.Articles);
                    if (getCategory != null)
                    {
                        return new DataResult<CategoryDto>(ResultStatus.Success, new CategoryDto
                        {
                            Category = getCategory,
                            ResultStatus = ResultStatus.Success
                        });
                    }
                    return new DataResult<CategoryDto>(ResultStatus.Error, "Kategori bulunamadı.", new CategoryDto
                    {
                        Category = null,
                        Message = "Kategori bulunamadı.",
                        ResultStatus = ResultStatus.Error
                    });
                }
                return new DataResult<CategoryDto>(ResultStatus.Error, "Kategori-id bulunamadı.", new CategoryDto
                {
                    Category = null,
                    Message = "Kategori-id bulunamadı.",
                    ResultStatus = ResultStatus.Error
                }, null);
            }
            catch (Exception ex)
            {
                return new DataResult<CategoryDto>(ResultStatus.TryCatch, "try-catch", new CategoryDto
                {
                    Category = null,
                    Message = "try-catch : " + ex.Message,
                    ResultStatus = ResultStatus.TryCatch
                }, ex);
            }
        }

        public async Task<IDataResult<CategoryListDto>> GetAll()
        {
            try
            {
                var categories = await _unitOfWork.Categories.GetAllAsync(null, x => x.Articles);
                if (categories.Count > 0)
                {
                    return new DataResult<CategoryListDto>(ResultStatus.Success, new CategoryListDto
                    {
                        Categories = categories,
                        ResultStatus = ResultStatus.Success
                    });
                }
                return new DataResult<CategoryListDto>(ResultStatus.Error, "Kategori bulunamadı.", new CategoryListDto
                {
                    Categories = null,
                    Message = "Kategori bulunamadı.",
                    ResultStatus = ResultStatus.Error
                });
            }
            catch (Exception ex)
            {
                return new DataResult<CategoryListDto>(ResultStatus.TryCatch, "try-catch", new CategoryListDto
                {
                    Categories = null,
                    Message = "try-catch : " + ex.Message,
                    ResultStatus = ResultStatus.TryCatch
                }, ex);
            }
        }

        public async Task<IDataResult<CategoryListDto>> GetAllByNonDeleted()
        {
            try
            {
                var categories = await _unitOfWork.Categories.GetAllAsync(x => !x.IsDeleted, x => x.Articles);
                if (categories.Count > 0)
                {
                    return new DataResult<CategoryListDto>(ResultStatus.Success, new CategoryListDto
                    {
                        Categories = categories,
                        ResultStatus = ResultStatus.Success
                    });
                }
                return new DataResult<CategoryListDto>(ResultStatus.Error, "Kategori bulunamdı.", new CategoryListDto
                {
                    Categories = null,
                    Message = "Kategori bulunamadı.",
                    ResultStatus = ResultStatus.Error
                });
            }
            catch (Exception ex)
            {
                return new DataResult<CategoryListDto>(ResultStatus.TryCatch, "try-catch", new CategoryListDto
                {
                    Categories = null,
                    Message = "try-catch : " + ex.Message,
                    ResultStatus = ResultStatus.TryCatch
                }, ex);
            }
        }

        public async Task<IDataResult<CategoryListDto>> GetAllByNonDeletedAndActive()
        {
            try
            {
                var categories = await _unitOfWork.Categories.GetAllAsync(x => !x.IsDeleted && x.IsActive, x => x.Articles);
                if (categories.Count > 0)
                {
                    return new DataResult<CategoryListDto>(ResultStatus.Success, new CategoryListDto
                    {
                        Categories = categories,
                        ResultStatus = ResultStatus.Success
                    });
                }
                return new DataResult<CategoryListDto>(ResultStatus.Error, "Kategori bulunamdı", new CategoryListDto
                {
                    Categories = null,
                    Message = "Kategori bulunamadı.",
                    ResultStatus = ResultStatus.Error
                });
            }
            catch (Exception ex)
            {
                return new DataResult<CategoryListDto>(ResultStatus.TryCatch, "try-catch", new CategoryListDto
                {
                    Categories = null,
                    Message = "try-catch : " + ex.Message,
                    ResultStatus = ResultStatus.TryCatch
                }, ex);
            }
        }

        public async Task<IDataResult<CategoryUpdateDto>> GetCategoryUpdateDto(int categoryId)
        {
            try
            {
                if (await _unitOfWork.Categories.AnyAsync(x => x.Id == categoryId))
                {
                    var category = await _unitOfWork.Categories.GetAsync(x => x.Id == categoryId);
                    var categoryUpdateDto = _mapper.Map<CategoryUpdateDto>(category);
                    return new DataResult<CategoryUpdateDto>(ResultStatus.Success, categoryUpdateDto);
                }
                return new DataResult<CategoryUpdateDto>(ResultStatus.Error, "Kategori bulunamadı.", null);
            }
            catch (Exception ex)
            {
                return new DataResult<CategoryUpdateDto>(ResultStatus.TryCatch, "try-catch", null, ex);
            }
        }

        public async Task<IResult> HardDelete(int categoryId)
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
                            return new Result(ResultStatus.Success, $"{category.Name} kategorisi başarılı bir şekilde databaseden silindi.");
                        }
                        return new Result(ResultStatus.Error, $"{category.Name} kategorisi databaseden silinemedi.");
                    }
                    return new Result(ResultStatus.Error, "Kategori bulunamadı.");
                }
                return new Result(ResultStatus.Error, "Kategori-id bulunamadı.");
            }
            catch (Exception ex)
            {
                return new Result(ResultStatus.TryCatch, "try-catch", ex);
            }
        }

        public async Task<IDataResult<CategoryDto>> Update(CategoryUpdateDto categoryUpdateDto, string modifiedByName)
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
                        return new DataResult<CategoryDto>(ResultStatus.Success, $"{updadetCategory.Name} kategorisi başarılı bir şekilde güncellendi.", new CategoryDto
                        {
                            Category = updadetCategory,
                            Message = $"{updadetCategory.Name} kategorisi başarılı bir şekilde güncellendi.",
                            ResultStatus = ResultStatus.Success
                        });
                    }
                    return new DataResult<CategoryDto>(ResultStatus.Error, "Kategori güncellenemedi.", new CategoryDto
                    {
                        Category = null,
                        Message = "Kategori güncellenemedi.",
                        ResultStatus = ResultStatus.Error
                    });
                }
                return new DataResult<CategoryDto>(ResultStatus.Error, "Kategori bulunamadı.", new CategoryDto
                {
                    Category = null,
                    Message = "Kategori bulunamadı.",
                    ResultStatus = ResultStatus.Error
                });
            }
            catch (Exception ex)
            {
                return new DataResult<CategoryDto>(ResultStatus.TryCatch, "try-catch", new CategoryDto
                {
                    Category = null,
                    Message = "try-catch : " + ex.Message,
                    ResultStatus = ResultStatus.TryCatch
                }, ex);
            }
        }
    }
}
