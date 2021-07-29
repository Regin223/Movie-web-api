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
            if (!_repository.Exsist(id))
            {
                return NotFound();
            }
            var movie = await _repository.GetById(id);

            return Ok(_mapper.Map<MovieReadDTO>(movie));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutMovie(int id, MovieEditDTO movieDTO)
        {

            if (id != movieDTO.MovieId)
            {
                return BadRequest();
            }
            if (!_repository.Exsist(id))
            {
                return NotFound();
            }

            Movie movie = _mapper.Map<Movie>(movieDTO);
            await _repository.Update(movie);

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<MovieReadDTO>> PostMovie(MovieCreateDTO movieDTO)
        {

            Movie movie = _mapper.Map<Movie>(movieDTO);

            try
            {
                movie = await _repository.Create(movie);
            }
            catch 
            {
                return BadRequest();
            }
            

            return base.CreatedAtAction("GetById",
                new { id = movie.MovieId },
                _mapper.Map<MovieReadDTO>(movie));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMovie(int id)
        {
            if (!_repository.Exsist(id))
            {
                return NotFound();
            }

            await _repository.Delete(id);

            return NoContent();
        }

        [HttpPost]
        [Route("createCharater")]
        public async Task<ActionResult<MovieReadDTO>> CreateCharacterAddToMovie(CharacterCreateDTO characterDTO, int movieId)
        {
            if (!_repository.Exsist(movieId))
            {
                return NotFound();
            }
            Movie movie = null;
            Character character = _mapper.Map<Character>(characterDTO);
            try
            {
                movie = await _repository.AddCharacterToMovie(character, movieId);
            }
            catch (DbUpdateConcurrencyException) 
            {
                return BadRequest();
            }
            

            return _mapper.Map<MovieReadDTO>(movie);
        }

        [HttpPut]
        [Route("removeCharater")]
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

        [HttpGet]
        [Route("getCharacters")]
        public async Task<ActionResult<IEnumerable<CharacterReadDTO>>> GetCharacters(int id)
        {
            if (!_repository.Exsist(id))
            {
                return NotFound();
            }
            IEnumerable<Character> characters = await _repository.GetCharacters(id);
            return _mapper.Map<List<CharacterReadDTO>>(characters);
        }
    }
}
