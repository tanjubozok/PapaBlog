using PapaBlog.Entities.Concrete;
using PapaBlog.Shared.Dtos;
using System.Collections.Generic;

namespace PapaBlog.Dtos.Concrete.UserDtos
{
    public class UserListDto : DtoGetBase
    {
        public IList<User> Users { get; set; }
    }
}
