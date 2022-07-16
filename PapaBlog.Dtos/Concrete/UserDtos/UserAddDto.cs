using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PapaBlog.Dtos.Concrete.UserDtos
{
    public class UserAddDto
    {
        [DisplayName("Kullanıcı Adı")]
        [Required(ErrorMessage = "{0} boş geçilemez.")]
        [MaxLength(50, ErrorMessage = "{0} en fazla {1} karakter olabilir.")]
        [MinLength(3, ErrorMessage = "{0} en az {1} karakter olabilir.")]
        [DataType(DataType.Text)]
        public string UserName { get; set; }

        [DisplayName("E-posta Adresi")]
        [Required(ErrorMessage = "{0} boş geçilemez.")]
        [MaxLength(100, ErrorMessage = "{0} en fazla {1} karakter olabilir.")]
        [MinLength(10, ErrorMessage = "{0} en az {1} karakter olabilir.")]
        [EmailAddress]
        public string Email { get; set; }

        [DisplayName("Şifre")]
        [Required(ErrorMessage = "{0} boş geçilemez.")]
        [MaxLength(50, ErrorMessage = "{0} en fazla {1} karakter olabilir.")]
        [MinLength(5, ErrorMessage = "{0} en az {1} karakter olabilir.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("Telefon Numarası")]
        [Required(ErrorMessage = "{0} boş geçilemez.")]
        [MaxLength(13, ErrorMessage = "{0} en fazla {1} karakter olabilir.")]
        [MinLength(13, ErrorMessage = "{0} en az {1} karakter olabilir.")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [DisplayName("Resim")]
        [DataType(DataType.Upload)]
        public IFormFile PictureFile { get; set; }
        public string Picture { get; set; }
    }
}
