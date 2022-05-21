using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PapaBlog.Dtos.Concrete.UserDtos
{
    public class UserPasswordChangeDto
    {
        public string UserName { get; set; }

        [DisplayName("Şifre")]
        [Required(ErrorMessage = "{0} boş geçilemez.")]
        [MaxLength(50, ErrorMessage = "{0} en fazla {1} karakter olabilir.")]
        [MinLength(5, ErrorMessage = "{0} en az {1} karakter olabilir.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("Yeni Şifre")]
        [Required(ErrorMessage = "{0} boş geçilemez.")]
        [MaxLength(50, ErrorMessage = "{0} en fazla {1} karakter olabilir.")]
        [MinLength(5, ErrorMessage = "{0} en az {1} karakter olabilir.")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [DisplayName("Yeni Şifre Tekrar")]
        [Required(ErrorMessage = "{0} boş geçilemez.")]
        [MaxLength(50, ErrorMessage = "{0} en fazla {1} karakter olabilir.")]
        [MinLength(5, ErrorMessage = "{0} en az {1} karakter olabilir.")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Yeni şifre ile Şifre tekrar uyuşmuyor.")]
        public string RepeatNewPassword { get; set; }
    }
}
