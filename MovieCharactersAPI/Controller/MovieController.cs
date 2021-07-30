using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieCharactersAPI.Model;
using MovieCharactersAPI.Model.DTO.Character;
using MovieCharactersAPI.Model.DTO.Movie;
using MovieCharactersAPI.Repositories;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;

namespace MovieCharactersAPI.Controller
{
    /// <summary>
    /// Class <c>MovieController</c> inherit ControllerBase for an MVC Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class MovieController : ControllerBase
    {
        private readonly IMovieRepository _repository;
        private readonly IMapper _mapper;

        public MovieController(IMovieRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all movies
        /// </summary>
        /// <returns>All movies</returns>
        /// <response code="200">All movies was retrived</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IEnumerable<MovieReadDTO>> GetAll()
        {
            var movies = await _repository.GetAll();
            return _mapper.Map<List<MovieReadDTO>>(movies);
        }

        /// <summary>
        /// Get a specific movie by it's Id
        /// </summary>
        /// <param name="id">The id of the specific movie</param>
        /// <returns>A Movie</returns>
        /// <response code="200">Movie was found</response>
        /// <response code="404">Movie not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MovieReadDTO>> GetById(int id)
        {
            if (!_repository.Exist(id))
            {
                return NotFound($"A movie with the id: {id} was not found");
            }
            var movie = await _repository.GetById(id);

            return Ok(_mapper.Map<MovieReadDTO>(movie));
        }

        /// <summary>
        /// Update an existing movie
        /// </summary>
        /// <param name="id">The id for the movie to update</param>
        /// <param name="movieDTO">Data to update</param>
        /// <returns></returns>
        /// <response code="204">Movie was updated</response>
        /// <response code="400">Ids not matching</response>
        /// <response code="404">Movie not found</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> PutMovie(int id, MovieEditDTO movieDTO)
        {

            if (id != movieDTO.MovieId)
            {
                return BadRequest("Ids not matching");
            }
            if (!_repository.Exist(id))
            {
                return NotFound($"A movie with the id: {id} was not found");
            }

            Movie movie = _mapper.Map<Movie>(movieDTO);
            await _repository.Update(movie);

            return NoContent();
        }

        /// <summary>
        /// Add a movie 
        /// </summary>
        /// <param name="movieDTO">Data to add to the movie</param>
        /// <returns>The movie created</returns>
        /// <response code="400">Movie creation faild</response>
        /// <response code="201">Movie was successfully added</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<MovieReadDTO>> PostMovie(MovieCreateDTO movieDTO)
        {
            Movie movie = _mapper.Map<Movie>(movieDTO);
            try
            {
                movie = await _repository.Create(movie);
            }
            catch 
            {
                return BadRequest("Movie was added");
            }
           
            return base.CreatedAtAction("GetById",
                new { id = movie.MovieId },
                _mapper.Map<MovieReadDTO>(movie));
        }

        /// <summary>
        /// Delete a movie
        /// </summary>
        /// <param name="id">The id for the movie to delete</param>
        /// <returns></returns>
        /// <reponse code="204">Movie was deleted</reponse>
        /// <reponse code="404">Movie was not found</reponse>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DeleteMovie(int id)
        {
            if (!_repository.Exist(id))
            {
                return NotFound($"A movie with the id: {id} was not found");
            }

            await _repository.Delete(id);

            return NoContent();
        }

        /// <summary>
        /// Add an existing character to an existing movie
        /// </summary>
        /// <param name="movieId">The movie id</param>
        /// <param name="characterId">The character id</param>
        /// <returns></returns>
        /// <response code="204">The character was added</response>
        /// <response code="404">The movie or character do not exist</response>
        [HttpPut]
        [Route("addCharacter")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> AddCharacterToMovie(int movieId, int characterId)
        {
            if (!_repository.Exist(movieId))
            {
                return NotFound($"A movie with the id: {movieId} was not found");
            }

            try
            {
                 await _repository.AddCharacterToMovie(characterId, movieId);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            catch
            {
                return NotFound($"A character with the id: {characterId} was not found");
            }

            return NoContent();
        }

        /// <summary>
        /// Create and add a character to an existing movie
        /// </summary>
        /// <param name="characterDTO"></param>
        /// <param name="movieId"></param>
        /// <returns>The movie with the added character</returns>
        /// <response code="404">Movie do not exist</response>
        /// <response code="400">Concurrency violation</response>
        /// <response code="200">The character was created and added to the movie</response>
        [HttpPost]
        [Route("createCharater")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<MovieReadDTO>> CreateCharacterAddToMovie(CharacterCreateDTO characterDTO, int movieId)
        {
            if (!_repository.Exist(movieId))
            {
                return NotFound($"A movie with the id: {movieId} was not found");
            }
            Movie movie;
            Character character = _mapper.Map<Character>(characterDTO);
            try
            {
                movie = await _repository.AddCharacterToMovie(character, movieId);
            }
            catch (DbUpdateConcurrencyException) 
            {
                return BadRequest();
            }
            return Ok(_mapper.Map<MovieReadDTO>(movie));
        }

        /// <summary>
        /// Remove a character from a movie
        /// </summary>
        /// <param name="movieId">Movie id</param>
        /// <param name="characterId">Id for the character to remove</param>
        /// <returns></returns>
        /// <response code="400">The character do not belong to the movie</response>
        /// <response code="204">The character was removed</response>
        [HttpPut]
        [Route("removeCharater")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> RemoveCharacterFromMovie(int movieId, int characterId)
        {

            CharacterMovie characterMovie = await _repository.GetLinkingTable(movieId, characterId);
            if(characterMovie == null)
            {
                return BadRequest();
            }

            try
            {
                await _repository.RemoveCharacterFromMovie(characterMovie);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
          
            return NoContent(); 
        }

        /// <summary>
        /// Get all Character in a specific movie
        /// </summary>
        /// <param name="id">Movie id</param>
        /// <returns>A list of characters in the movie</returns>
        /// <response code="404">Movie not found</response>
        /// <response code="200">The characters was successfully retrived</response>
        [HttpGet]
        [Route("getCharacters")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<CharacterReadDTO>>> GetCharacters(int id)
        {
            if (!_repository.Exist(id))
            {
                return NotFound();
            }
            IEnumerable<Character> characters = await _repository.GetCharacters(id);
            return _mapper.Map<List<CharacterReadDTO>>(characters);
        }
    }
}
