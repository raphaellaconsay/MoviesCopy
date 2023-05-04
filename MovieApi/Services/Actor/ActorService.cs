using AutoMapper;
using MovieApi.Contracts;
using MovieApi.Dtos.Actor;
using MovieApi.Models;

namespace MovieApi.Services
{
    public class ActorService : IActorService
    {
        private readonly IActorRepository _actorRepository;
        private readonly IMapper _mapper;

        public ActorService(IActorRepository actorRepository, IMapper mapper)
        {
            _actorRepository = actorRepository;
            _mapper = mapper;
        }

        public async Task<ActorDto?> GetActorById(int id)
        {
            var actorModel = await _actorRepository.GetActor(id);
            if (actorModel == null) return null;

            return _mapper.Map<ActorDto>(actorModel);
        }

        public async Task<IEnumerable<ActorDto>> GetAllActors()
        {
            var actorModels = await _actorRepository.GetAll();

            return _mapper.Map<IEnumerable<ActorDto>>(actorModels);
        }

        public async Task<ActorDto> CreateActor(ActorCreationDto actorToCreate)
        {
            var actorModel = _mapper.Map<Actor>(actorToCreate);

            actorModel.Id = await _actorRepository.Create(actorModel);

            return _mapper.Map<ActorDto>(actorModel);
        }

        public async Task<IEnumerable<ActorDto>> GetAllActors(int movieId)
        {
            var actorModels = await _actorRepository.GetAllByMovieId(movieId);

            return _mapper.Map<IEnumerable<ActorDto>>(actorModels);
        }

        public async Task<bool> UpdateActor(int id, ActorUpdateDto actorToUpdate)
        {
            var actorModel = _mapper.Map<Actor>(actorToUpdate);
            actorModel.Id = id;

            return await _actorRepository.Update(actorModel);
        }

        public async Task<bool> DeleteActor(int id)
        {
            return await _actorRepository.Delete(id);
        }
    }
}
