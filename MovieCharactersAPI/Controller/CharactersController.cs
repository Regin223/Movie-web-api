using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieCharactersAPI.Repositories;
using MovieCharactersAPI.Model;

namespace MovieCharactersAPI.Controller
{
    [Route("api/characters")]
    [ApiController]
    public class CharactersController : ControllerBase
    {
        private readonly IRepository<Character> _repository;
        public CharactersController(IRepository<Character> characterRepository)
        {
            _repository = characterRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Character>> GetAll()
        {
            return await _repository.GetAll();
        }
        
        
    }
}
