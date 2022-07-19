using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using PapaBlog.Dtos.Concrete.ArticleDtos;
using PapaBlog.Entities.ComplexTypes;
using PapaBlog.Entities.Concrete;
using PapaBlog.MvcWebUI.Areas.Admin.Models;
using PapaBlog.MvcWebUI.Helpers.Abstract;
using PapaBlog.Services.Abstract;
using PapaBlog.Shared.Utilities.Results.ComplexTypes;
using PapaBlog.Shared.Utilities.Results.Concrete;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
namespace PapaBlog.MvcWebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin, Editor")]
    public class ArticleController : BaseController
    {
        private readonly IArticleService _articleService;
        private readonly ICategoryService _categoryService;
        private readonly IToastNotification _toastNotification;

        public ArticleController(IArticleService articleService, ICategoryService categoryService, UserManager<User> userManager, IMapper mapper, IImageHelper imageHelper, IToastNotification toastNotification)
            : base(userManager, mapper, imageHelper)
        {
            _articleService = articleService;
            _categoryService = categoryService;
            _toastNotification = toastNotification;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _articleService.GetAllByNonDeletedAsync();
            if (result.ResultStatus == ResultStatus.Success)
                return View(result.Data);
            return NotFound();
        }

        [HttpGet]
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
                    _toastNotification.AddSuccessToastMessage(result.Message, new ToastrOptions
                    {
                        Title = "Başarılı"
                    });
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

        [HttpGet]
        public async Task<IActionResult> Update(int articleId)
        {
            var articleResult = await _articleService.GetArticleUpdateDtoAsycn(articleId);
            var categoriesResult = await _categoryService.GetAllByNonDeletedAndActiveAsync();
            if (articleResult.ResultStatus == ResultStatus.Success && categoriesResult.ResultStatus == ResultStatus.Success)
            {
                var articleUpdateViewModel = this.Mapper.Map<ArticleUpdateViewModel>(articleResult.Data);
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
                    var uploadedImageResult = await this.ImageHelper.Upload(model.Title, model.ThumbnailFile, PictureType.Post);
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

                    _toastNotification.AddSuccessToastMessage(result.Message, new ToastrOptions
                    {
                        Title = "Başarılı"
                    });
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

        [HttpPost]
        public async Task<JsonResult> Delete(int articleId)
        {
            var article = await _articleService.DeleteAsync(articleId, LoggedInUser.UserName);
            var articleResult = JsonSerializer.Serialize(article, new JsonSerializerOptions { ReferenceHandler = ReferenceHandler.Preserve });
            return Json(articleResult);
        }

        [HttpGet]
        public async Task<JsonResult> GetAllArticles()
        {
            var articles = await _articleService.GetAllByNonDeletedAndActiveAsync();
            var articleResult = JsonSerializer.Serialize(articles, new JsonSerializerOptions { ReferenceHandler = ReferenceHandler.Preserve });
            _toastNotification.AddInfoToastMessage("Makale listesi başarılı bir şekilde yenilendi.", new ToastrOptions
            {
                Title = "Başarılı"
            });
            return Json(articleResult);
        }
    }
}
