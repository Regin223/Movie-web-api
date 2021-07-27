using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieCharactersAPI.Model; 

namespace MovieCharactersAPI.Repositories
{
    public class CharacterRepository : IRepository<Character>
    {
        private readonly MovieCharacterDbContext _dbContext;
        public CharacterRepository(MovieCharacterDbContext context)
        {
            _dbContext = context;
        }
        public async Task<IEnumerable<Character>> GetAll()
        {
            var characters = await _dbContext.Characters.Include(m => m.Movies).ToListAsync();
            return characters;
        }
        public async Task<Character> GetById(int id)
        {
            var character = await _dbContext.Characters.Include(m => m.Movies).Where(c => c.CharacterId == id).FirstAsync();
            return character;
        }

        public bool Exsist(int id)
        {
            return _dbContext.Characters.Any(c => c.CharacterId == id);
        }
        public async Task Update(Character entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Character> Create(Character entity)
        {
            _dbContext.Characters.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public Task Delete(Character entity)
        {
            throw new NotImplementedException();
        }
       

      
    }
}
