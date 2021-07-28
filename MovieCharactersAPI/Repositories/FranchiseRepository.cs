using Microsoft.EntityFrameworkCore;
using MovieCharactersAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCharactersAPI.Repositories
{
    public class FranchiseRepository : IFranchiseRepository
    {

        private readonly MovieCharacterDbContext _dbContext;
        


        public FranchiseRepository(MovieCharacterDbContext context)
        {
            _dbContext = context;
        }
        public async Task<IEnumerable<Franchise>> GetAll()
        {
            return await _dbContext.Franchises.Include(m => m.Movies).ToListAsync();
        }

        public async Task<Franchise> GetById(int id)
        {
            Franchise franchise = await _dbContext.Franchises.Include(m => m.Movies).Where(f => f.FranchiseId == id).FirstAsync();
            return franchise;
        }
        public async Task Update(Franchise entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Franchise> Create(Franchise entity)
        {
            _dbContext.Franchises.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public Task Delete(Franchise entity)
        {
            throw new NotImplementedException();
        }

        public bool Exsist(int id)
        {
            return _dbContext.Franchises.Any(f => f.FranchiseId == id);
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
