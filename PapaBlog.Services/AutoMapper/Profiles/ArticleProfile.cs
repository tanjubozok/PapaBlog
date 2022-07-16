using AutoMapper;
using PapaBlog.Dtos.Concrete.ArticleDtos;
using PapaBlog.Entities.Concrete;
using System;

namespace PapaBlog.Services.AutoMapper.Profiles
{
    public class ArticleProfile : Profile
    {
        public ArticleProfile()
        {
            CreateMap<ArticleAddDto, Article>().ForMember(x => x.CreatedDate, opt => opt.MapFrom(x => DateTime.Now));
            CreateMap<ArticleUpdateDto, Article>().ForMember(x => x.ModifiedDate, opt => opt.MapFrom(x => DateTime.Now));

            CreateMap<Article, ArticleUpdateDto>();
        }
    }
}
