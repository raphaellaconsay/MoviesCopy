using MovieApi.Dtos.Movie;
using MovieApi.Models;

namespace MovieApi.Services
{
    public interface IMovieService
    {
        /// <summary>
        /// Gets all Movies
        /// </summary>
        /// <returns>Returns all Movies as MovieDto</returns>
        Task<IEnumerable<MovieDto>> GetAllMovies();

        /// <summary>
        /// Gets all movies from a given <paramref name="genreId"/>
        /// </summary>
        /// <param name="genreId"></param>
        /// <returns>Returns all Movies from a genre as MovieDto</returns>
        Task<IEnumerable<MovieDto>> GetAllMoviesByGenreId(int genreId);

        /// <summary>
        /// Gets all movies from a given <paramref name="actorId"/>
        /// </summary>
        /// <param name="actorId"></param>
        /// <returns>Returns all Movies from an actor as MovieDto</returns>
        Task<IEnumerable<MovieDto>> GetAllMoviesByActorId(int actorId);

        /// <summary>
        /// Gets a single movie from a given <paramref name="id"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns a Movie with all of its Properties as MovieByIdDto</returns>
        Task<MovieByIdDto?> GetMovieById(int id);

        /// <summary>
        /// Gets a single movie from a given <paramref name="id"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns a Movie without IEnumerable Properties</returns>
        Task<MovieDto?> GetMovieOnly(int id);

        /// <summary>
        /// Creates a new Movie from <paramref name="movieToCreate"/>
        /// </summary>
        /// <param name="movieToCreate"></param>
        /// <returns>Returns the newly created Movie as MovieDto</returns>
        Task<MovieDto> CreateMovie(MovieCreationDto movieToCreate);

        /// <summary>
        /// Updates an existing movie record from <paramref name="id"/> given data <paramref name="movieToUpdate"/>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="movieToUpdate"></param>
        /// <returns>Returns true if update is successful, false otherwise</returns>
        Task<bool> UpdateMovie(int id, MovieUpdateDto movieToUpdate);

        /// <summary>
        /// Deletes a movie from a given <paramref name="id"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns true if delete is successful, false otherwise</returns>
        Task<bool> DeleteMovie(int id);
    }
}
