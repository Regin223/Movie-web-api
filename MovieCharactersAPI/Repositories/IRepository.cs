using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCharactersAPI.Repositories
{
    /// <summary>
    /// Generic interface <c>IRepository</c> with CRUD functions.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity>
    {
        public Task<TEntity> GetById(int id);

        public Task<IEnumerable<TEntity>> GetAll();

        public Task<TEntity> Create(TEntity entity);

        public Task Update(TEntity entity);

        public Task Delete(TEntity entity);
    }
}
