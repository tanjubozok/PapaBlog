using PapaBlog.Entities.Concrete;
using System.Collections.Generic;

namespace PapaBlog.MvcWebUI.Areas.Admin.Models
{
    public class UserWithRolesViewModel
    {
        public User User { get; set; }
        public IList<string> Roles { get; set; }
    }
}
