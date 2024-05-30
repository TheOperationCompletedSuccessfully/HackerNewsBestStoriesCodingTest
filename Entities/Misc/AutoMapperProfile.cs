using AutoMapper;
using HackerNewsBestStories.Entities.Dtos;

namespace HackerNewsBestStories.Entities.Misc
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<HackerNewsFirebaseData, HackerNewsBestStory>()
              .ForMember(dest => dest.PostedBy, opt => opt.MapFrom(src => src.By))
              .ForMember(dest => dest.Uri, opt => opt.MapFrom(src => src.Url))
              .ForMember(dest => dest.CommentCount, opt => opt.MapFrom(src => src.Descendants))
              .ForMember(dest => dest.Time, opt => opt.MapFrom(src => DateTimeOffset.FromUnixTimeSeconds(src.Time).ToString("o")));
        }

    }
}
