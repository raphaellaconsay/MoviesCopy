namespace MovieApi.Services
{
    public interface IMovieActorService
    {
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
        /// Deletes <paramref name="actorId"/> from <paramref name="movieId"/>
        /// </summary>
        /// <param name="movieId"></param>
        /// <param name="actorId"></param>
        /// <returns>Returns true if delete is successful, false otherwise</returns>
        Task<bool> DeleteActorInMovie(int movieId, int actorId);
    }
}
