using AutoMapper;
using MovieApi.Dtos.Movie;
using MovieApi.Models;
using System.Globalization;

namespace MovieApi.Mappings
{
    public class MovieMappings : Profile
    {
        public MovieMappings()
        {
            CreateMap<Movie, MovieDto>()
                .ForMember(moviedto => moviedto.ReleaseDate, opt => opt.MapFrom(movie => movie.ReleaseDate.ToString("D")));

            CreateMap<MovieCreationDto, Movie>()
                .ForMember(movie => movie.ReleaseDate, opt => opt.MapFrom(movieCDto => DateTime.ParseExact(movieCDto.ReleaseDate!, "M-d-yyyy", CultureInfo.InvariantCulture)));
        }
    }
}
