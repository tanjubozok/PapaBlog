using AutoMapper;
using PapaBlog.Dtos.Concrete.UserDtos;
using PapaBlog.Entities.Concrete;

namespace PapaBlog.MvcWebUI.AutoMapper.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserAddDto, User>();
            CreateMap<User, UserUpdateDto>().ReverseMap();
        }
    }
}