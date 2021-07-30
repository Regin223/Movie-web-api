using Microsoft.EntityFrameworkCore;
using MovieCharactersAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCharactersAPI.Repositories
{
    /// <summary>
    /// Class <c>MovieRepository</c> is the repository for movies. 
    /// The repository is responsible for the communication with the database.
    /// Inherits from IMovieRepository
    /// </summary>
    public class MovieRepository : IMovieRepository
    {
        private readonly MovieCharacterDbContext _dbContext;

        public MovieRepository(MovieCharacterDbContext conext)
        {
            _dbContext = conext;
        }

        /// <summary>
        /// Returns all movies in the database.
        /// </summary>
        /// <returns>A List of movies</returns>
        public async Task<IEnumerable<Movie>> GetAll()
        {
            return await _dbContext.Movies.Include(m => m.CharacterMovies).ToListAsync();
        }

        /// <summary>
        /// Returns a movie object where movie id equals the input id.
        /// </summary>
        /// <param name="id">movie id</param>
        /// <returns>A movie object</returns>
        public async Task<Movie> GetById(int id)
        {
            return await _dbContext.Movies.Include(c => c.CharacterMovies).Where(m => m.MovieId == id).FirstAsync();
        }

        /// <summary>
        /// Checks if a movie exists.
        /// True if the movie exists.
        /// False if the movie does not exists.
        /// </summary>
        /// <param name="id">movie id</param>
        /// <returns>A bool</returns>
        public bool Exsist(int id)
        {
            return _dbContext.Movies.Any(m => m.MovieId == id);
        }

        /// <summary>
        /// Updating an existing movie.
        /// </summary>
        /// <param name="entity">Takes in a movie object</param>
        /// <returns>Nothing</returns>
        public async Task Update(Movie entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Adding a new movie.
        /// </summary>
        /// <param name="entity">Takes in a movie object</param>
        /// <returns>A movie object</returns>
        public async Task<Movie> Create(Movie entity)
        {
            _dbContext.Movies.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        /// <summary>
        /// Deletes a movie that equals the input id. 
        /// </summary>
        /// <param name="id">Takes in a movie id</param>
        /// <returns>Nothing</returns>
        public async Task Delete(int id)
        {
            var movie = await _dbContext.Movies.FindAsync(id);
            _dbContext.Movies.Remove(movie);
            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Adding a character to a movie by adding a link in the linking table. 
        /// A character with character id gets added to a movie with the input movie id. 
        /// </summary>
        /// <param name="characterId">Character id</param>
        /// <param name="movieId">Movie id</param>
        /// <returns>Nothing</returns>
        public async Task AddCharacterToMovie(int characterId, int movieId)
        {
            if(!_dbContext.Characters.Any(c => c.CharacterId == characterId))
            {
                throw new Exception();
            }
            CharacterMovie characterMovie = new CharacterMovie()
            {
                CharacterId = characterId,
                MovieId = movieId
            };
            _dbContext.CharacterMovies.Add(characterMovie);
            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Adding a new character to the database and add a link to a given movie by updating the linking table.
        /// </summary>
        /// <param name="character">Character object</param>
        /// <param name="id">Movie id</param>
        /// <returns></returns>
        public async Task<Movie> AddCharacterToMovie(Character character, int id)
        {
            _dbContext.Characters.Add(character);
            await _dbContext.SaveChangesAsync();
            Character addedCharacter = _dbContext.Characters.OrderBy(i => i.CharacterId).Last();
            CharacterMovie characterMovie = new CharacterMovie() { CharacterId = addedCharacter.CharacterId, MovieId = id };
            _dbContext.CharacterMovies.Add(characterMovie);
            await _dbContext.SaveChangesAsync();
            return await GetById(id);
        }

        /// <summary>
        /// Getting the linking table from the databse to get the many to many relationship between charcters and movies.
        /// </summary>
        /// <param name="movieId">Movie id</param>
        /// <param name="characterId">Character id</param>
        /// <returns></returns>
        public async Task<CharacterMovie> GetLinkingTable(int movieId, int characterId)
        {
            CharacterMovie characterMovie = await _dbContext.CharacterMovies.FindAsync(movieId,characterId);
            return characterMovie;
        }

        /// <summary>
        /// Removes a character from a movie by removing the link in the linking table. 
        /// </summary>
        /// <param name="characterMovie">CharacterMovie object</param>
        /// <returns></returns>
        public async Task RemoveCharacterFromMovie(CharacterMovie characterMovie)
        {
            _dbContext.CharacterMovies.Remove(characterMovie);
            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Gets all characters in a movie. 
        /// </summary>
        /// <param name="movieId">Movie id</param>
        /// <returns>A List of characters.</returns>
        public async Task<IEnumerable<Character>> GetCharacters(int movieId)
        {
            List<int> characterIds = await _dbContext.CharacterMovies.Where(cm => cm.MovieId == movieId).Select(c => c.CharacterId).ToListAsync();
            List<Character> characters = await _dbContext.Characters.Where(c => characterIds.Contains(c.CharacterId)).ToListAsync();
            return characters;
        }
    }
}
