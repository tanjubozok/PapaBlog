using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PapaBlog.Entities.Concrete;
using PapaBlog.MvcWebUI.Areas.Admin.Models;
using PapaBlog.Services.Abstract;
using PapaBlog.Shared.Utilities.Results.ComplexTypes;
using System.Threading.Tasks;

namespace PapaBlog.MvcWebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Editor")]
    public class HomeController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly ICommentService _commentService;
        private readonly IArticleService _articleService;
        private readonly UserManager<User> _userManager;

        public HomeController(ICategoryService categoryService, ICommentService commentService, IArticleService articleService, UserManager<User> userManager)
        {
            _categoryService = categoryService;
            _commentService = commentService;
            _articleService = articleService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var categoriesCountResult = await _categoryService.CountByNonDeleteAsync();
            var commentsCountResult = await _commentService.CountByNonDeleteAsync();
            var articlesCountResult = await _articleService.CountByNonDeleteAsync();
            var userCount = await _userManager.Users.CountAsync();
            var articlesResult = await _articleService.GetAllAsync();

            if (categoriesCountResult.ResultStatus == ResultStatus.Success
                && commentsCountResult.ResultStatus == ResultStatus.Success
                && articlesCountResult.ResultStatus == ResultStatus.Success
                && userCount > -1
                && articlesResult.ResultStatus == ResultStatus.Success)
            {
                return View(new DashboardViewModel
                {
                    Articles = articlesResult.Data,
                    ArticlesCount = articlesCountResult.Data,
                    CategoriesCount = categoriesCountResult.Data,
                    CommentsCount = commentsCountResult.Data,
                    UsersCount = userCount
                });
            }
            return NotFound();
        }
    }
}
