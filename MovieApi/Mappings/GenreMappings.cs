using AutoMapper;
using MovieApi.Dtos.Genre;
using MovieApi.Models;

namespace MovieApi.Mappings
{
    public class GenreMappings : Profile
    {
        public GenreMappings()
        {
            CreateMap<Genre, GenreDto>();
            CreateMap<GenreCreationDto, Genre>();
            CreateMap<GenreUpdateDto, Genre>();
        }
    }
}
