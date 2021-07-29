using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using MovieCharactersAPI.Repositories;
using MovieCharactersAPI.Model;
using MovieCharactersAPI.Model.DTO.Character;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Net.Mime;

namespace MovieCharactersAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
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
            if(!_repository.Exsist(id))
            {
                return NotFound();
            }

            var character = await _repository.GetById(id);

            return _mapper.Map<CharacterReadDTO>(character);
        }

        [HttpPut("{id}")]
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
            try
            {
                await _repository.Update(character);
            }
            catch (DbUpdateConcurrencyException)
            {

                throw;
            }
          
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Character>> PostCharacter(CharacterCreateDTO characterDto)
        {

            Character character = _mapper.Map<Character>(characterDto);

            try
            {
                character = await _repository.Create(character);
            }
            catch
            {
                return BadRequest();
            }
            
            return CreatedAtAction("GetById",
                new { id = character.CharacterId }, 
                _mapper.Map<CharacterReadDTO>(character));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCharacter(int id)
        {
            if (!_repository.Exsist(id))
            {
                return NotFound();
            }

            await _repository.Delete(id);

            return NoContent();
        }
    }
}
