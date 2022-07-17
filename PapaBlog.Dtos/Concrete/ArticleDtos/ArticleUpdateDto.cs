using PapaBlog.Entities.Concrete;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PapaBlog.Dtos.Concrete.ArticleDtos
{
    public class ArticleUpdateDto
    {
        [Required]
        public int Id { get; set; }

        [DisplayName("Başlık")]
        [Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        [MaxLength(100, ErrorMessage = "{0} alanı en fazla {1} karakter olmalıdır.")]
        [MinLength(5, ErrorMessage = "{0} alanı en az {1} karakter olmalıdır.")]
        public string Title { get; set; }

        [DisplayName("İçerik")]
        [Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        [MinLength(10, ErrorMessage = "{0} alanı en az {1} karakter olmalıdır.")]
        public string Content { get; set; }

        [DisplayName("Thumbnail")]
        [Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        [MaxLength(250, ErrorMessage = "{0} alanı en fazla {1} karakter olmalıdır.")]
        [MinLength(5, ErrorMessage = "{0} alanı en az {1} karakter olmalıdır.")]
        public string Thumbnail { get; set; }

        [DisplayName("Tarih")]
        [Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Date { get; set; }

        [DisplayName("Seo Yazar")]
        [Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        [MaxLength(50, ErrorMessage = "{0} alanı en fazla {1} karakter olmalıdır.")]
        [MinLength(1, ErrorMessage = "{0} alanı en az {1} karakter olmalıdır.")]
        public string SeoAuther { get; set; }

        [DisplayName("Seo Açıklama")]
        [Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        [MaxLength(150, ErrorMessage = "{0} alanı en fazla {1} karakter olmalıdır.")]
        [MinLength(5, ErrorMessage = "{0} alanı en az {1} karakter olmalıdır.")]
        public string SeoDescription { get; set; }

        [DisplayName("Seo Etiketler")]
        [Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        [MaxLength(70, ErrorMessage = "{0} alanı en fazla {1} karakter olmalıdır.")]
        [MinLength(5, ErrorMessage = "{0} alanı en az {1} karakter olmalıdır.")]
        public string SeoTags { get; set; }

        [DisplayName("Kategori")]
        [Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        [DisplayName("Aktif Mi?")]
        [Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        public bool IsActive { get; set; }

        [DisplayName("Silinsin Mi?")]
        [Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        public bool IsDelete { get; set; }

        public int UserId { get; set; }
    }
}
