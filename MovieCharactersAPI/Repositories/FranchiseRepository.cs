using Microsoft.EntityFrameworkCore;
using MovieCharactersAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCharactersAPI.Repositories
{
    /// <summary>
    /// Class <c>FranchiseRepository</c> is the repository for franchises. 
    /// The repository is responsible for the communication with the database.
    /// Inherits from IFranchiseRepository
    /// </summary>
    public class FranchiseRepository : IFranchiseRepository
    {

        private readonly MovieCharacterDbContext _dbContext;
     
        public FranchiseRepository(MovieCharacterDbContext context)
        {
            _dbContext = context;
        }

        /// <summary>
        /// Returns all franchises in the database.
        /// </summary>
        /// <returns>A List of franchises</returns>
        public async Task<IEnumerable<Franchise>> GetAll()
        {
            return await _dbContext.Franchises.Include(m => m.Movies).ToListAsync();
        }

        /// <summary>
        /// Returns a franchise object where franchise id equals the input id.
        /// </summary>
        /// <param name="id">franchise id</param>
        /// <returns>A franchise object</returns>
        public async Task<Franchise> GetById(int id)
        {
            Franchise franchise = await _dbContext.Franchises.Include(m => m.Movies).Where(f => f.FranchiseId == id).FirstAsync();
            return franchise;
        }
 
        /// <summary>
        /// Updating an existing character.
        /// </summary>
        /// <param name="entity">Takes in a character object</param>
        /// <returns>Nothing</returns>
        public async Task Update(Franchise entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Adding a new character.
        /// </summary>
        /// <param name="entity">Takes in a character object</param>
        /// <returns>A character object</returns>
        public async Task<Franchise> Create(Franchise entity)
        {
            await _dbContext.Franchises.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        /// <summary>
        /// Checks if a franchise exists.
        /// True if the franchise exists.
        /// False if the franchise does not exists.
        /// </summary>
        /// <param name="id">franchise id</param>
        /// <returns>A bool</returns>
        public bool Exist(int id)
        {
            return _dbContext.Franchises.Any(f => f.FranchiseId == id);
        }

        /// <summary>
        /// Updating the foreign key for a movie so it exists in a franchise.
        /// </summary>
        /// <param name="movieId">Id for the movie</param>
        /// <param name="franciseId">Id for the franchise</param>
        /// <returns></returns>
        public async Task AddMovie(int movieId, int franciseId)
        {
            if(!_dbContext.Movies.Any(m => m.MovieId == movieId))
            {
                throw new Exception();
            }
            Movie movie = await _dbContext.Movies.FindAsync(movieId);
            movie.FranchiseId = franciseId;
            _dbContext.Update(movie);
            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Adding a movie to the database and adding it to a franchise.
        /// </summary>
        /// <param name="movie">The movie object to add</param>
        /// <param name="franchiseId">Id for the franchise</param>
        public async Task AddMovie(Movie movie, int franchiseId)
        {
            // Create and add movie
            movie.FranchiseId = franchiseId;
            await _dbContext.Movies.AddAsync(movie);
            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes a franchise that equals the input id. 
        /// </summary>
        /// <param name="id">takes in a franchise id</param>
        /// <returns>Nothing</returns>
        public async Task Delete(int id)
        {
            // Set FranchiseId to null in the Movies table
            List<Movie> movies = await _dbContext.Movies.Where(m => m.FranchiseId == id).ToListAsync();
            foreach (Movie movie in movies)
            {
                movie.FranchiseId = null;
                _dbContext.Update(movie);
            }

            // Remove the franchise
            Franchise franchise = await GetById(id);
            _dbContext.Remove(franchise);

            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Removes a movie from a franchise.
        /// </summary>
        /// <param name="franchiseId">id for the franchise</param>
        /// <param name="movieId">id for the movie to remove</param>
        /// <returns></returns>
        public async Task RemoveMovie(int franchiseId, int movieId)
        {
            var movies = await _dbContext.Franchises.Where(fId => fId.FranchiseId == franchiseId).SelectMany(m => m.Movies).ToListAsync();
            foreach(Movie movie in movies)
            {
                if (movie.MovieId == movieId)
                {
                    movie.FranchiseId = null;
                    _dbContext.Movies.Update(movie);
                }
            }
            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Get all movies in a franchise.
        /// </summary>
        /// <param name="franchiseId">franchise id</param>
        /// <returns>A List of movies</returns>
        public async Task<IEnumerable<Movie>> GetMovies(int franchiseId)
        {
            return await _dbContext.Movies.Include(m => m.CharacterMovies).Where(m => m.FranchiseId == franchiseId).ToListAsync();
        }

        /// <summary>
        /// Gets all characters in a franchise. 
        /// </summary>
        /// <param name="franchiseId">id for the franchise</param>
        /// <returns>A List of characters</returns>
        public async Task<IEnumerable<Character>> GetCharacters(int franchiseId)
        {
            List<int> moviesIds = await _dbContext.Movies.Where(f => f.FranchiseId == franchiseId).Select(m => m.MovieId).ToListAsync();
            List<int> characterIds = await _dbContext.CharacterMovies.Where(cm => moviesIds.Contains(cm.MovieId)).Select(cm => cm.CharacterId).ToListAsync();
            List<Character> characters = await _dbContext.Characters.Include(c => c.CharacterMovies).Where(c => characterIds.Contains(c.CharacterId)).ToListAsync();
            return characters;
        }
    }
}
