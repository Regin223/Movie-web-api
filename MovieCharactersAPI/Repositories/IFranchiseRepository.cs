using MovieCharactersAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCharactersAPI.Repositories
{
    public interface IFranchiseRepository : IRepository<Franchise>
    {

        public Task RemoveMovieFromFranchise(int franchiseId, int movieId);

    }
}
