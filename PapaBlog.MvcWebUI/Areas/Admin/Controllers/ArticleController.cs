using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PapaBlog.Dtos.Concrete.ArticleDtos;
using PapaBlog.Entities.ComplexTypes;
using PapaBlog.MvcWebUI.Areas.Admin.Models;
using PapaBlog.MvcWebUI.Helpers.Abstract;
using PapaBlog.Services.Abstract;
using PapaBlog.Shared.Utilities.Results.ComplexTypes;
using System.Threading.Tasks;

namespace PapaBlog.MvcWebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ArticleController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        private readonly IImageHelper _imageHelper;

        public ArticleController(IArticleService articleService, ICategoryService categoryService, IMapper mapper, IImageHelper imageHelper)
        {
            _articleService = articleService;
            _categoryService = categoryService;
            _mapper = mapper;
            _imageHelper = imageHelper;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _articleService.GetAllByNonDeletedAsync();
            if (result.ResultStatus == ResultStatus.Success)
                return View(result.Data);
            return NotFound();
        }

        public async Task<IActionResult> Add()
        {
            var result = await _categoryService.GetAllByNonDeletedAsync();
            if (result.ResultStatus == ResultStatus.Success)
            {
                return View(new ArticleAddViewModel
                {
                    Categories = result.Data.Categories
                });
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Add(ArticleAddViewModel model)
        {
            if (ModelState.IsValid)
            {
                var articleAddDto = _mapper.Map<ArticleAddDto>(model);
                var imageResult = await _imageHelper.Upload(model.Title, model.ThumbnailFile, PictureType.Post);
                articleAddDto.Thumbnail = imageResult.Data.FullName;
                var result = await _articleService.AddAsync(articleAddDto, "Admin");
                if (result.ResultStatus == ResultStatus.Success)
                {
                    TempData.Add("SuccessMessage", result.Message);
                    return RedirectToAction("Index", "Article");
                }
                else
                {
                    ModelState.AddModelError("", result.Message);
                }
            }
            return View(model);
        }
    }
}
