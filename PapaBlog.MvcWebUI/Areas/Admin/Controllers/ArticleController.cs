using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PapaBlog.Dtos.Concrete.ArticleDtos;
using PapaBlog.Entities.ComplexTypes;
using PapaBlog.Entities.Concrete;
using PapaBlog.MvcWebUI.Areas.Admin.Models;
using PapaBlog.MvcWebUI.Helpers.Abstract;
using PapaBlog.MvcWebUI.Helpers.Concrete;
using PapaBlog.Services.Abstract;
using PapaBlog.Shared.Utilities.Results.ComplexTypes;
using System.Threading.Tasks;

namespace PapaBlog.MvcWebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ArticleController : BaseController
    {
        private readonly IArticleService _articleService;
        private readonly ICategoryService _categoryService;

        public ArticleController(IArticleService articleService, ICategoryService categoryService, UserManager<User> userManager, IMapper mapper, IImageHelper imageHelper)
            : base(userManager, mapper, imageHelper)
        {
            _articleService = articleService;
            _categoryService = categoryService;
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
            var result = await _categoryService.GetAllByNonDeletedAndActiveAsync();
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
                var articleAddDto = Mapper.Map<ArticleAddDto>(model);
                var imageResult = await ImageHelper.Upload(model.Title, model.ThumbnailFile, PictureType.Post);
                articleAddDto.Thumbnail = imageResult.Data.FullName;
                var result = await _articleService.AddAsync(articleAddDto, LoggedInUser.UserName, LoggedInUser.Id);
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
            var categoriesResult = await _categoryService.GetAllByNonDeletedAndActiveAsync();
            model.Categories = categoriesResult.Data.Categories;
            return View(model);
        }

        public async Task<IActionResult> Update(int articleId)
        {
            var articleResult = await _articleService.GetArticleUpdateDtoAsycn(articleId);
            var categoriesResult = await _categoryService.GetAllByNonDeletedAndActiveAsync();
            if (articleResult.ResultStatus == ResultStatus.Success && categoriesResult.ResultStatus == ResultStatus.Success)
            {
                var articleUpdateViewModel = Mapper.Map<ArticleUpdateViewModel>(articleResult.Data);
                articleUpdateViewModel.Categories = categoriesResult.Data.Categories;
                return View(articleUpdateViewModel);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(ArticleUpdateViewModel model)
        {
            if (ModelState.IsValid)
            {
                bool isNewThumbnailUploaded = false;
                var oldThumbnail = model.Thumbnail;
                if (model.ThumbnailFile != null)
                {
                    var uploadedImageResult = await ImageHelper.Upload(model.Title, model.ThumbnailFile, PictureType.Post);
                    model.Thumbnail = uploadedImageResult.ResultStatus == ResultStatus.Success
                        ? uploadedImageResult.Data.FullName
                        : "postImages/default.jpg";

                    if (oldThumbnail == "postImages/default.jpg")
                        isNewThumbnailUploaded = true;
                }
                var articleUpdateDto = Mapper.Map<ArticleUpdateDto>(model);
                var result = await _articleService.UpdateAsync(articleUpdateDto, LoggedInUser.UserName);
                if (result.ResultStatus == ResultStatus.Success)
                {
                    if (isNewThumbnailUploaded)
                        ImageHelper.Delete(oldThumbnail);

                    TempData.Add("SuccessMessage", result.Message);
                    return RedirectToAction("Index", "Article");
                }
                else
                {
                    ModelState.AddModelError("", result.Message);
                }
            }
            var categories = await _categoryService.GetAllByNonDeletedAndActiveAsync();
            model.Categories = categories.Data.Categories;
            return View(model);
        }
    }
}
