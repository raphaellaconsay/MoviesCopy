using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MovieApi.Dtos.Genre;
using MovieApi.Dtos.Movie;
using MovieApi.Services;

namespace MovieApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _genreService;
        private readonly IMovieService _movieService;
        private readonly IMovieGenreService _movieGenreService;
        private readonly ILogger<GenresController> _logger;

        public GenresController(IGenreService genreService, ILogger<GenresController> logger, IMovieService movieService, IMovieGenreService movieGenreService)
        {
            _genreService = genreService;
            _logger = logger;
            _movieService = movieService;
            _movieGenreService = movieGenreService;
        }

        /// <summary>
        /// Gets all genres
        /// </summary>
        /// <returns>Returns all genres</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/Genres
        /// 
        /// </remarks>
        /// <response code = "200">Successfully returned genres</response>
        /// <response code = "204">Genres have no content</response>
        /// <response code = "500">Internal Server Error</response>
        [HttpGet(Name = "GetAllGenres")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(GenreDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllGenres()
        {
            try
            {
                var genres = await _genreService.GetAllGenres();

                if (genres.IsNullOrEmpty())
                {
                    return NoContent();
                }
                return Ok(genres);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Gets a single genre by a given <paramref name="id"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns a single genre</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/Genres/1
        /// 
        /// </remarks>
        /// <response code = "200">Successfully returned genre</response>
        /// <response code = "404">Genre with <paramref name="id"/> does not exist</response>
        /// <response code = "500">Internal Server Error</response>
        [HttpGet("{id}", Name = "GetGenreById")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(GenreDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetGenreById(int id)
        {
            try
            {
                var genre = await _genreService.GetGenreById(id);

                if (genre == null)
                {
                    return NotFound($"Genre with id {id} does not exist");
                }

                return Ok(genre);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Gets all movies of a genre
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns all movies of a genre</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/Genres/1/movies
        /// 
        /// </remarks>
        /// <response code = "200">Successfully returned movies</response>
        /// <response code = "204">Genre have no movies</response>
        /// <response code = "404">Genre with <paramref name="id"/> does not exist</response>
        /// <response code = "500">Internal Server Error</response>
        [HttpGet("{id}/movies", Name = "GetGenreMovies")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(MovieDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetGenreMovies(int id)
        {
            try
            {
                var genre = await _genreService.GetGenreById(id);

                if (genre == null)
                {
                    return NotFound($"Genre with id {id} does not exist");
                }

                var movies = await _movieService.GetAllMoviesByGenreId(id);
                if (!movies.Any())
                {
                    return NoContent();
                }

                return Ok(movies);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Creates a genre
        /// </summary>
        /// <param name="genreToCreate">Genre details</param>
        /// <returns>Returns the newly created genre</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/Genres
        ///     {
        ///         "name" : "Comedy"
        ///     }
        /// 
        /// </remarks>
        /// <response code = "201">Successfully created a genre</response>
        /// <response code = "400">Genre details are invalid / Genre already existing</response>
        /// <response code = "500">Internal Server Error</response>
        [HttpPost(Name = "CreateGenre")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(GenreDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateGenre([FromBody] GenreCreationDto genreToCreate)
        {
            try
            {
                //check if genre already exists
                var genreCheck = await _genreService.GetGenreByName(genreToCreate.Name!);
                if (genreCheck != null) return BadRequest("Genre already exists");

                var newGenre = await _genreService.CreateGenre(genreToCreate);

                return CreatedAtRoute("GetGenreById", new { id = newGenre.Id }, newGenre);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Adds a movie to a genre
        /// </summary>
        /// <param name="movieId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT /api/Genres/1/movies/2
        /// 
        /// </remarks>
        /// <response code = "200">Successfully added movie to genre</response>
        /// <response code = "400">Movie is already in genre</response>
        /// <response code = "404">Genre / Movie with the given ids does not exist</response>
        /// <response code = "500">Internal Server Error</response>
        [HttpPut("{id}/movies/{movieId}", Name = "AddMovieToGenre")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddMovieToGenre(int movieId, int id)
        {
            try
            {
                var genre = await _genreService.GetGenreById(id);

                if (genre == null)
                {
                    return NotFound($"Genre with id {id} does not exist");
                }

                var movie = await _movieService.GetMovieOnly(movieId);

                if (movie == null)
                {
                    return NotFound($"Movie with id {movieId} does not exist");
                }

                //check if genre is already in movie
                if (await _movieGenreService.IsGenreInMovie(movieId, id))
                {
                    return BadRequest("Movie Already In Genre");
                }

                var isComplete = await _movieGenreService.AddGenreInMovie(movieId, id);
                return Ok("Added Genre to Movie");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Updates an existing genre
        /// </summary>
        /// <param name="id"></param>
        /// <param name="genreToUpdate"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT /api/Genres/1
        ///     {
        ///         "name" : "Action"
        ///     }
        /// 
        /// </remarks>
        /// <response code = "200">Successfully updated genre</response>
        /// <response code = "400">Genre details are invalid / Genre already existing</response>
        /// <response code = "404">Genre with <paramref name="id"/> does not exist</response>
        /// <response code = "500">Internal Server Error</response>
        [HttpPut("{id}", Name = "UpdateGenre")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateGenre(int id, [FromBody] GenreUpdateDto genreToUpdate)
        {
            try
            {
                //check if genre exists
                var checkGenre = await _genreService.GetGenreById(id);
                if (checkGenre == null)
                {
                    return NotFound($"Genre with id {id} does not exist");
                }

                //check if genre already exists
                var genreCheck = await _genreService.GetGenreByName(genreToUpdate.Name!);
                if (genreCheck != null) return BadRequest("Genre already exists");

                var isGenreUpdated = await _genreService.UpdateGenre(id, genreToUpdate);
                return Ok("Genre Updated");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Deletes a genre
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     DELETE /api/Genres/1
        /// 
        /// </remarks>
        /// <response code = "200">Successfully deleted a genre</response>
        /// <response code = "404">Genre with <paramref name="id"/> does not exist</response>
        /// <response code = "500">Internal Server Error</response>
        [HttpDelete("{id}", Name = "DeleteGenre")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteGenre(int id)
        {
            try
            {
                var checkGenre = await _genreService.GetGenreById(id);
                if (checkGenre == null)
                {
                    return NotFound($"Genre with id {id} does not exist");
                }

                var isGenreDeleted = await _genreService.DeleteGenre(id);
                return Ok("Genre Deleted");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Removes a movie from a genre
        /// </summary>
        /// <param name="id"></param>
        /// <param name="movieId"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     DELETE /api/Genres/1/movies/2
        /// 
        /// </remarks>
        /// <response code = "200">Successfully deleted movie from genre</response>
        /// <response code = "400">Movie is not in genre</response>
        /// <response code = "404">Genre / Movie with the given ids does not exist</response>
        /// <response code = "500">Internal Server Error</response>
        [HttpDelete("{id}/movies/{movieId}", Name = "DeleteMovieFromGenre")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteMovieFromGenre(int id, int movieId)
        {
            try
            {
                var genre = await _genreService.GetGenreById(id);

                if (genre == null)
                {
                    return NotFound($"Genre with id {id} does not exist");
                }

                var movie = await _movieService.GetMovieOnly(movieId);

                if (movie == null)
                {
                    return NotFound($"Movie with id {movieId} does not exist");
                }

                if (!(await _movieGenreService.IsGenreInMovie(movieId, id)))
                {
                    return BadRequest("Movie is NOT in Genre");
                }

                var isMovieFromGenreDeleted = await _movieGenreService.DeleteGenreInMovie(movieId, id);
                return Ok("Movie From Genre Deleted");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }
    }
}
