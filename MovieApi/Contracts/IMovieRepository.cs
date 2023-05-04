using MovieApi.Models;

namespace MovieApi.Contracts
{
    public interface IMovieRepository
    {
        /// <summary>
        /// Gets all Movies
        /// </summary>
        /// <returns>Returns all Movies</returns>
        Task<IEnumerable<Movie>> GetAll();

        /// <summary>
        /// Gets all Movies from the given <paramref name="genreId"/>
        /// </summary>
        /// <param name="genreId"></param>
        /// <returns>Returns all Movies from a genre</returns>
        Task<IEnumerable<Movie>> GetAllByGenreId(int genreId);

        /// <summary>
        /// Gets all Movies from the given <paramref name="actorId"/>
        /// </summary>
        /// <param name="actorId"></param>
        /// <returns>Returns all Movies from an actor</returns>
        Task<IEnumerable<Movie>> GetAllByActorId(int actorId);

        /// <summary>
        /// Gets a single Movie from a given <paramref name="id"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns a Movie with all of its Properties</returns>
        Task<Movie?> GetMovie(int id);

        /// <summary>
        /// Gets a single Movie from a given <paramref name="id"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns a Movie without IEnumerable Properties</returns>
        Task<Movie?> GetMovieOnly(int id);

        /// <summary>
        /// Creates a new record from a given <paramref name="movie"/>
        /// </summary>
        /// <param name="movie"></param>
        /// <returns>Returns the if of the new Movie</returns>
        Task<int> Create(Movie movie);

        /// <summary>
        /// Checks whether <paramref name="actorId"/> is in <paramref name="movieId"/>
        /// </summary>
        /// <param name="movieId"></param>
        /// <param name="actorId"></param>
        /// <returns>Returns true if actor exists within a movie, false otherwise</returns>
        Task<bool> IsActorInMovie(int movieId, int actorId);

        /// <summary>
        /// Inserts <paramref name="actorId"/> to <paramref name="movieId"/>
        /// </summary>
        /// <param name="movieId"></param>
        /// <param name="actorId"></param>
        /// <returns>Returns true if insert successful, false otherwise</returns>
        Task<bool> AddActorInMovie(int movieId, int actorId);

        /// <summary>
        /// Checks whether <paramref name="genreId"/> is in <paramref name="movieId"/>
        /// </summary>
        /// <param name="movieId"></param>
        /// <param name="genreId"></param>
        /// <returns>Returns true if genre exists within a movie, false otherwise</returns>
        Task<bool> IsGenreInMovie(int movieId, int genreId);

        /// <summary>
        /// Inserts <paramref name="genreId"/> to <paramref name="movieId"/>
        /// </summary>
        /// <param name="movieId"></param>
        /// <param name="genreId"></param>
        /// <returns>Returns true if insert successful, false otherwise</returns>
        Task<bool> AddGenreInMovie(int movieId, int genreId);

        /// <summary>
        /// Updates an existing <paramref name="movie"/> record
        /// </summary>
        /// <param name="movie"></param>
        /// <returns>Returns true if update is successful, false otherwise</returns>
        Task<bool> Update(Movie movie);

        /// <summary>
        /// Deletes a movie from a given <paramref name="id"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns true if delete is successful, false otherwise</returns>
        Task<bool> Delete(int id);

        /// <summary>
        /// Deletes <paramref name="genreId"/> from <paramref name="movieId"/>
        /// </summary>
        /// <param name="movieId"></param>
        /// <param name="genreId"></param>
        /// <returns>Returns true if delete is successful, false otherwise</returns>
        Task<bool> DeleteGenreInMovie(int movieId, int genreId);

        /// <summary>
        /// Deletes <paramref name="actorId"/> from <paramref name="movieId"/>
        /// </summary>
        /// <param name="movieId"></param>
        /// <param name="actorId"></param>
        /// <returns>Returns true if delete is successful, false otherwise</returns>
        Task<bool> DeleteActorInMovie(int movieId, int actorId);
    }
}
