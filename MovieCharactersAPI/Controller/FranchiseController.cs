using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieCharactersAPI.Model;
using MovieCharactersAPI.Model.DTO.Character;
using MovieCharactersAPI.Model.DTO.Franchise;
using MovieCharactersAPI.Model.DTO.Movie;
using MovieCharactersAPI.Repositories;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;

namespace MovieCharactersAPI.Controller
{
    /// <summary>
    /// Class <c>FranchiseController</c> inherit ControllerBase for an MVC Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class FranchiseController : ControllerBase
    {
        private readonly IFranchiseRepository _repository;
        private readonly IMapper _mapper;

        public FranchiseController(IMapper mapper, IFranchiseRepository franchiseRepository)
        {
            _repository = franchiseRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all franchises
        /// </summary>
        /// <returns>All franchises</returns>
        /// <response code="200">All franchises was successfully retrived</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IEnumerable<FranchiseReadDTO>> GetAll()
        {
            var franchises = await _repository.GetAll();
            return _mapper.Map<List<FranchiseReadDTO>>(franchises);
        }

        /// <summary>
        /// Get a specific franchise by id
        /// </summary>
        /// <param name="id">The id for the francise</param>
        /// <returns>The requested franchise</returns>
        /// <response code="200">The franchise was successfully retrived</response>
        /// <response code="404">The franchise was not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<FranchiseReadDTO>> GetById(int id)
        {
            if(!_repository.Exist(id))
            {
                return NotFound($"A franchise with the id: {id} was not found");
            }
            var franchise = await _repository.GetById(id);
            return _mapper.Map<FranchiseReadDTO>(franchise);
        }

        /// <summary>
        /// Update exisiting franchise
        /// </summary>
        /// <param name="id">id for the franchise to update</param>
        /// <param name="franchiseDto">Data to update</param>
        /// <returns></returns>
        /// /// <response code="404">The franchise do not exist</response>
        /// <response code="400">The ids do not match</response>
        /// <response code="204">The franchise was updated</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> PutFranchise(int id, FranchiseEditDTO franchiseDto)
        {
            if(id != franchiseDto.FranchiseId)
            {
                return BadRequest("The ids must match");
            }
            if (!_repository.Exist(id))
            {
                return NotFound($"A franchise with the id: {id} was not found");
            }

            Franchise franchise = _mapper.Map<Franchise>(franchiseDto);
            try
            {
                await _repository.Update(franchise);
            }
            catch (DbUpdateConcurrencyException)
            {

                throw;
            }
           
            return NoContent();
        }

        /// <summary>
        /// Add a franchise
        /// </summary>
        /// <param name="franchiseDto">Franchise data</param>
        /// <returns>The franchise created</returns>
        /// <response code="400">The franchise was  not added</response>
        /// <response code="201">The franchise was added</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Franchise>> PostFranchise(FranchiseCreateDTO franchiseDto)
        {

            Franchise franchise = _mapper.Map<Franchise>(franchiseDto);

            try
            {
                franchise = await _repository.Create(franchise);
            }
            catch 
            {
                return BadRequest();
            }
            
            return CreatedAtAction("GetById",
                new { id = franchise.FranchiseId },
                _mapper.Map<FranchiseReadDTO>(franchise));
        }

        /// <summary>
        /// Delete a specific franchise
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="404">The franchise was not found</response>
        /// <response code="204">The franchise was deleted</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DeleteFranchice(int id)
        {
            if (!_repository.Exist(id))
            {
                return NotFound($"A franchise with the id: {id} was not found");
            }
            await _repository.Delete(id);
            return NoContent();
        }

        /// <summary>
        /// Add an existing movie to a franchise
        /// </summary>
        /// <param name="movieId">The id for the movie to add</param>
        /// <param name="franchiseId">The franchise id to add movie to</param>
        /// <returns></returns>
        /// <response code="204">The franchise was added</response>
        /// <response code="404">The franchise or movie do not exist</response>
        [HttpPut]
        [Route("addMovie")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> AddMovieToFranchise(int movieId, int franchiseId)
        {
            if (!_repository.Exist(franchiseId))
            {
                return NotFound($"A franchise with the id: {franchiseId} was not found");
            }

            try
            {
                await _repository.AddMovie(movieId, franchiseId);
            }
            catch (DbUpdateConcurrencyException)
            {

                throw;
            }
            catch
            {
                throw;
            }
            return NoContent();
        }

        /// <summary>
        /// Create a movie and add it to an existing franchise
        /// </summary>
        /// <param name="movieDTO"></param>
        /// <param name="franchiseId"></param>
        /// <returns></returns>
        /// <response code="404">Franchise do not exist</response>
        /// <response code="400">Concurrency violation</response>
        /// <response code="200">The movie was created and added to the franchise</response>
        [HttpPost]
        [Route("createMovie")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> CreateMovieAddToFranchise(MovieCreateDTO movieDTO, int franchiseId)
        {
            if (!_repository.Exist(franchiseId))
            {
                return NotFound($"A franchise with the id: {franchiseId} was not found");
            }
            Movie movie = _mapper.Map<Movie>(movieDTO);

            try
            {
                await _repository.AddMovie(movie, franchiseId);
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }
            
            return Ok($"{movie.MovieTitle} was added to the franchise with the id {franchiseId}");
        }

        /// <summary>
        /// Remove a movie from a specific franchise
        /// </summary>
        /// <param name="franchiseId">Franchise id</param>
        /// <param name="movieId">Id for movie to remove</param>
        /// <returns></returns>
        /// <response code="404">The the franchise was not found</response>
        /// <response code="204">The movie was removed</response>
        [HttpPut]
        [Route("removeMovie")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> RemoveMovie(int franchiseId, int movieId)
        {
            
            if (!_repository.Exist(franchiseId))
            {
                return NotFound($"A franchise with the id: {franchiseId} was not found");
            }
            await _repository.RemoveMovie(franchiseId, movieId);

            return NoContent();
        }

        /// <summary>
        /// Get all movies from a specific franchise
        /// </summary>
        /// <param name="id">Id for the specific franchise</param>
        /// <returns>All movies beloning to the franchise</returns>
        /// <response code="404">The francise was not found</response>
        /// <response code="200">All movies was successfully retrived</response>
        [HttpGet]
        [Route("getMovies")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<MovieReadDTO>>> GetMovies(int id)
        {
            if (!_repository.Exist(id))
            {
                return NotFound($"A franchise with the id: {id} was not found");
            }
            IEnumerable<Movie> movies = await _repository.GetMovies(id);

            return _mapper.Map <List<MovieReadDTO>>(movies);
        }

        /// <summary>
        /// Gett all characters from a specific franchise
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="404">The francise was not found</response>
        /// <response code="200">All characters was successfully retrived</response>
        [HttpGet]
        [Route("getCharacters")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<CharacterReadDTO>>> GetCharacters(int id)
        {
            if (!_repository.Exist(id))
            {
                return NotFound($"A franchise with the id: {id} was not found");
            }
            IEnumerable<Character> characters = await _repository.GetCharacters(id);
            return _mapper.Map<List<CharacterReadDTO>>(characters);
        }
    }
}
