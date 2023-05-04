using MovieApi.Models;

namespace MovieApi.Contracts
{
    public interface IGenreRepository
    {
        /// <summary>
        /// Gets all Genres
        /// </summary>
        /// <returns>Returns all Genres</returns>
        Task<IEnumerable<Genre>> GetAll();

        /// <summary>
        /// Gets all Genres from the given <paramref name="movieId"/>
        /// </summary>
        /// <param name="movieId"></param>
        /// <returns>Returns all Genres from a movie</returns>
        Task<IEnumerable<Genre>> GetAllByMovieId(int movieId);

        /// <summary>
        /// Gets a single Genre from a given <paramref name="id"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns a Genre</returns>
        Task<Genre?> GetGenre(int id);

        /// <summary>
        /// Gets a single Genre from a given <paramref name="name"/>
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Returns a Genre</returns>
        Task<Genre?> GetGenre(string name);

        /// <summary>
        /// Creates a new record from a given <paramref name="genre"/>
        /// </summary>
        /// <param name="genre"></param>
        /// <returns>Returns the id of the new Genre</returns>
        Task<int> Create(Genre genre);

        /// <summary>
        /// Updates an existing <paramref name="genre"/> record
        /// </summary>
        /// <param name="genre"></param>
        /// <returns>Returns true if update is successful, false otherwise</returns>
        Task<bool> Update(Genre genre);

        /// <summary>
        /// Deletes a genre from a given <paramref name="id"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns true if delete is successful, false otherwise</returns>
        Task<bool> Delete(int id);
    }
}
