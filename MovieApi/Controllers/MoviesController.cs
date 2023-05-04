using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MovieApi.Dtos.Actor;
using MovieApi.Dtos.Award;
using MovieApi.Dtos.Genre;
using MovieApi.Dtos.Movie;
using MovieApi.Services;

namespace MovieApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;
        private readonly IGenreService _genreService;
        private readonly IActorService _actorService;
        private readonly IAwardService _awardService;
        private readonly IMovieActorService _movieActorService;
        private readonly IMovieGenreService _movieGenreService;
        private readonly ILogger<MoviesController> _logger;

        public MoviesController(
            IMovieService movieService,
            ILogger<MoviesController> logger,
            IGenreService genreService,
            IActorService actorService,
            IAwardService awardService,
            IMovieActorService movieActorService,
            IMovieGenreService movieGenreService)
        {
            _movieService = movieService;
            _logger = logger;
            _genreService = genreService;
            _actorService = actorService;
            _awardService = awardService;
            _movieActorService = movieActorService;
            _movieGenreService = movieGenreService;
        }

        /// <summary>
        /// Gets all movies
        /// </summary>
        /// <returns>Returns all movies</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/Movies
        /// 
        /// </remarks>
        /// <response code = "200">Successfully returned movies</response>
        /// <response code = "204">Movies have no content</response>
        /// <response code = "500">Internal Server Error</response>
        [HttpGet(Name = "GetAllMovies")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(MovieDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllMovies()
        {
            try
            {
                var movies = await _movieService.GetAllMovies();

                if (movies.IsNullOrEmpty())
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
        /// Gets a single movie by a given <paramref name="id"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns a single movie</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/Movies/1
        /// 
        /// </remarks>
        /// <response code = "200">Successfully returned movie</response>
        /// <response code = "404">Movie with <paramref name="id"/> does not exist</response>
        /// <response code = "500">Internal Server Error</response>
        [HttpGet("{id}", Name = "GetMovieById")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(MovieByIdDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetMovieById(int id)
        {
            try
            {
                var movieCheck = await _movieService.GetMovieOnly(id);

                if (movieCheck == null)
                {
                    return NotFound($"Movie with id {id} does not exist");
                }

                var movie = await _movieService.GetMovieById(id);

                return Ok(movie);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Gets all genres of a movie
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns all genres of a movie</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/Movies/1/genres
        /// 
        /// </remarks>
        /// <response code = "200">Successfully returned genres</response>
        /// <response code = "204">Movie have no genres</response>
        /// <response code = "404">Movie with <paramref name="id"/> does not exist</response>
        /// <response code = "500">Internal Server Error</response>
        [HttpGet("{id}/genres", Name = "GetMovieGenres")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(GenreDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetMovieGenres(int id)
        {
            try
            {
                var movie = await _movieService.GetMovieOnly(id);

                if (movie == null)
                {
                    return NotFound($"Movie with id {id} does not exist");
                }

                var genres = await _genreService.GetAllGenres(id);
                if (!genres.Any())
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
        /// Gets all actors of a movie
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns all actors of a movie</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/Movies/1/actors
        /// 
        /// </remarks>
        /// <response code = "200">Successfully returned actors</response>
        /// <response code = "204">Movie have no actors</response>
        /// <response code = "404">Movie with <paramref name="id"/> does not exist</response>
        /// <response code = "500">Internal Server Error</response>
        [HttpGet("{id}/actors", Name = "GetMovieActors")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ActorDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetMovieActors(int id)
        {
            try
            {
                var movie = await _movieService.GetMovieOnly(id);

                if (movie == null)
                {
                    return NotFound($"Movie with id {id} does not exist");
                }

                var actors = await _actorService.GetAllActors(id);
                if (!actors.Any())
                {
                    return NoContent();
                }

                return Ok(actors);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Gets all awards of a movie
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns all awards of a movie</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/Movies/1/awards
        /// 
        /// </remarks>
        /// <response code = "200">Successfully returned awards</response>
        /// <response code = "204">Movie have no awards</response>
        /// <response code = "404">Movie with <paramref name="id"/> does not exist</response>
        /// <response code = "500">Internal Server Error</response>
        [HttpGet("{id}/awards", Name = "GetMovieAwards")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(AwardDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetMovieAwards(int id)
        {
            try
            {
                var movie = await _movieService.GetMovieOnly(id);

                if (movie == null)
                {
                    return NotFound($"Movie with id {id} does not exist");
                }

                var awards = await _awardService.GetAllAwards(id);
                if (!awards.Any())
                {
                    return NoContent();
                }

                return Ok(awards);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Creates a movie
        /// </summary>
        /// <param name="movieToCreate">Movie details</param>
        /// <returns>Returns the newly created movie</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/Movies
        ///     {
        ///         "title" : "John Wick",
        ///         "director" : "Chad Stahelski",
        ///         "duration" 101,
        ///         "releaseDate" : "10-29-2014",
        ///         "rate" : 91
        /// 
        /// </remarks>
        [HttpPost(Name = "CreateMovie")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(MovieDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateMovie([FromBody] MovieCreationDto movieToCreate)
        {
            try
            {
                var newMovie = await _movieService.CreateMovie(movieToCreate);

                return CreatedAtRoute("GetMovieById", new { id = newMovie.Id }, newMovie);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Adds an actor to movie
        /// </summary>
        /// <param name="id"></param>
        /// <param name="actorId"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT /api/Movies/1/actors/2
        /// 
        /// </remarks>
        /// <response code = "200">Successfully added actor to movie</response>
        /// <response code = "400">Actor is already in movie</response>
        /// <response code = "404">Movie / Actor with the given ids does not exist</response>
        /// <response code = "500">Internal Server Error</response>
        [HttpPut("{id}/actors/{actorId}", Name = "AddActorToMovie")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddActorToMovie(int id, int actorId)
        {
            try
            {
                var movie = await _movieService.GetMovieOnly(id);

                if (movie == null)
                {
                    return NotFound($"Movie with id {id} does not exist");
                }

                var actor = await _actorService.GetActorById(actorId);

                if (actor == null)
                {
                    return NotFound($"Actor with id {actorId} does not exist");
                }

                //check if actor is already in movie
                if (await _movieActorService.IsActorInMovie(id, actorId) == true)
                {
                    return BadRequest("Actor Already In Movie");
                }

                var isComplete = await _movieActorService.AddActorInMovie(id, actorId);
                return Ok("Added Actor to Movie");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Adds an genre to movie
        /// </summary>
        /// <param name="id"></param>
        /// <param name="genreId"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT /api/Movies/1/genres/2
        /// 
        /// </remarks>
        /// <response code = "200">Successfully added genre to movie</response>
        /// <response code = "400">Genre is already in movie</response>
        /// <response code = "404">Movie / Actor with the given ids does not exist</response>
        /// <response code = "500">Internal Server Error</response>
        [HttpPut("{id}/genres/{genreId}", Name = "AddGenreToMovie")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddGenreToMovie(int id, int genreId)
        {
            try
            {
                var movie = await _movieService.GetMovieOnly(id);

                if (movie == null)
                {
                    return NotFound($"Movie with id {id} does not exist");
                }

                var genre = await _genreService.GetGenreById(genreId);

                if (genre == null)
                {
                    return NotFound($"Genre with id {genreId} does not exist");
                }

                //check if genre is already in movie
                if (await _movieGenreService.IsGenreInMovie(id, genreId))
                {
                    return BadRequest("Genre Already In Movie");
                }

                var isComplete = await _movieGenreService.AddGenreInMovie(id, genreId);
                return Ok("Added Genre to Movie");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Updates an existing movie
        /// </summary>
        /// <param name="id"></param>
        /// <param name="movieToUpdate"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT /api/Movies/1
        ///     {
        ///         "releaseDate" : "9-11-2002",
        ///         "rate" : 96
        /// 
        /// </remarks>
        /// <response code = "200">Successfully updated movie</response>
        /// <response code = "400">Movie details are invalid</response>
        /// <response code = "404">Movie with <paramref name="id"/> does not exist</response>
        /// <response code = "500">Internal Server Error</response>
        [HttpPut("{id}", Name = "UpdateMovie")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateMovie(int id, [FromBody] MovieUpdateDto movieToUpdate)
        {
            try
            {
                //check if movie exists
                var checkMovie = await _movieService.GetMovieOnly(id);
                if (checkMovie == null)
                {
                    return NotFound($"Movie with id {id} does not exist");
                }

                var isMovieUpdated = await _movieService.UpdateMovie(id, movieToUpdate);
                return Ok("Movie Updated");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Deletes a movie
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     DELETE /api/Movies/1
        /// 
        /// </remarks>
        /// <response code = "200">Successfully deleted a movie</response>
        /// <response code = "404">Movie with <paramref name="id"/> does not exist</response>
        /// <response code = "500">Internal Server Error</response>
        [HttpDelete("{id}", Name = "DeleteMovie")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            try
            {
                var checkMovie = await _movieService.GetMovieOnly(id);
                if (checkMovie == null)
                {
                    return NotFound($"Movie with id {id} does not exist");
                }

                var isMovieDeleted = await _movieService.DeleteMovie(id);
                return Ok("Movie Deleted");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Removes a genre from movie
        /// </summary>
        /// <param name="id"></param>
        /// <param name="genreId"></param>
        /// <returns></returns>
        /// <remarks>Sample request:
        /// 
        ///     DELETE /api/Movies/1/genres/2
        /// 
        /// </remarks>
        /// <response code = "200">Successfully deleted genre from movie</response>
        /// <response code = "400">Genre is not in movie</response>
        /// <response code = "404">Movie / Genre with the given ids does not exist</response>
        /// <response code = "500">Internal Server Error</response>
        [HttpDelete("{id}/genres/{genreId}", Name = "DeleteGenreFromMovie")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteGenreFromMovie(int id, int genreId)
        {
            try
            {
                var movie = await _movieService.GetMovieOnly(id);

                if (movie == null)
                {
                    return NotFound($"Movie with id {id} does not exist");
                }

                var genre = await _genreService.GetGenreById(genreId);

                if (genre == null)
                {
                    return NotFound($"Genre with id {genreId} does not exist");
                }

                if (!(await _movieGenreService.IsGenreInMovie(id, genreId)))
                {
                    return BadRequest("Genre is NOT in Movie");
                }

                var isGenreFromMovieDeleted = await _movieGenreService.DeleteGenreInMovie(id, genreId);
                return Ok("Genre From Movie Deleted");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Removes an actor from movie
        /// </summary>
        /// <param name="id"></param>
        /// <param name="actorId"></param>
        /// <returns></returns>
        /// <remarks>Sample request:
        /// 
        ///     DELETE /api/Movies/1/actors/2
        /// 
        /// </remarks>
        /// <response code = "200">Successfully deleted actor from movie</response>
        /// <response code = "400">Actor is not in movie</response>
        /// <response code = "404">Movie / Actor with the given ids does not exist</response>
        /// <response code = "500">Internal Server Error</response>
        [HttpDelete("{id}/actors/{actorId}", Name = "DeleteActorFromMovie")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteActorFromMovie(int id, int actorId)
        {
            try
            {
                var movie = await _movieService.GetMovieOnly(id);

                if (movie == null)
                {
                    return NotFound($"Movie with id {id} does not exist");
                }

                var actor = await _actorService.GetActorById(actorId);

                if (actor == null)
                {
                    return NotFound($"Actor with id {actorId} does not exist");
                }

                if (!(await _movieActorService.IsActorInMovie(id, actorId)))
                {
                    return BadRequest("Actor is NOT in Movie");
                }

                var isActorFromMovieDeleted = await _movieActorService.DeleteActorInMovie(id, actorId);
                return Ok("Actor From Movie Deleted");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }
    }
}
