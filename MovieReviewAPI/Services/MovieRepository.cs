using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieReviewAPI.Models;

namespace MovieReviewAPI.Services
{
    public class MovieRepository : IMovieRepository<Movie>
    {
        private readonly MovieAPIDbContext _context;

        public MovieRepository(MovieAPIDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Movie>> GetAll()
        {
            List<Movie> list = await _context.Movie
                                                .Include(c => c.MovieRating)
                                                .Include(c => c.MovieComment)
                                                .ToListAsync();
            
            return list.OrderByDescending(x => x.DateCreated).ToList();
        }

        public async Task<IEnumerable<Movie>> GetByTitle(string movieTitle)
        {
            List<Movie> list = await _context.Movie.Where(movie => movie.MovieTitle.Contains(movieTitle)).ToListAsync();
            return list.OrderByDescending(x => x.DateCreated).ToList(); ;
        }

        public async Task<IEnumerable<Movie>> GetByActor(string movieActor)
        {
            List<Movie> list = await _context.Movie.Where(movie => movie.Actor.Contains(movieActor)).ToListAsync();
            return list.OrderByDescending(x => x.DateCreated).ToList(); ;
        }


        public async Task<Movie> GetById(int? MovieId)
        {
            return await _context.Movie
                                 .Include(c => c.MovieComment)
                                 .Include(c => c.MovieRating)
                                 .SingleOrDefaultAsync(item => item.MovieId == MovieId.Value);                                
        }

        public async Task Add(Movie movie)
        {
            _context.Movie.Add(movie);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int movieId)
        {
            var movie = await _context.Movie.FindAsync(movieId);
            if(movie != null)
            {
                _context.Movie.Remove(movie);
                await _context.SaveChangesAsync(); ;
            }
              
        }


        public async Task<bool> isExists(int movieId)
        {
            return await _context.Movie.AnyAsync<Movie>(c => c.MovieId == movieId);
        }
     
        public async Task Update(Movie movie)
        {
            // _context.Update(movie);
            _context.Entry(movie).State = EntityState.Modified;
            await _context.SaveChangesAsync();          
        }
    }
}
