using PapaBlog.Entities.Concrete;
using PapaBlog.Shared.Dtos;
using System.Collections.Generic;

namespace PapaBlog.Dtos.Concrete.CategoryDtos
{
    public class CategoryListDto : DtoGetBase
    {
        public IList<Category> Categories { get; set; }
    }
}
