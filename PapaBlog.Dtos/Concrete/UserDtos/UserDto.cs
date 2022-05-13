using PapaBlog.Entities.Concrete;
using PapaBlog.Shared.Dtos;

namespace PapaBlog.Dtos.Concrete.UserDtos
{
    public class UserDto : DtoGetBase
    {
        public User Users { get; set; }
    }
}
