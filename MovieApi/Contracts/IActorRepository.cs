using MovieApi.Models;

namespace MovieApi.Contracts
{
    public interface IActorRepository
    {
        /// <summary>
        /// Gets all Actors
        /// </summary>
        /// <returns>Returns all Actors</returns>
        Task<IEnumerable<Actor>> GetAll();

        /// <summary>
        /// Gets all Actors from the given <paramref name="movieId"/>
        /// </summary>
        /// <param name="movieId"></param>
        /// <returns>Returns all Actors from a movie</returns>
        Task<IEnumerable<Actor>> GetAllByMovieId(int movieId);

        /// <summary>
        /// Gets a single Actor from a given <paramref name="id"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns an Actor</returns>
        Task<Actor?> GetActor(int id);

        /// <summary>
        /// Creates a new record from a given <paramref name="actor"/>
        /// </summary>
        /// <param name="actor"></param>
        /// <returns>Returns the id of the new actor</returns>
        Task<int> Create(Actor actor);

        /// <summary>
        /// Updates an existing <paramref name="actor"/> record
        /// </summary>
        /// <param name="actor"></param>
        /// <returns>Returns true if update is successful, false otherwise</returns>
        Task<bool> Update(Actor actor);

        /// <summary>
        /// Deletes an actor from a given <paramref name="id"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns true if delete is successful, false otherwise</returns>
        Task<bool> Delete(int id);
    }
}
