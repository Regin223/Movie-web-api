using AutoMapper;
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

        private readonly MovieCharacterDbContext _dbcontext;
        private readonly IMapper _mapper;


        public FranchiseRepository(MovieCharacterDbContext context, IMapper mapper)
        {
            _dbcontext = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<Franchise>> GetAll()
        {
            return await _dbcontext.Franchises.Include(m => m.Movies).ToListAsync();
        }

        public async Task<Franchise> GetById(int id)
        {
            Franchise franchise = await _dbcontext.Franchises.Include(m => m.Movies).Where(f => f.FranchiseId == id).FirstAsync();
            return franchise;
        }

        public Task<Franchise> Create(Franchise entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Franchise entity)
        {
            throw new NotImplementedException();
        }

        public bool Exsist(int id)
        {
            throw new NotImplementedException();
        }

        public Task Update(Franchise entity)
        {
            throw new NotImplementedException();
        }
    }
}
