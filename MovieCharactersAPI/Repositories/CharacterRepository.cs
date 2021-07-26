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
            return await _dbContext.Characters.ToListAsync();
        }
        public async Task<Character> GetById(int id)
        {
            return await _dbContext.Characters.FindAsync(id);
        }
        public Task<Character> Create(Character entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Character entity)
        {
            throw new NotImplementedException();
        }

       

       

        public Task Update(Character entity)
        {
            throw new NotImplementedException();
        }
    }
}
