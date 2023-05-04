using AutoMapper;
using MovieApi.Contracts;
using MovieApi.Dtos.Movie;
using MovieApi.Models;
using System.Globalization;

namespace MovieApi.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;

        public MovieService(IMovieRepository repository, IMapper mapper)
        {
            _movieRepository = repository;
            _mapper = mapper;
        }

        public async Task<MovieDto> CreateMovie(MovieCreationDto movieToCreate)
        {
            var movieModel = _mapper.Map<Movie>(movieToCreate);

            movieModel.Id = await _movieRepository.Create(movieModel);

            return _mapper.Map<MovieDto>(movieModel);
        }

        public async Task<bool> DeleteMovie(int id)
        {
            return await _movieRepository.Delete(id);
        }

        public async Task<IEnumerable<MovieDto>> GetAllMovies()
        {
            var movieModels = await _movieRepository.GetAll();

            return _mapper.Map<IEnumerable<MovieDto>>(movieModels);
        }

        public async Task<IEnumerable<MovieDto>> GetAllMoviesByActorId(int actorId)
        {
            var movieModels = await _movieRepository.GetAllByActorId(actorId);

            return _mapper.Map<IEnumerable<MovieDto>>(movieModels);
        }

        public async Task<IEnumerable<MovieDto>> GetAllMoviesByGenreId(int genreId)
        {
            var movieModels = await _movieRepository.GetAllByGenreId(genreId);

            return _mapper.Map<IEnumerable<MovieDto>>(movieModels);
        }


        public async Task<MovieByIdDto?> GetMovieById(int id)
        {
            var movieModel = await _movieRepository.GetMovie(id);
            if (movieModel == null) return null;

            var ret = new MovieByIdDto
            {
                Id = movieModel.Id,
                Title = movieModel.Title,
                Director = movieModel.Director,
                Duration = movieModel.Duration,
                ReleaseDate = movieModel.ReleaseDate.ToString("D"),
                Rate = movieModel.Rate
            };

            var nonNullMovies = movieModel.Genres.Where(x => x != null).ToList();
            var nonNullActors = movieModel.Actors.Where(x => x != null).ToList();
            var nonNullAwards = movieModel.Awards.Where(x => x != null).ToList();

            if (nonNullMovies.Any())
            {
                var movieNames = nonNullMovies.Select(x => x.Name).ToList();
                ret.Genres = movieNames.Distinct().ToList();
            }

            if (nonNullActors.Any())
            {
                var actorNames = nonNullActors.Select(x => x.Name).ToList();
                ret.Actors = actorNames.Distinct().ToList();
            }

            if (nonNullAwards.Any())
            {
                var awardNames = nonNullAwards.Select(x => x.Name).ToList();
                ret.Awards = awardNames.Distinct().ToList();
            }

            return ret;
        }

        public async Task<MovieDto?> GetMovieOnly(int id)
        {
            var movieModel = await _movieRepository.GetMovieOnly(id);
            if (movieModel == null) return null;

            return _mapper.Map<MovieDto>(movieModel);
        }

        public async Task<bool> UpdateMovie(int id, MovieUpdateDto movieToUpdate)
        {
            var movieModel = new Movie
            {
                Id = id,
                ReleaseDate = DateTime.ParseExact(movieToUpdate.ReleaseDate!, "M-d-yyyy", CultureInfo.InvariantCulture),
                Rate = movieToUpdate.Rate
            };

            return await _movieRepository.Update(movieModel);
        }
    }
}
