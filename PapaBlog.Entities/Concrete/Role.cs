using PapaBlog.Shared.Entities.Abstract;
using System.Collections.Generic;

namespace PapaBlog.Entities.Concrete
{
    public class Role : EntityBase, IEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
