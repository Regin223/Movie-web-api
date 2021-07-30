using MovieCharactersAPI.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieCharactersAPI.Repositories
{
    /// <summary>
    /// Interface <c>IMovieRepository</c> with functions additional to crud.
    /// Inherits from IRepository.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IMovieRepository : IRepository<Movie>
    {
        public Task<Movie> AddCharacterToMovie(Character character, int id);
        public Task RemoveCharacterFromMovie(CharacterMovie characterMovie);
        public Task<CharacterMovie> GetLinkingTable(int characterId, int movieId);
        public Task<IEnumerable<Character>> GetCharacters(int movieId);
        public Task AddCharacterToMovie(int characterId, int movieId);
    }
}
