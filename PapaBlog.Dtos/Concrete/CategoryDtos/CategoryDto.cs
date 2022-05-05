using PapaBlog.Entities.Concrete;
using PapaBlog.Shared.Dtos;

namespace PapaBlog.Dtos.Concrete.CategoryDtos
{
    public class CategoryDto : DtoGetBase
    {
        public Category Category { get; set; }
    }
}
