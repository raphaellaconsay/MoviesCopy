using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MovieApi.Dtos.Actor;
using MovieApi.Dtos.Movie;
using MovieApi.Services;

namespace MovieApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorsController : ControllerBase
    {
        private readonly IActorService _actorService;
        private readonly IMovieService _movieService;
        private readonly IMovieActorService _movieActorService;
        private readonly ILogger<ActorsController> _logger;

        public ActorsController(IActorService actorservice, ILogger<ActorsController> logger, IMovieService movieService, IMovieActorService movieActorService)
        {
            _actorService = actorservice;
            _logger = logger;
            _movieService = movieService;
            _movieActorService = movieActorService;
        }

        /// <summary>
        /// Gets all actors
        /// </summary>
        /// <returns>Returns all actors</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/Actors
        /// 
        /// </remarks>
        /// <response code = "200">Successfully returned actors</response>
        /// <response code = "204">Actors have no content</response>
        /// <response code = "500">Internal Server Error</response>
        [HttpGet(Name = "GetAllActors")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ActorDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllActors()
        {
            try
            {
                var actors = await _actorService.GetAllActors();
                if (actors.IsNullOrEmpty())
                {
                    return NoContent();
                }
                return Ok(actors);
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Gets a single actor by a given <paramref name="id"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns a single actor</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/Actors/1
        /// 
        /// </remarks>
        /// <response code = "200">Successfully returned actor</response>
        /// <response code = "404">Actor with <paramref name="id"/> does not exist</response>
        /// <response code = "500">Internal Server Error</response>
        [HttpGet("{id}", Name = "GetActorById")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ActorDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetActorById(int id)
        {
            try
            {
                var actor = await _actorService.GetActorById(id);

                if (actor == null)
                {
                    return NotFound($"Actor with id {id} does not exist");
                }

                return Ok(actor);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Gets all movies of an actor
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns all movies of an actor</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/Actors/1/movies
        /// 
        /// </remarks>
        /// <response code = "200">Successfully returned movies</response>
        /// <response code = "204">Actor have no movies</response>
        /// <response code = "404">Actor with <paramref name="id"/> does not exist</response>
        /// <response code = "500">Internal Server Error</response>
        [HttpGet("{id}/movies", Name = "GetActorMovies")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(MovieDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetActorMovies(int id)
        {
            try
            {
                var actor = await _actorService.GetActorById(id);

                if (actor == null)
                {
                    return NotFound($"Actor with id {id} does not exist");
                }

                var movies = await _movieService.GetAllMoviesByActorId(id);
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
        /// Creates an actor
        /// </summary>
        /// <param name="actorToCreate">Actor details</param>
        /// <returns>Returns the newly created actor</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/Actors
        ///     {
        ///         "name" : "Will Smith",
        ///         "gender" : "Male",
        ///         "birthday" : "9-25-1968"
        ///     }
        /// 
        /// </remarks>
        /// <response code = "201">Successfully created an actor</response>
        /// <response code = "400">Actor details are invalid</response>
        /// <response code = "500">Internal Server Error</response>
        [HttpPost(Name = "CreateActor")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ActorDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateActor([FromBody] ActorCreationDto actorToCreate)
        {
            try
            {              
                var newActor = await _actorService.CreateActor(actorToCreate);

                return CreatedAtRoute("GetActorById", new { id = newActor.Id }, newActor);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Adds a movie to an actor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="movieId"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT /api/Actors/1/movies/2
        /// 
        /// </remarks>
        /// <response code = "200">Successfully added movie to actor</response>
        /// <response code = "400">Movie is already in actor</response>
        /// <response code = "404">Actor / Movie with the given ids does not exist</response>
        /// <response code = "500">Internal Server Error</response>
        [HttpPut("{id}/movies/{movieId}", Name = "AddMovieToActor")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddMovieToActor(int id, int movieId)
        {
            try
            {
                var actor = await _actorService.GetActorById(id);

                if (actor == null)
                {
                    return NotFound($"Actor with id {id} does not exist");
                }

                var movie = await _movieService.GetMovieOnly(movieId);

                if (movie == null)
                {
                    return NotFound($"Movie with id {movieId} does not exist");
                }                

                //check if movie is already in actor
                if (await _movieActorService.IsActorInMovie(movieId, id) == true)
                {
                    return BadRequest("Movie Already In Actor");
                }

                var isComplete = await _movieActorService.AddActorInMovie(movieId, id);
                return Ok("Added Movie in Actor");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Updates an existing actor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="actorToUpdate"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request
        /// 
        ///     PUT /api/Actors/1
        ///     {
        ///         "name" : "William Smithereen"
        ///     }
        /// 
        /// </remarks>
        /// <response code = "200">Successfully updated actor</response>
        /// <response code = "400">Actor details are invalid</response>
        /// <response code = "404">Actor with <paramref name="id"/> does not exist</response>
        /// <response code = "500">Internal Server Error</response>
        [HttpPut("{id}", Name = "UpdateActor")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateActor(int id, [FromBody] ActorUpdateDto actorToUpdate)
        {
            try
            {
                //check if actor exists
                var checkActor = await _actorService.GetActorById(id);
                if (checkActor == null)
                {
                    return NotFound($"Actor with id {id} does not exist");
                }

                var isActorUpdated = await _actorService.UpdateActor(id, actorToUpdate);
                return Ok("Actor Updated");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Deletes an actor
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     DELETE /api/Actors/1
        /// 
        /// </remarks>
        /// <response code = "200">Successfully deleted an actor</response>
        /// <response code = "404">Actor with <paramref name="id"/> does not exist</response>
        /// <response code = "500">Internal Server Error</response>
        [HttpDelete("{id}", Name = "DeleteActor")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteActor(int id)
        {
            try
            {
                var checkActor = await _actorService.GetActorById(id);
                if (checkActor == null)
                {
                    return NotFound($"Actor with id {id} does not exist");
                }

                var isActorDeleted = await _actorService.DeleteActor(id);
                return Ok("Actor Deleted");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Removes a movie from an actor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="movieId"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     DELETE /api/Actors/1/movies/2
        /// 
        /// </remarks>
        /// <response code = "200">Successfully deleted movie from actor</response>
        /// <response code = "400">Movie is not in actor</response>
        /// <response code = "404">Actor / Movie with the given ids does not exist</response>
        /// <response code = "500">Internal Server Error</response>
        [HttpDelete("{id}/movies/{movieId}", Name = "DeleteMovieFromActor")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteMovieFromActor(int id, int movieId)
        {
            try
            {
                var actor = await _actorService.GetActorById(id);

                if (actor == null)
                {
                    return NotFound($"Actor with id {id} does not exist");
                }

                var movie = await _movieService.GetMovieOnly(movieId);

                if (movie == null)
                {
                    return NotFound($"Movie with id {movieId} does not exist");
                }

                if (!(await _movieActorService.IsActorInMovie(movieId, id)))
                {
                    return BadRequest("Movie is NOT in Actor");
                }

                var isMovieFromActorDeleted = await _movieActorService.DeleteActorInMovie(movieId, id);
                return Ok("Movie From Actor Deleted");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }
    }
}
