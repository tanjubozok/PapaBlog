﻿using PapaBlog.Dtos.Concrete.UserDtos;

namespace PapaBlog.MvcWebUI.Areas.Admin.Models
{
    public class UserAddAjaxViewModel
    {
        public UserAddDto UserAddDto { get; set; }
        public string UserAddPartial { get; set; }
        public UserDto UserDto { get; set; }
    }
}
