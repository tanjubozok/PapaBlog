using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using PapaBlog.Dtos.Concrete.ImageDtos;
using PapaBlog.Entities.ComplexTypes;
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
        private readonly string _wwwroot;
        private const string imgFolder = "img";
        private const string userImagesFolder = "userImages";
        private const string postImagesFolder = "postImages";

        public ImageHelper(IWebHostEnvironment webHost)
        {
            _webHost = webHost;
            _wwwroot = _webHost.WebRootPath;
        }

        public IDataResult<DeletedImageDto> Delete(string pictureName)
        {
            var fileToDelete = Path.Combine($"{_wwwroot}/{imgFolder}/", pictureName);
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

        public async Task<IDataResult<UploadedImageDto>> Upload(string name, IFormFile pictureFile, PictureType pictureType, string folderName = null)
        {
            /* Eğer folderName değişkeni null gelir ise, o zaman resim tipine göre (PictureType) klasör adı ataması yapılır. */
            folderName ??= pictureType == PictureType.User ? userImagesFolder : postImagesFolder;

            /* Eğer folderName değişkeni ile gelen klasör adı sistemimizde mevcut değilse, yeni bir klasör oluşturulur. */
            if (!Directory.Exists($"{_wwwroot}/{imgFolder}/{folderName}"))
            {
                Directory.CreateDirectory($"{_wwwroot}/{imgFolder}/{folderName}");
            }

            /* Resimin yüklenme sırasındaki ilk adı oldFileName adlı değişkene atanır. */
            string oldFileName = Path.GetFileNameWithoutExtension(pictureFile.FileName);

            /* Resimin uzantısı fileExtension adlı değişkene atanır. */
            string fileExtension = Path.GetExtension(pictureFile.FileName);

            DateTime dateTime = DateTime.Now;
            /*
            // Parametre ile gelen değerler kullanılarak yeni bir resim adı oluşturulur.
            */
            string newFileName = $"{name}_{dateTime.FullDateAndTimeStringWithUnderScore()}{fileExtension}";

            /* Kendi parametrelerimiz ile sistemimize uygun yeni bir dosya yolu (path) oluşturulur. */
            var path = Path.Combine($"{_wwwroot}/{imgFolder}/{folderName}", newFileName);

            /* Sistemimiz için oluşturulan yeni dosya yoluna resim kopyalanır. */
            await using (var stream = new FileStream(path, FileMode.Create))
            {
                await pictureFile.CopyToAsync(stream);
            }

            /* Resim tipine göre kullanıcı için bir mesaj oluşturulur. */
            string nameMessage = pictureType == PictureType.User
                ? $"{name} adlı kullanıcının resimi başarıyla yüklenmiştir."
                : $"{name} adlı makalenin resimi başarıyla yüklenmiştir.";

            return new DataResult<UploadedImageDto>(ResultStatus.Success, nameMessage, new UploadedImageDto
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