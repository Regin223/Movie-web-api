using Microsoft.EntityFrameworkCore;
using MovieCharactersAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCharactersAPI.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MovieCharacterDbContext _dbContext;

        public MovieRepository(MovieCharacterDbContext conext)
        {
            _dbContext = conext;
        }
        public Task<Character> AddCharacterToMovie(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Movie> Create(Movie entity)
        {
            _dbContext.Movies.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public Task Delete(Movie entity)
        {
            throw new NotImplementedException();
        }

        public bool Exsist(int id)
        {
            return _dbContext.Movies.Any(m => m.MovieId == id);
        }

        public async Task<IEnumerable<Movie>> GetAll()
        {
            return await _dbContext.Movies.Include(m => m.Characters).ToListAsync();
        }

        public async Task<Movie> GetById(int id)
        {
            return await _dbContext.Movies.Include(c => c.Characters).Where(m => m.MovieId == id).FirstAsync();
        }

        public async Task Update(Movie entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}
