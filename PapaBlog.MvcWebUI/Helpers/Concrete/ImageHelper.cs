using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using PapaBlog.Dtos.Concrete.ImageDtos;
using PapaBlog.MvcWebUI.Helpers.Abstract;
using PapaBlog.Shared.Utilities.Extensions;
using PapaBlog.Shared.Utilities.Results.Abstract;
using PapaBlog.Shared.Utilities.Results.ComplexTypes;
using PapaBlog.Shared.Utilities.Results.Concrete;
using System;
using System.IO;
using System.Threading.Tasks;

namespace PapaBlog.MvcWebUI.Helpers.Concrete
{
    public class ImageHelper : IImageHelper
    {
        private readonly IWebHostEnvironment _webHost;
        private readonly string _wwwRoot;
        private readonly string imgFolder = "img";

        public ImageHelper(IWebHostEnvironment webHost)
        {
            _webHost = webHost;
            _wwwRoot = _webHost.WebRootPath;
        }

        public IDataResult<DeletedImageDto> Delete(string pictureName)
        {
            var fileToDelete = Path.Combine($"{_wwwRoot}/{imgFolder}/", pictureName);
            if (File.Exists(fileToDelete))
            {
                FileInfo fileInfo = new(fileToDelete);
                DeletedImageDto deletedImageDto = new()
                {
                    FullName = pictureName,
                    Extension = fileInfo.Extension,
                    Path = fileInfo.FullName,
                    Size = fileInfo.Length
                };
                File.Delete(fileToDelete);
                return new DataResult<DeletedImageDto>(ResultStatus.Success, deletedImageDto);
            }
            else
                return new DataResult<DeletedImageDto>(ResultStatus.Error, "Böyle bir resim bulunamdı.", null);
        }

        public async Task<IDataResult<UploadedImageDto>> UploadUserImage(string userName, IFormFile pictureFile, string folderName = "userImages")
        {
            if (!Directory.Exists($"{_wwwRoot}/{imgFolder}/{folderName}"))
                Directory.CreateDirectory($"{_wwwRoot}/{imgFolder}/{folderName}");

            DateTime dateTime = new();
            var oldFileName = Path.GetFileNameWithoutExtension(pictureFile.FileName);
            var fileExtension = Path.GetExtension(pictureFile.FileName);
            var newFileName = $"{userName}_{dateTime.FullDateAndTimeStringWithUnderScore()}{fileExtension}";
            var path = Path.Combine($"{_wwwRoot}/{imgFolder}/{folderName}", newFileName);

            await using (FileStream stream = new(path, FileMode.Create))
                await pictureFile.CopyToAsync(stream);

            return new DataResult<UploadedImageDto>(ResultStatus.Success, $"{userName} adlı kullanıcı resimi başarılı bir şekilde yüklemiştir.", new UploadedImageDto
            {
                FullName = $"{folderName}/{newFileName}",
                OldName = oldFileName,
                Extension = fileExtension,
                FolderName = folderName,
                Path = path,
                Size = pictureFile.Length
            });
        }
    }
}