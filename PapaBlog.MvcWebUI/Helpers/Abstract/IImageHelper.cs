using Microsoft.AspNetCore.Http;
using PapaBlog.Dtos.Concrete.ImageDtos;
using PapaBlog.Entities.ComplexTypes;
using PapaBlog.Shared.Utilities.Results.Abstract;
using System.Threading.Tasks;

namespace PapaBlog.MvcWebUI.Helpers.Abstract
{
    public interface IImageHelper
    {
        Task<IDataResult<UploadedImageDto>> Upload(string name, IFormFile pictureFile, PictureType pictureType, string folderName = null);
        IDataResult<DeletedImageDto> Delete(string pictureName);
    }
}