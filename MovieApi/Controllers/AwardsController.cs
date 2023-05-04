using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MovieApi.Dtos.Award;
using MovieApi.Services;

namespace MovieApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AwardsController : ControllerBase
    {
        private readonly IAwardService _awardService;
        private readonly IMovieService _movieService;
        private readonly ILogger<AwardsController> _logger;

        public AwardsController(IAwardService awardService, ILogger<AwardsController> logger, IMovieService movieService)
        {
            _awardService = awardService;
            _logger = logger;
            _movieService = movieService;
        }

        /// <summary>
        /// Gets all awards
        /// </summary>
        /// <returns>Returns all awards</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/Awards
        /// 
        /// </remarks>
        /// <response code = "200">Successfully returned awards</response>
        /// <response code = "204">Awards have no content</response>
        /// <response code = "500">Internal Server Error</response>
        [HttpGet(Name = "GetAllAwards")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(AwardDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllAwards()
        {
            try
            {
                var awards = await _awardService.GetAllAwards();

                if (awards.IsNullOrEmpty())
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
        /// Gets a single award by a given <paramref name="id"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns a single award</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/Awards/1
        /// 
        /// </remarks>
        /// <response code = "200">Successfully returned award</response>
        /// <response code = "404">Award with <paramref name="id"/> does not exist</response>
        /// <response code = "500">Internal Server Error</response>
        [HttpGet("{id}", Name = "GetAwardById")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(AwardDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAwardById(int id)
        {
            try
            {
                var award = await _awardService.GetAwardById(id);

                if (award == null)
                {
                    return NotFound($"Award with id {id} does not exist");
                }

                return Ok(award);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Creates an award
        /// </summary>
        /// <param name="awardToCreate">Award details</param>
        /// <returns>Returns the newly created award</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/Awards
        ///     {
        ///         "name" : "Best Film",
        ///         "year" : 2022,
        ///         "movieId" : 1
        ///     }
        /// 
        /// </remarks>
        /// <response code = "201">Successfully created an award</response>
        /// <response code = "400">Actor details are invalid</response>
        /// <response code = "404">MovieId does not exist</response>
        /// <response code = "500">Internal Server Error</response>
        [HttpPost(Name = "CreateAward")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(AwardDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateAward([FromBody] AwardCreationDto awardToCreate)
        {
            try
            {
                //check if movieId exists
                var movie = await _movieService.GetMovieOnly(awardToCreate.MovieId);

                if (movie == null)
                {
                    return NotFound($"Movie with id {awardToCreate.MovieId} does not exist");
                }

                var newAward = await _awardService.CreateAward(awardToCreate);

                return CreatedAtRoute("GetAwardById", new { id = newAward.Id }, newAward);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Updates an existing award
        /// </summary>
        /// <param name="id"></param>
        /// <param name="awardToUpdate"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT /api/Awards/1
        ///     {
        ///         "name" : "Best Cinematography",
        ///         "year" : 2021
        ///     }
        /// 
        /// </remarks>
        /// <response code = "200">Successfully updated award</response>
        /// <response code = "400">Award details are invalid</response>
        /// <response code = "404">Award with <paramref name="id"/> does not exist</response>
        /// <response code = "500">Internal Server Error</response>
        [HttpPut("{id}", Name = "UpdateAward")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAward(int id, [FromBody] AwardUpdateDto awardToUpdate)
        {
            try
            {
                //check if award exists
                var checkAward = await _awardService.GetAwardById(id);
                if (checkAward == null)
                {
                    return NotFound($"Award with id {id} does not exist");
                }

                var isAwardUpdated = await _awardService.UpdateAward(id, awardToUpdate);
                return Ok("Award Updated");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Deletes an award
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     DELETE /api/Awards/1
        /// 
        /// </remarks>
        /// <response code = "200">Successfully deleted an award</response>
        /// <response code = "404">Award with <paramref name="id"/> does not exist</response>
        /// <response code = "500">Internal Server Error</response>
        [HttpDelete("{id}", Name = "DeleteAward")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAward(int id)
        {
            try
            {
                var checkAward = await _awardService.GetAwardById(id);
                if (checkAward == null)
                {
                    return NotFound($"Award with id {id} does not exist");
                }

                var isAwardDeleted = await _awardService.DeleteAward(id);
                return Ok("Award Deleted");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }
    }

}
