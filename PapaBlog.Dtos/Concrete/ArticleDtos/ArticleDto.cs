using PapaBlog.Entities.Concrete;
using PapaBlog.Shared.Dtos;

namespace PapaBlog.Dtos.Concrete.ArticleDtos
{
    public class ArticleDto : DtoGetBase
    {
        public Article Article { get; set; }
    }
}
