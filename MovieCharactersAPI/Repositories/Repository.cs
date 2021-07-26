using MovieCharactersAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCharactersAPI.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity>
    {
        private readonly MovieCharacterDbContext _dbContext;
        public Repository(MovieCharacterDbContext context)
        {
            _dbContext = context;
        }

        public Task<TEntity> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> Create(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TEntity>> GetAll()
        {
            throw new NotImplementedException();
        }

        

        public Task Update(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
