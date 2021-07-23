using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCharactersAPI.Repositories
{
    public interface IRepository<T>
    {

        public Task<T> GetById(int id);

        public Task<IEnumerable<T>> GetAll();

        public Task<T> Create(T entity);

        public Task Update(T entity);

        public Task Delete(T entity);

    }
}
