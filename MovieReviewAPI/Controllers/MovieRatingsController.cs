using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieReviewAPI.Models;

namespace MovieReviewAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieRatingsController : ControllerBase
    {
        private readonly MovieAPIDbContext _context;

        public MovieRatingsController(MovieAPIDbContext context)
        {
            _context = context;
        }

        // GET: api/MovieRatings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieRating>>> GetMovieRating()
        {
            return await _context.MovieRating.ToListAsync();
        }

        // GET: api/MovieRatings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MovieRating>> GetMovieRating(int id)
        {
            var movieRating = await _context.MovieRating.FindAsync(id);

            if (movieRating == null)
            {
                return NotFound();
            }

            return movieRating;
        }

        // PUT: api/MovieRatings/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovieRating(int id, MovieRating movieRating)
        {
            if (id != movieRating.MovieRatingId)
            {
                return BadRequest();
            }

            _context.Entry(movieRating).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieRatingExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/MovieRatings
        [HttpPost]
        public async Task<ActionResult<MovieRating>> PostMovieRating(MovieRating movieRating)
        {
            //find existing movierating with username and movieId
            var movie = await FindMovieRatingAndUser(movieRating.UserName, movieRating.MovieId);
            if (movie != null)
            {
                //Update rating if exists
                //movieRating.MovieRatingId = movie.MovieRatingId;
                _context.Update(movie);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            //add new rating
            _context.MovieRating.Add(movieRating);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetMovieRating", new { id = movieRating.MovieRatingId }, movieRating);
           
        }

        // DELETE: api/MovieRatings/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MovieRating>> DeleteMovieRating(int id)
        {
            var movieRating = await _context.MovieRating.FindAsync(id);
            if (movieRating == null)
            {
                return NotFound();
            }

            _context.MovieRating.Remove(movieRating);
            await _context.SaveChangesAsync();

            return movieRating;
        }

        private bool MovieRatingExists(int id)
        {
            return _context.MovieRating.Any(e => e.MovieRatingId == id);
        }

        private async Task<MovieRating> FindMovieRatingAndUser(string username,int movieId)
        {
            MovieRating movie = null;
            try
            {
                movie = await _context.MovieRating.SingleAsync(e => e.UserName == username & e.MovieId == movieId);
            }
            catch
            {
                return null;
            }
            return movie;  
        }
    }
}
