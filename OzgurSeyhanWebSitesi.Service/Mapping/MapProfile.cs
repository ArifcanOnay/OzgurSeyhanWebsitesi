using AutoMapper;
using OzgurSeyhanWebSitesi.Core.Dtos;
using OzgurSeyhanWebSitesi.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzgurSeyhanWebSitesi.Service.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile() // Changed from 'protected' to 'public'
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Ogretmen, OgretmenDto>().ReverseMap();
            CreateMap<Spotify, SpotifyDto>().ReverseMap();
            CreateMap<Video, VideoDto>().ReverseMap();
            CreateMap<Kurs, KursDto>().ReverseMap(); 
            // Fixed method call syntax
        }
    }
}
