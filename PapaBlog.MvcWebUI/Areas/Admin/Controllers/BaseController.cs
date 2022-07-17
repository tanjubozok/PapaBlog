using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PapaBlog.Entities.Concrete;
using PapaBlog.MvcWebUI.Helpers.Abstract;

namespace PapaBlog.MvcWebUI.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        public BaseController(UserManager<User> userManager, IMapper mapper, IImageHelper imageHelper)
        {
            this.UserManager = userManager;
            this.Mapper = mapper;
            this.ImageHelper = imageHelper;
        }

        protected UserManager<User> UserManager { get; }
        protected IMapper Mapper { get; }
        protected IImageHelper ImageHelper { get; }
        protected User LoggedInUser => this.UserManager.GetUserAsync(this.HttpContext.User).Result;
    }
}
