using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using MovieCharactersAPI.Repositories;
using MovieCharactersAPI.Model;
using MovieCharactersAPI.Model.DTO.Character;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Net.Mime;
using Microsoft.AspNetCore.Http;

namespace MovieCharactersAPI.Controller
{
    /// <summary>
    /// Class <c>CharacterController</c> inherit ControllerBase for an MVC Controller
    /// </summary>
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

        /// <summary>
        /// Get all characters
        /// </summary>
        /// <returns>All characters</returns>
        /// <response code="200">All characters was successfully retrived</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IEnumerable<CharacterReadDTO>> GetAll()
        {
            var characters = await _repository.GetAll(); 
            return _mapper.Map<List<CharacterReadDTO>>(characters);
        }

        /// <summary>
        /// Get a specific character by id
        /// </summary>
        /// <param name="id">The id for the character</param>
        /// <returns>The requested character</returns>
        /// <response code="200">The character was successfully retrived</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CharacterReadDTO>> GetById(int id)
        {
            if(!_repository.Exist(id))
            {
                return NotFound();
            }

            var character = await _repository.GetById(id);

            return _mapper.Map<CharacterReadDTO>(character);
        }

        /// <summary>
        /// Update an existing character
        /// </summary>
        /// <param name="id">id for the character to update</param>
        /// <param name="characterDto">Data to update</param>
        /// <returns></returns>
        /// <response code="404">The character do not exist</response>
        /// <response code="400">The ids do not match</response>
        /// <response code="204">The character was updated</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> PutCharacter(int id,CharacterEditDTO characterDto)
        {

            if(id != characterDto.CharacterId)
            {
                return BadRequest();
            }
            if (!_repository.Exist(id))
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

        /// <summary>
        /// Add a character
        /// </summary>
        /// <param name="characterDto">The character data</param>
        /// <returns>The added character</returns>
        /// <response code="400">The character was  not added</response>
        /// <response code="201">The character was added</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
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

        /// <summary>
        /// Delete a specific character
        /// </summary>
        /// <param name="id">Id for character to remove</param>
        /// <returns></returns>
        /// <response code="404">The character was not found</response>
        /// <response code="204">The character was deleted</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DeleteCharacter(int id)
        {
            if (!_repository.Exist(id))
            {
                return NotFound();
            }

            await _repository.Delete(id);

            return NoContent();
        }
    }
}
