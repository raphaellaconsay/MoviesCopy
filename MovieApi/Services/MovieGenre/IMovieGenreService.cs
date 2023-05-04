namespace MovieApi.Services
{
    public interface IMovieGenreService
    {
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
        /// Deletes <paramref name="genreId"/> from <paramref name="movieId"/>
        /// </summary>
        /// <param name="movieId"></param>
        /// <param name="genreId"></param>
        /// <returns>Returns true if delete is successful, false otherwise</returns>
        Task<bool> DeleteGenreInMovie(int movieId, int genreId);
    }
}
