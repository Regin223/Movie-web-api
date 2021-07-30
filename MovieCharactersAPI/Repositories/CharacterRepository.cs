using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieCharactersAPI.Model; 

namespace MovieCharactersAPI.Repositories
{
    /// <summary>
    /// Class <c>CharacterRepository</c> is the repository for characters. 
    /// The repository is responsible for the communication with the database.
    /// Inherits from IRepository
    /// </summary>
    public class CharacterRepository : IRepository<Character>
    {
        private readonly MovieCharacterDbContext _dbContext;
        public CharacterRepository(MovieCharacterDbContext context)
        {
            _dbContext = context;
        }
        /// <summary>
        /// Returns all characters in the database.
        /// </summary>
        /// <returns>A List of characters</returns>
        public async Task<IEnumerable<Character>> GetAll()
        {
            var characters = await _dbContext.Characters.Include(m => m.CharacterMovies).ToListAsync();
            return characters;
        }
        /// <summary>
        /// Returns a character object where character id equals the input id.
        /// </summary>
        /// <param name="id">character id</param>
        /// <returns>A character object</returns>
        public async Task<Character> GetById(int id)
        {
            var character = await _dbContext.Characters.Include(m => m.CharacterMovies).Where(c => c.CharacterId == id).FirstAsync();
            return character;
        }
        /// <summary>
        /// Checks if a character exists.
        /// True if the character exists.
        /// False if the character does not exists.
        /// </summary>
        /// <param name="id">character id</param>
        /// <returns>A bool</returns>
        public bool Exsist(int id)
        {
            return _dbContext.Characters.Any(c => c.CharacterId == id);
        }
        /// <summary>
        /// Updating an exsisting character.
        /// </summary>
        /// <param name="entity">Takes in a character object</param>
        /// <returns>Nothing</returns>
        public async Task Update(Character entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
        /// <summary>
        /// Adding a new character.
        /// </summary>
        /// <param name="entity">Takes in a character object</param>
        /// <returns>A character object</returns>
        public async Task<Character> Create(Character entity)
        {
            _dbContext.Characters.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
        /// <summary>
        /// Deletes a character that equals the input id. 
        /// </summary>
        /// <param name="id">takes in a character id</param>
        /// <returns>Nothing</returns>
        public async Task Delete(int id)
        {
            var character = await _dbContext.Characters.FindAsync(id);
            _dbContext.Characters.Remove(character);
            await _dbContext.SaveChangesAsync();
        }
       

      
    }
}
