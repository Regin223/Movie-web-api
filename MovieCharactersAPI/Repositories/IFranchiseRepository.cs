using MovieCharactersAPI.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieCharactersAPI.Repositories
{
    /// <summary>
    /// Interface <c>IFranchiseRepository</c> with functions additional to crud.
    /// Inherits from IRepository.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IFranchiseRepository : IRepository<Franchise>
    {
        public Task RemoveMovie(int franchiseId, int movieId);
        public Task AddMovie(Movie movie, int franchiseId);
        public Task AddMovie(int movieId, int franciseId);
        public Task<IEnumerable<Movie>> GetMovies(int franchiseId);
        public Task<IEnumerable<Character>> GetCharacters(int franchiseId);
    }
}
