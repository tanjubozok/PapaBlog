using PapaBlog.Data.Abstract;
using PapaBlog.Dtos.Concrete;
using PapaBlog.Entities.Concrete;
using PapaBlog.Services.Abstract;
using PapaBlog.Shared.Utilities.Results.Abstract;
using PapaBlog.Shared.Utilities.Results.ComplexTypes;
using PapaBlog.Shared.Utilities.Results.Concrete;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PapaBlog.Services.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IResult> Add(CategoryAddDto categoryAddDto, string createByName)
        {
            try
            {
                await _unitOfWork.Categories.AddAsync(new Category
                {
                    Name = categoryAddDto.Name,
                    Description = categoryAddDto.Description,
                    Note = categoryAddDto.Note,
                    IsActive = categoryAddDto.IsActive,
                    CreatedByName = createByName,
                    CreatedDate = DateTime.Now,
                    ModifiedByName = createByName,
                    ModifiedDate = DateTime.Now,
                    IsDeleted = false
                });
                var category = await _unitOfWork.SaveAsync();
                if (category == 1)
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
            catch (Exception ex)
            {
                return new Result(ResultStatus.Error, "try-catch", ex);
            }
        }

        public async Task<IDataResult<Category>> Get(int categoryId)
        {
            try
            {
                var category = await _unitOfWork.Categories.GetAsync(x => x.Id == categoryId, x => x.Articles);
                if (category != null)
                {
                    return new DataResult<Category>(ResultStatus.Success, category);
                }
                return new DataResult<Category>(ResultStatus.Error, "Kategori bulunamadı.", null);
            }
            catch (Exception ex)
            {
                return new DataResult<Category>(ResultStatus.Error, "try-catch", null, ex);
            }
        }

        public async Task<IDataResult<IList<Category>>> GetAll()
        {
            try
            {
                var categories = await _unitOfWork.Categories.GetAllAsync(null, x => x.Articles);
                if (categories.Count > 0)
                {
                    return new DataResult<IList<Category>>(ResultStatus.Success, categories);
                }
                return new DataResult<IList<Category>>(ResultStatus.Error, "Kategori bulunamadı", null);
            }
            catch (Exception ex)
            {
                return new DataResult<IList<Category>>(ResultStatus.Error, "try-catch", null, ex);
            }
        }

        public async Task<IDataResult<IList<Category>>> GetAllByNonDeleted()
        {
            try
            {
                var categories = await _unitOfWork.Categories.GetAllAsync(x => !x.IsDeleted, x => x.Articles);
                if (categories.Count > 0)
                {
                    return new DataResult<IList<Category>>(ResultStatus.Success, categories);
                }
                return new DataResult<IList<Category>>(ResultStatus.Error, "Kategori bulunamdı.", null);
            }
            catch (Exception ex)
            {
                return new DataResult<IList<Category>>(ResultStatus.Error, "try-catch", null, ex);
            }
        }

        public async Task<IResult> HardDelete(int categoryId)
        {
            try
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
            catch (Exception ex)
            {
                return new Result(ResultStatus.Error, "try-catch", ex);
            }
        }

        public async Task<IResult> Update(CategoryUpdateDto categoryUpdateDto, string modifiedByName)
        {
            try
            {
                var category = await _unitOfWork.Categories.GetAsync(x => x.Id == categoryUpdateDto.Id);
                if (category != null)
                {
                    category.Name = categoryUpdateDto.Name;
                    category.Description = categoryUpdateDto.Description;
                    category.Note = categoryUpdateDto.Note;
                    category.IsActive = category.IsActive;
                    category.IsDeleted = category.IsDeleted;
                    category.ModifiedByName = modifiedByName;
                    category.ModifiedDate = DateTime.Now;
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
