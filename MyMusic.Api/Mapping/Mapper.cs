using AutoMapper;
using MyMusic.Api.Resources;
using MyMusic.Core.Models;

namespace MyMusic.Api.Mapping;

public class Mapper : Profile
{
    public Mapper()
    {
        CreateMap<Music, MusicResource>().ReverseMap();
        CreateMap<Artist, ArtistResource>().ReverseMap();
        
        CreateMap<Music, SaveMusicResource>().ReverseMap();
        CreateMap<Artist, SaveArtistResource>().ReverseMap();
    }
}