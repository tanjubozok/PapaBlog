using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PapaBlog.Dtos.Concrete.CategoryDtos
{
    public class CategoryAddDto
    {
        [DisplayName("Kategori Adı")]
        [Required(ErrorMessage = "{0} boş geçilemez.")]
        [MaxLength(70, ErrorMessage = "{0} en fazla {1} karakter olabilir.")]
        [MinLength(3, ErrorMessage = "{0} en az {1} karakter olabilir.")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [DisplayName("Kategori Açıklama")]
        [MaxLength(500, ErrorMessage = "{0} en fazla {1} karakter olabilir.")]
        [MinLength(3, ErrorMessage = "{0} en az {1} karakter olabilir.")]
        [DataType(DataType.Text)]
        public string Description { get; set; }

        [DisplayName("Kategori Özel Not")]
        [MaxLength(500, ErrorMessage = "{0} en fazla {1} karakter olabilir.")]
        [MinLength(3, ErrorMessage = "{0} en az {1} karakter olabilir.")]
        [DataType(DataType.Text)]
        public string Note { get; set; }

        [DisplayName("Aktif Mi?")]
        [Required(ErrorMessage = "{0} boş geçilemez.")]
        public bool IsActive { get; set; }
    }
}
