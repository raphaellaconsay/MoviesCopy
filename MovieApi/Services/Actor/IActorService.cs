using MovieApi.Dtos.Actor;

namespace MovieApi.Services
{
    public interface IActorService
    {
        /// <summary>
        /// Gets all Actors
        /// </summary>
        /// <returns>Returns all Actors as ActorDto</returns>
        Task<IEnumerable<ActorDto>> GetAllActors();

        /// <summary>
        /// Gets all actors from a given <paramref name="movieId"/>
        /// </summary>
        /// <param name="movieId"></param>
        /// <returns>Returns all Actors from a movie as ActorDto</returns>
        Task<IEnumerable<ActorDto>> GetAllActors(int movieId);

        /// <summary>
        /// Gets a single Actor from a given <paramref name="id"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns an actor as ActorDto</returns>
        Task<ActorDto?> GetActorById(int id);

        /// <summary>
        /// Creates a new Actor from <paramref name="actorToCreate"/>
        /// </summary>
        /// <param name="actorToCreate"></param>
        /// <returns>Returns the newly created Actor as ActorDto</returns>
        Task<ActorDto> CreateActor(ActorCreationDto actorToCreate);

        /// <summary>
        /// Updates an existing actor record from <paramref name="id"/> given data <paramref name="actorToUpdate"/>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="actorToUpdate"></param>
        /// <returns>Returns true if update is successful, false otherwise</returns>
        Task<bool> UpdateActor(int id, ActorUpdateDto actorToUpdate);

        /// <summary>
        /// Deletes an actor from a given <paramref name="id"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns true if delete is successful, false otherwise</returns>
        Task<bool> DeleteActor(int id);
    }
}
