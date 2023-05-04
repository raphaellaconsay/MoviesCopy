using MovieApi.Models;
using MovieApi.Contracts;
using MovieApi.Dtos.Award;
using AutoMapper;

namespace MovieApi.Services
{
    public class AwardService : IAwardService
    {
        private readonly IAwardRepository _awardRepository;
        private readonly IMapper _mapper;

        public AwardService(IAwardRepository awardrepository, IMapper mapper)
        {
            _awardRepository = awardrepository;
            _mapper = mapper;
        }

        public async Task<AwardDto> CreateAward(AwardCreationDto awardToCreate)
        {
            var awardModel = _mapper.Map<Award>(awardToCreate);

            awardModel.Id = await _awardRepository.Create(awardModel);

            return _mapper.Map<AwardDto>(awardModel);
        }

        public async Task<bool> DeleteAward(int id)
        {
            return await _awardRepository.Delete(id);
        }

        public async Task<IEnumerable<AwardDto>> GetAllAwards()
        {
            var awardModels = await _awardRepository.GetAll();

            return _mapper.Map<IEnumerable<AwardDto>>(awardModels);
        }

        public async Task<IEnumerable<AwardDto>> GetAllAwards(int movieId)
        {
            var awardModels = await _awardRepository.GetAllByMovieId(movieId);

            return _mapper.Map<IEnumerable<AwardDto>>(awardModels);
        }

        public async Task<AwardDto?> GetAwardById(int id)
        {
            var awardModel = await _awardRepository.GetAward(id);
            if (awardModel == null) return null;

            return _mapper.Map<AwardDto>(awardModel);
        }

        public async Task<bool> UpdateAward(int id, AwardUpdateDto awardToUpdate)
        {
            var awardModel = _mapper.Map<Award>(awardToUpdate);
            awardModel.Id = id;

            return await _awardRepository.Update(awardModel);
        }
    }
}
