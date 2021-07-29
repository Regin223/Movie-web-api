using MovieCharactersAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCharactersAPI.Repositories
{
    public interface IMovieRepository : IRepository<Movie>
    {
        public Task<Movie> AddCharacterToMovie(Character character, int id);
        public Task RemoveCharacterFromMovie(CharacterMovie characterMovie);
        public Task<CharacterMovie> GetLinkingTable(int characterId, int movieId);
    }
}
