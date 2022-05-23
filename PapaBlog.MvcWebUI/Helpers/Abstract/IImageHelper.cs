using Microsoft.AspNetCore.Http;
using PapaBlog.Dtos.Concrete.ImageDtos;
using PapaBlog.Shared.Utilities.Results.Abstract;
using System.Threading.Tasks;

namespace PapaBlog.MvcWebUI.Helpers.Abstract
{
    public interface IImageHelper
    {
        Task<IDataResult<UploadedImageDto>> UploadUserImage(string userName, IFormFile pictureFile, string folderName = "userImages");
        IDataResult<DeletedImageDto> Delete(string pictureName);
    }
}