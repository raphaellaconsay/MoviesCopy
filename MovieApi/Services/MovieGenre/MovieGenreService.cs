using MovieApi.Contracts;

namespace MovieApi.Services
{
    public class MovieGenreService : IMovieGenreService
    {
        private readonly IMovieRepository _movieRepository;

        public MovieGenreService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<bool> AddGenreInMovie(int movieId, int genreId)
        {
            return await _movieRepository.AddGenreInMovie(movieId, genreId);
        }

        public async Task<bool> DeleteGenreInMovie(int movieId, int genreId)
        {
            return await _movieRepository.DeleteGenreInMovie(movieId, genreId);
        }

        public async Task<bool> IsGenreInMovie(int movieId, int genreId)
        {
            return await _movieRepository.IsGenreInMovie(movieId, genreId);
        }
    }
}
