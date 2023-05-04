using AutoMapper;
using MovieApi.Contracts;
using MovieApi.Dtos.Genre;
using MovieApi.Models;

namespace MovieApi.Services
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IMapper _mapper;

        public GenreService(IGenreRepository repository, IMapper mapper)
        {
            _genreRepository = repository;
            _mapper = mapper;
        }

        public async Task<GenreDto> CreateGenre(GenreCreationDto genreToCreate)
        {
            var genreModel = _mapper.Map<Genre>(genreToCreate);

            genreModel.Id = await _genreRepository.Create(genreModel);

            return _mapper.Map<GenreDto>(genreModel);
        }

        public async Task<bool> DeleteGenre(int id)
        {
            return await _genreRepository.Delete(id);
        }

        public async Task<IEnumerable<GenreDto>> GetAllGenres()
        {
            var genreModels = await _genreRepository.GetAll();

            return _mapper.Map<IEnumerable<GenreDto>>(genreModels);
        }

        public async Task<IEnumerable<GenreDto>> GetAllGenres(int movieId)
        {
            var genreModels = await _genreRepository.GetAllByMovieId(movieId);

            return _mapper.Map<IEnumerable<GenreDto>>(genreModels);
        }

        public async Task<GenreDto?> GetGenreById(int id)
        {
            var genreModel = await _genreRepository.GetGenre(id);
            if (genreModel == null) return null;

            return _mapper.Map<GenreDto>(genreModel);
        }

        public async Task<GenreDto?> GetGenreByName(string name)
        {
            var genreModel = await _genreRepository.GetGenre(name);
            if (genreModel == null) return null;

            return _mapper.Map<GenreDto>(genreModel);
        }

        public async Task<bool> UpdateGenre(int id, GenreUpdateDto genreToUpdate)
        {
            var genreModel = _mapper.Map<Genre>(genreToUpdate);
            genreModel.Id = id;

            return await _genreRepository.Update(genreModel);

        }
    }
}
