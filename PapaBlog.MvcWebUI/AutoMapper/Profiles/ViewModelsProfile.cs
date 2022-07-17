using AutoMapper;
using PapaBlog.Dtos.Concrete.ArticleDtos;
using PapaBlog.MvcWebUI.Areas.Admin.Models;

namespace PapaBlog.MvcWebUI.AutoMapper.Profiles
{
    public class ViewModelsProfile : Profile
    {
        public ViewModelsProfile()
        {
            CreateMap<ArticleAddViewModel, ArticleAddDto>();
            CreateMap<ArticleUpdateDto, ArticleUpdateViewModel>().ReverseMap();
        }
    }
}
