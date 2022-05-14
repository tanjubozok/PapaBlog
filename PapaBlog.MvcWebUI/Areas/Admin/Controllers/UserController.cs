using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PapaBlog.Dtos.Concrete.UserDtos;
using PapaBlog.Entities.Concrete;
using PapaBlog.MvcWebUI.Areas.Admin.Models;
using PapaBlog.Shared.Utilities.Extensions;
using PapaBlog.Shared.Utilities.Results.ComplexTypes;
using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PapaBlog.MvcWebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHost;

        public UserController(UserManager<User> userManager, IWebHostEnvironment webHost, IMapper mapper)
        {
            _userManager = userManager;
            _webHost = webHost;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            return View(new UserListDto
            {
                Users = users,
                Message = "Başarılı",
                ResultStatus = ResultStatus.Success
            });
        }

        public async Task<JsonResult> GetAllUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            var userListDto = JsonSerializer.Serialize(new UserListDto
            {
                Users = users,
                ResultStatus = ResultStatus.Success
            }, new JsonSerializerOptions { ReferenceHandler = ReferenceHandler.Preserve });
            return Json(userListDto);
        }

        public async Task<JsonResult> Delete(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                var deletedUser = JsonSerializer.Serialize(new UserDto
                {
                    ResultStatus = ResultStatus.Success,
                    Users = user,
                    Message = $"{user.UserName} adlı kullanıcı başarılı bir şekilde siinmiştir."
                });
                return Json(deletedUser);
            }
            string errorMessage = "";
            foreach (var item in result.Errors)
                errorMessage = $"* {item.Description}";

            var deletedUserErrorModel = JsonSerializer.Serialize(new UserDto
            {
                ResultStatus = ResultStatus.Error,
                Message = $"{user.UserName} adlı kullanıcı silinirken bir hata oluştu.\n{errorMessage}",
                Users = user
            });
            return Json(deletedUserErrorModel);
        }

        public IActionResult Add()
        {
            return PartialView("_PartialAddUser");
        }

        [HttpPost]
        public async Task<JsonResult> Add(UserAddDto userAddDto)
        {
            if (ModelState.IsValid)
            {
                userAddDto.Picture = await ImagesUpload(userAddDto.UserName, userAddDto.PictureFile);
                var user = _mapper.Map<User>(userAddDto);
                var result = await _userManager.CreateAsync(user, userAddDto.Password);
                if (result.Succeeded)
                {
                    var userAddAjaxViewModel = JsonSerializer.Serialize(new UserAddAjaxViewModel
                    {
                        UserDto = new UserDto
                        {
                            ResultStatus = ResultStatus.Success,
                            Message = $"{user.UserName} adlı kullanıcı adına sahip, kullanıcı başarıyla eklenmiştir.",
                            Users = user
                        },
                        UserAddPartial = await this.RenderViewToStringAsync("_PartialAddUser", userAddDto)
                    });
                    return Json(userAddAjaxViewModel);
                }
                else
                {
                    foreach (var item in result.Errors)
                        ModelState.AddModelError("", item.Description);

                    var userAddAjaxViewErrorModel = JsonSerializer.Serialize(new UserAddAjaxViewModel
                    {
                        UserAddDto = userAddDto,
                        UserAddPartial = await this.RenderViewToStringAsync("_PartialAddUser", userAddDto)
                    });
                    return Json(userAddAjaxViewErrorModel);
                }
            }
            var userAddAjaxViewModelStataErrorModel = JsonSerializer.Serialize(new UserAddAjaxViewModel
            {
                UserAddDto = userAddDto,
                UserAddPartial = await this.RenderViewToStringAsync("_PartialAddUser", userAddDto)
            });
            return Json(userAddAjaxViewModelStataErrorModel);
        }

        public async Task<string> ImagesUpload(string userName, IFormFile pictureFile)
        {
            DateTime dateTime = new DateTime();
            string wwwroot = _webHost.WebRootPath;
            //string fileName2 = Path.GetFileNameWithoutExtension(pictureFile.FileName);
            string fileExtension = Path.GetExtension(pictureFile.FileName);
            string fileName = $"{userName}_{dateTime.FullDateAndTimeStringWithUnderScore()}{fileExtension}";
            var path = Path.Combine($"{wwwroot}/img", fileName);

            await using (FileStream stream = new(path, FileMode.Create))
                await pictureFile.CopyToAsync(stream);

            return fileName;
        }

        public bool ImageDelete(string pictureName)
        {
            string wwwRoot = _webHost.WebRootPath;
            var fileToDelete = Path.Combine($"{wwwRoot}/img", pictureName);
            if (System.IO.File.Exists(fileToDelete))
            {
                System.IO.File.Delete(fileToDelete);
                return true;
            }
            return false;
        }

        public async Task<PartialViewResult> Update(int userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);
            var userUpdateDto = _mapper.Map<UserUpdateDto>(user);
            return PartialView("_PartialUpdateUser", userUpdateDto);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UserUpdateDto userUpdateDto)
        {
            if (ModelState.IsValid)
            {
                bool isNewPictureUpload = false;
                var oldUser = await _userManager.FindByIdAsync(userUpdateDto.Id.ToString());
                var oldUserPicture = oldUser.Picture;
                if (userUpdateDto.PictureFile != null)
                {
                    userUpdateDto.Picture = await ImagesUpload(userUpdateDto.UserName, userUpdateDto.PictureFile);
                    isNewPictureUpload = true;
                }
                var updatedUser = _mapper.Map<UserUpdateDto, User>(userUpdateDto, oldUser);
                var result = await _userManager.UpdateAsync(updatedUser);
                if (result.Succeeded)
                {
                    if (isNewPictureUpload)
                        ImageDelete(oldUserPicture);

                    var userUpdateViewModel = JsonSerializer.Serialize(new UserUpdateAjaxViewModel
                    {
                        UserDto = new UserDto
                        {
                            ResultStatus = ResultStatus.Success,
                            Message = $"{updatedUser.UserName} adlı kullanıcı başarıyla güncellenmiştir.",
                            Users = updatedUser
                        },
                        UserUpdatePartial = await this.RenderViewToStringAsync("_PartialUpdateUser", userUpdateDto)
                    });
                    return Json(userUpdateViewModel);
                }
                else
                {
                    foreach (var item in result.Errors)
                        ModelState.AddModelError("", item.Description);

                    var userUpdateErrorViewModel = JsonSerializer.Serialize(new UserUpdateAjaxViewModel
                    {
                        UserUpdateDto = userUpdateDto,
                        UserUpdatePartial = await this.RenderViewToStringAsync("_PartialUpdateUser", userUpdateDto)
                    });
                    return Json(userUpdateErrorViewModel);
                }
            }
            var userUpdateErrorModelStateViewModel = JsonSerializer.Serialize(new UserUpdateAjaxViewModel
            {
                UserUpdateDto = userUpdateDto,
                UserUpdatePartial = await this.RenderViewToStringAsync("_PartialUpdateUser", userUpdateDto)
            });
            return Json(userUpdateErrorModelStateViewModel);
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Logout()
        {
            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
