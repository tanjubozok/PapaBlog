using Microsoft.AspNetCore.Http;
using PapaBlog.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PapaBlog.MvcWebUI.Areas.Admin.Models
{
    public class ArticleUpdateViewModel
    {
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

        [DisplayName("Küçük Resim")]
        public string Thumbnail { get; set; }

        [DisplayName("Küçük Resim Ekle")]
        public IFormFile ThumbnailFile { get; set; }

        [DisplayName("Tarih")]
        [Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Date { get; set; }

        [DisplayName("Yazar Adı")]
        [Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        [MaxLength(50, ErrorMessage = "{0} alanı en fazla {1} karakter olmalıdır.")]
        [MinLength(1, ErrorMessage = "{0} alanı en az {1} karakter olmalıdır.")]
        public string SeoAuther { get; set; }

        [DisplayName("Makale Açıklama")]
        [Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        [MaxLength(150, ErrorMessage = "{0} alanı en fazla {1} karakter olmalıdır.")]
        [MinLength(5, ErrorMessage = "{0} alanı en az {1} karakter olmalıdır.")]
        public string SeoDescription { get; set; }

        [DisplayName("Makale Etiketleri")]
        [Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        [MaxLength(70, ErrorMessage = "{0} alanı en fazla {1} karakter olmalıdır.")]
        [MinLength(5, ErrorMessage = "{0} alanı en az {1} karakter olmalıdır.")]
        public string SeoTags { get; set; }

        [DisplayName("Kategori")]
        [Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        public int CategoryId { get; set; }

        [DisplayName("Aktif Mi?")]
        public bool IsActive { get; set; }

        [Required(ErrorMessage = "Kullanıcı bilgisi zorunlu alandır.")]
        public int UserId { get; set; }

        public IList<Category> Categories { get; set; }
    }
}
