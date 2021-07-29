using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MovieCharactersAPI.Model;
using MovieCharactersAPI.Model.DTO.Franchise;
using MovieCharactersAPI.Model.DTO.Movie;
using MovieCharactersAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCharactersAPI.Controller
{
    [Route("api/franchises")]
    [ApiController]
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
        [HttpPut("id")]
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
            await _repository.Update(franchise);
            return NoContent();

        }

        [HttpPost]
        public async Task<ActionResult<Franchise>> PostFranchise(FranchiseCreateDTO franchiseDto)
        {

            Franchise franchise = _mapper.Map<Franchise>(franchiseDto);

            franchise = await _repository.Create(franchise);

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
        [Route("/addMovie")]
        public async Task<ActionResult> AddMovie(MovieCreateDTO movieDTO, int franchiseId)
        {
            if (!_repository.Exsist(franchiseId))
            {
                return NotFound();
            }
            Movie movie = _mapper.Map<Movie>(movieDTO);
            await _repository.AddMovie(movie, franchiseId);

            return Ok($"{movie.MovieTitle} was added to the franchise with the id {franchiseId}");
        }

        [HttpDelete]
        [Route("/removeMovie")]
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
        [Route("/GetMovies")]
        public async Task<ActionResult<IEnumerable<MovieReadDTO>>> GetMovies(int id)
        {
            if (!_repository.Exsist(id))
            {
                return NotFound();
            }
            IEnumerable<Movie> movies = await _repository.GetMovies(id);

            return _mapper.Map <List<MovieReadDTO>>(movies);

        }


    }
}
