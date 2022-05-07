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

        public async Task<IResult> Add(CategoryAddDto categoryAddDto, string createByName)
        {
            try
            {
                var category = _mapper.Map<Category>(categoryAddDto);
                category.ModifiedByName = createByName;
                category.CreatedByName = createByName;
                await _unitOfWork.Categories.AddAsync(category);
                var result = await _unitOfWork.SaveAsync();
                if (result == 1)
                {
                    return new Result(ResultStatus.Success, $"{categoryAddDto.Name} kategorisi başarılı bir şekilde eklendi.");
                }
                return new Result(ResultStatus.Error, "Kategori eklenemedi.");
            }
            catch (Exception ex)
            {
                return new Result(ResultStatus.Error, "try-catch", ex);
            }
        }

        public async Task<IResult> Delete(int categoryId, string modifiedByName)
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
                        await _unitOfWork.Categories.UpdateAsycn(category);
                        var result = await _unitOfWork.SaveAsync();
                        if (result == 1)
                        {
                            return new Result(ResultStatus.Success, $"{category.Name} kategorisi başarılı bir şekilde silindi.");
                        }
                        return new Result(ResultStatus.Error, "Kategori silinemedi.");
                    }
                    return new Result(ResultStatus.Error, "Kategori bulunamadı.");
                }
                return new Result(ResultStatus.Error, "Kategori-id bulunamadı.");
            }
            catch (Exception ex)
            {
                return new Result(ResultStatus.Error, "try-catch", ex);
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
                return new DataResult<CategoryDto>(ResultStatus.Error, "try-catch", new CategoryDto
                {
                    Category = null,
                    Message = "try-catch",
                    ResultStatus = ResultStatus.Error
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
                return new DataResult<CategoryListDto>(ResultStatus.Error, "try-catch", new CategoryListDto
                {
                    Categories = null,
                    Message = "try-catch",
                    ResultStatus = ResultStatus.Error
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
                    ResultStatus = ResultStatus.Error,
                    Message = "Kategori bulunamadı."
                });
            }
            catch (Exception ex)
            {
                return new DataResult<CategoryListDto>(ResultStatus.Error, "try-catch", new CategoryListDto
                {
                    Categories = null,
                    Message = "try-catch",
                    ResultStatus = ResultStatus.Error
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
                    ResultStatus = ResultStatus.Error,
                    Message = "Kategori bulunamadı."
                });
            }
            catch (Exception ex)
            {
                return new DataResult<CategoryListDto>(ResultStatus.Error, "try-catch", new CategoryListDto
                {
                    Categories = null,
                    Message = "try-catch",
                    ResultStatus = ResultStatus.Error
                }, ex);
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
                return new Result(ResultStatus.Error, "try-catch", ex);
            }
        }

        public async Task<IResult> Update(CategoryUpdateDto categoryUpdateDto, string modifiedByName)
        {
            try
            {
                if (await _unitOfWork.Categories.AnyAsync(x => x.Id == categoryUpdateDto.Id))
                {
                    var category = _mapper.Map<Category>(categoryUpdateDto);
                    category.ModifiedByName = modifiedByName;
                    await _unitOfWork.Categories.UpdateAsycn(category);
                    var result = await _unitOfWork.SaveAsync();
                    if (result == 1)
                    {
                        return new Result(ResultStatus.Success, $"{categoryUpdateDto.Name} kategorisi başarılı bir şekilde güncellendi.");
                    }
                    return new Result(ResultStatus.Error, "Kategori güncellenemedi.");
                }
                return new Result(ResultStatus.Error, "Kategori bulunamadı.");
            }
            catch (Exception ex)
            {
                return new Result(ResultStatus.Error, "try-catch", ex);
            }
        }
    }
}
