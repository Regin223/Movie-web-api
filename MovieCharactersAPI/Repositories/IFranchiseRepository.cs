using MovieCharactersAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCharactersAPI.Repositories
{
    public interface IFranchiseRepository : IRepository<Franchise>
    {

        public Task RemoveMovie(int franchiseId, int movieId);
        public Task AddMovie(Movie movie, int franchiseId);
        public Task<IEnumerable<Movie>> GetMovies(int franchiseId);
        public Task<IEnumerable<Character>> GetCharacters(int franchiseId);

    }
}
