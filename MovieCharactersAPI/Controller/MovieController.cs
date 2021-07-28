﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MovieCharactersAPI.Model;
using MovieCharactersAPI.Model.DTO.Character;
using MovieCharactersAPI.Model.DTO.Movie;
using MovieCharactersAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCharactersAPI.Controller
{
    [Route("api/movies")]
    [ApiController]
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
        public async Task<IEnumerable<MovieReadDTO>> GetAll()
        {
            var movies = await _repository.GetAll();
            return _mapper.Map<List<MovieReadDTO>>(movies);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MovieReadDTO>> GetById(int id)
        {
            var movie = await _repository.GetById(id);

            if (movie == null)
            {
                return NotFound();
            }

            return _mapper.Map<MovieReadDTO>(movie);
        }

        [HttpPut("id")]
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

            movie = await _repository.Create(movie);

            return CreatedAtAction("GetById",
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
        [Route("/createCharaterToMovie")]
        public async Task<ActionResult<MovieReadDTO>> CreateCharacterAddToMovie(CharacterReadDTO characterDTO, int movieId)
        {
            if (!_repository.Exsist(movieId))
            {
                return NotFound();
            }

            Character character = _mapper.Map<Character>(characterDTO);
            Movie movie = await _repository.AddCharacterToMovie(character, movieId);
        }

    }
}
