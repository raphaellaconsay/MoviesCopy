using MovieApi.Contracts;

namespace MovieApi.Services
{
    public class MovieActorService : IMovieActorService
    {
        private readonly IMovieRepository _movieRepository;

        public MovieActorService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<bool> AddActorInMovie(int movieId, int actorId)
        {
            return await _movieRepository.AddActorInMovie(movieId, actorId);
        }

        public async Task<bool> DeleteActorInMovie(int movieId, int actorId)
        {
            return await _movieRepository.DeleteActorInMovie(movieId, actorId);
        }

        public async Task<bool> IsActorInMovie(int movieId, int actorId)
        {
            return await _movieRepository.IsActorInMovie(movieId, actorId);
        }
    }
}
