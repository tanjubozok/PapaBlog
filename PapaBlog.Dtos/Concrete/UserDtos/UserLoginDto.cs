using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PapaBlog.Dtos.Concrete.UserDtos
{
    public class UserLoginDto
    {
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

        [DisplayName("Beni Hatırla")]
        public bool RememberMe { get; set; }
    }
}
