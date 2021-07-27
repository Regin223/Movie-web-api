using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCharactersAPI.Repositories
{
    /// <summary>
    /// Generic interface <c>IRepository</c> with CRUD functions.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T>
    {
        public Task<T> GetById(int id);

        public Task<IEnumerable<T>> GetAll();

        public bool Exsist(int id);

        public Task<T> Create(T entity);

        public Task Update(T entity);

        public Task Delete(T entity);
    }
}
