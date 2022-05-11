﻿using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace PapaBlog.Entities.Concrete
{
    public class User : IdentityUser<int>
    {
        public string Picture { get; set; }
        public ICollection<Article> Articles { get; set; }
    }
}
