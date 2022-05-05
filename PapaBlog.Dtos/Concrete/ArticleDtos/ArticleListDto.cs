using PapaBlog.Entities.Concrete;
using PapaBlog.Shared.Dtos;
using System.Collections.Generic;

namespace PapaBlog.Dtos.Concrete.ArticleDtos
{
    public class ArticleListDto : DtoGetBase
    {
        public IList<Article> Articles { get; set; }
    }
}
