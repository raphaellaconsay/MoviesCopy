using MovieApi.Dtos.Genre;
using MovieApi.Models;

namespace MovieApi.Services
{
    public interface IGenreService
    {
        /// <summary>
        /// Gets all Genres
        /// </summary>
        /// <returns>Returns all Genres as GenreDto</returns>
        Task<IEnumerable<GenreDto>> GetAllGenres();

        /// <summary>
        /// Gets all genres from a given <paramref name="movieId"/>
        /// </summary>
        /// <param name="movieId"></param>
        /// <returns>Returns all Genres from a movie as GenreDto</returns>
        Task<IEnumerable<GenreDto>> GetAllGenres(int movieId);

        /// <summary>
        /// Gets a single Genre from a given <paramref name="id"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns a genre as GenreDto</returns>
        Task<GenreDto?> GetGenreById(int id);

        /// <summary>
        /// Gets a single Genre from a given <paramref name="name"/>
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Returns a genre as GenreDto</returns>
        Task<GenreDto?> GetGenreByName(string name);

        /// <summary>
        /// Creates a new Genre from <paramref name="genreToCreate"/>
        /// </summary>
        /// <param name="genreToCreate"></param>
        /// <returns>Returns the newly created Genre as GenreDto</returns>
        Task<GenreDto> CreateGenre(GenreCreationDto genreToCreate);

        /// <summary>
        /// Updates an existing genre record from <paramref name="id"/> given data <paramref name="genreToUpdate"/>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="genreToUpdate"></param>
        /// <returns>Returns true if update is successful, false otherwise</returns>
        Task<bool> UpdateGenre(int id, GenreUpdateDto genreToUpdate);

        /// <summary>
        /// Deletes a genre from a given <paramref name="id"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns true if delete is successful, false otherwise</returns>
        Task<bool> DeleteGenre(int id);
    }
}
