using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieReviewAPI.Models;

namespace MovieReviewAPI.Services
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MovieAPIDbContext _context;

        public MovieRepository(MovieAPIDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Movie>> GetMovies()
        {
            return await _context.Movie.ToListAsync();
        }


        public async Task<Movie> GetMovieById(int? MovieId)
        {           
            return await _context.Movie
                                 .SingleOrDefaultAsync(item => item.MovieId == MovieId.Value);

        }

        public async Task AddMovie(Movie movie)
        {
            _context.Movie.Add(movie);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMovie(int movieId)
        {
            var movie = await _context.Movie.FindAsync(movieId);
            if(movie != null)
            {
                _context.Movie.Remove(movie);
                await _context.SaveChangesAsync(); ;
            }
              
        }


        public async Task<bool> MovieExists(int movieId)
        {
            return await _context.Movie.AnyAsync<Movie>(c => c.MovieId == movieId);
        }
     
        public async Task UpdateMovie(Movie movie)
        {
            // _context.Update(movie);
            _context.Entry(movie).State = EntityState.Modified;
            await _context.SaveChangesAsync();          
        }
    }
}
