using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieCharactersAPI.Repositories;
using MovieCharactersAPI.Model;
using MovieCharactersAPI.Model.DTO.Character;
using AutoMapper;

namespace MovieCharactersAPI.Controller
{
    [Route("api/characters")]
    [ApiController]
    public class CharactersController : ControllerBase
    {
        private readonly IRepository<Character> _repository;
        private readonly IMapper _mapper;
        public CharactersController(IMapper mapper ,IRepository<Character> characterRepository)
        {
            _repository = characterRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<CharacterReadDTO>> GetAll()
        {
            var characters = await _repository.GetAll(); 
            return _mapper.Map<List<CharacterReadDTO>>(characters); ;
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<CharacterReadDTO>> GetById(int id)
        {
            var character = await _repository.GetById(id);
  
            if(character == null)
            {
                return NotFound();
            } 

            return _mapper.Map<CharacterReadDTO>(character);
        }

        [HttpPut("id")]
        public async Task<ActionResult> PutCharacter(int id,CharacterEditDTO characterDto)
        {

            if(id != characterDto.CharacterId)
            {
                return BadRequest();
            }
            if (!_repository.Exsist(id))
            {
                return NotFound();
            }

            Character character = _mapper.Map<Character>(characterDto);
            await _repository.Update(character);

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Character>> PostCharacter(CharacterCreateDTO characterDto)
        {

            Character character = _mapper.Map<Character>(characterDto);

            character = await _repository.Create(character);

            return CreatedAtAction("GetById",
                new { id = character.CharacterId }, 
                _mapper.Map<CharacterReadDTO>(character));
        }

        





        
    }
}
