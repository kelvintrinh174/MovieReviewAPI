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
    public class MovieCommentsController : ControllerBase
    {
        private readonly MovieAPIDbContext _context;

        public MovieCommentsController(MovieAPIDbContext context)
        {
            _context = context;
        }

        // GET: api/MovieComments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieComment>>> GetMovieComment()
        {
            return await _context.MovieComment.ToListAsync();
        }

        // GET: api/MovieComments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MovieComment>> GetMovieComment(int id)
        {
            var movieComment = await _context.MovieComment.FindAsync(id);

            if (movieComment == null)
            {
                return NotFound();
            }

            return movieComment;
        }

        // PUT: api/MovieComments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovieComment(int id, MovieComment movieComment)
        {
            if (id != movieComment.MovieCommentId)
            {
                return BadRequest();
            }

            _context.Entry(movieComment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieCommentExists(id))
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

        // POST: api/MovieComments
        [HttpPost]
        public async Task<ActionResult<MovieComment>> PostMovieComment(MovieComment movieComment)
        {
            _context.MovieComment.Add(movieComment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMovieComment", new { id = movieComment.MovieCommentId }, movieComment);
        }

        // DELETE: api/MovieComments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MovieComment>> DeleteMovieComment(int id)
        {
            var movieComment = await _context.MovieComment.FindAsync(id);
            if (movieComment == null)
            {
                return NotFound();
            }

            _context.MovieComment.Remove(movieComment);
            await _context.SaveChangesAsync();

            return movieComment;
        }

        private bool MovieCommentExists(int id)
        {
            return _context.MovieComment.Any(e => e.MovieCommentId == id);
        }
    }
}
