using AutoMapper;
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

        [HttpGet]
        public async Task<IEnumerable<FranchiseReadDTO>> GetAll()
        {
            var franchises = await _repository.GetAll();
            return _mapper.Map<List<FranchiseReadDTO>>(franchises);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FranchiseReadDTO>> GetById(int id)
        {
            if(!_repository.Exsist(id))
            {
                return NotFound();
            }
            var franchise = await _repository.GetById(id);
            return _mapper.Map<FranchiseReadDTO>(franchise);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> PutFranchise(int id, FranchiseEditDTO franchiseDto)
        {
            if(id != franchiseDto.FranchiseId)
            {
                return BadRequest();
            }
            if (!_repository.Exsist(id))
            {
                return NotFound();
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

        [HttpPost]
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

        [HttpDelete]
        public async Task<ActionResult> DeleteFranchice(int id)
        {
            if (!_repository.Exsist(id))
            {
                return NotFound();
            }
            await _repository.Delete(id);
            return NoContent();
        }

        [HttpPut]
        [Route("addMovie")]
        public async Task<ActionResult> AddMovieToFranchise(int movieId, int franchiseId)
        {
            if (!_repository.Exsist(franchiseId))
            {
                return NotFound("Franchise do not exsist");
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

        [HttpPost]
        [Route("createMovie")]
        public async Task<ActionResult> CreateMovieAddToFranchise(MovieCreateDTO movieDTO, int franchiseId)
        {
            if (!_repository.Exsist(franchiseId))
            {
                return NotFound();
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

        [HttpDelete]
        [Route("removeMovie")]
        public async Task<ActionResult> RemoveMovie(int franchiseId, int movieId)
        {
            
            if (!_repository.Exsist(franchiseId))
            {
                return NotFound();
            }
            await _repository.RemoveMovie(franchiseId, movieId);

            return NoContent();
        }

        [HttpGet]
        [Route("getMovies")]
        public async Task<ActionResult<IEnumerable<MovieReadDTO>>> GetMovies(int id)
        {
            if (!_repository.Exsist(id))
            {
                return NotFound();
            }
            IEnumerable<Movie> movies = await _repository.GetMovies(id);

            return _mapper.Map <List<MovieReadDTO>>(movies);
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
