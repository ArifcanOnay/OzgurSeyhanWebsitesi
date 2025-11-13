using AutoMapper;
using OzgurSeyhanWebSitesi.Core.Dtos.OgretmenDtos;
using OzgurSeyhanWebSitesi.Core.Dtos.OzelDersDtos;
using OzgurSeyhanWebSitesi.Core.Dtos.PodcastDtos;
using OzgurSeyhanWebSitesi.Core.Dtos.YoutubeVideoDtos;
using OzgurSeyhanWebSitesi.Core.Models;

namespace OzgurSeyhanWebSitesi.Bussinies.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            // ==================== OGRETMEN MAPPINGS ====================
            CreateMap<Ogretmen, OgretmenDto>()
                .ReverseMap()
                .ForMember(dest => dest.Podcasts, opt => opt.Ignore())
                .ForMember(dest => dest.YoutubeVideolari, opt => opt.Ignore())
                .ForMember(dest => dest.OzelDersler, opt => opt.Ignore());
            
            CreateMap<CreateOgretmenDto, Ogretmen>();
            CreateMap<UpdateOgretmenDto, Ogretmen>();

            // ==================== OZEL DERS MAPPINGS ====================
            CreateMap<OzelDers, OzelDersDto>()
                .ReverseMap()
                .ForMember(dest => dest.Ogretmen, opt => opt.Ignore());
            
            CreateMap<CreateOzelDersDto, OzelDers>();
            CreateMap<UpdateOzelDersDto, OzelDers>();

            // ==================== PODCAST MAPPINGS ====================
            CreateMap<Podcast, PodcastDto>()
                .ReverseMap()
                .ForMember(dest => dest.Ogretmen, opt => opt.Ignore());
            
            CreateMap<CreatePodcastDto, Podcast>();
            CreateMap<UpdatePodcastDto, Podcast>();

            // ==================== YOUTUBE VIDEO MAPPINGS ====================
            CreateMap<YoutubeVideo, YoutubeVideoDto>()
                .ReverseMap()
                .ForMember(dest => dest.Ogretmen, opt => opt.Ignore());
            
            CreateMap<CreateYoutubeVideoDto, YoutubeVideo>();
            CreateMap<UpdateYoutubeVideoDto, YoutubeVideo>();
        }
    }
}
