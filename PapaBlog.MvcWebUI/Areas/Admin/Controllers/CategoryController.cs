using Microsoft.AspNetCore.Mvc;
using PapaBlog.Dtos.Concrete.CategoryDtos;
using PapaBlog.MvcWebUI.Areas.Admin.Models;
using PapaBlog.Services.Abstract;
using PapaBlog.Shared.Utilities.Extensions;
using PapaBlog.Shared.Utilities.Results.ComplexTypes;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PapaBlog.MvcWebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _categoryService.GetAllByNonDeleted();
            return View(result.Data);
        }

        public IActionResult Add()
        {
            return PartialView("_PartialAddCategory");
        }

        [HttpPost]
        public async Task<IActionResult> Add(CategoryAddDto categoryAddDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _categoryService.Add(categoryAddDto, "System");
                if (result.ResultStatus == ResultStatus.Success)
                {
                    var categoryAddAjaxViewModel = JsonSerializer.Serialize(new CategoryAddAjaxViewModel
                    {
                        CategoryDto = result.Data,
                        CategoryAddPartial = await this.RenderViewToStringAsync("_PartialAddCategory", categoryAddDto)
                    });
                    return Json(categoryAddAjaxViewModel);
                }
            }
            var categoryAddErrorAjaxViewModel = JsonSerializer.Serialize(new CategoryAddAjaxViewModel
            {
                CategoryAddPartial = await this.RenderViewToStringAsync("_PartialAddCategory", categoryAddDto)
            });
            return Json(categoryAddErrorAjaxViewModel);
        }

        public async Task<JsonResult> GetAllCategories()
        {
            var result = await _categoryService.GetAllByNonDeleted();
            var categories = JsonSerializer.Serialize(result.Data, new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            });
            return Json(categories);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(int categoryId)
        {
            var result = await _categoryService.Delete(categoryId, "System");
            var category = JsonSerializer.Serialize(result.Data);
            return Json(category);
        }
    }
}