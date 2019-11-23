using Microsoft.EntityFrameworkCore;
using MovieReviewAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieReviewAPI.Services
{
    public class MovieCommentRepository : IMovieRepository<MovieComment>
    {
        private readonly MovieAPIDbContext _context;

        public MovieCommentRepository(MovieAPIDbContext context)
        {
            _context = context;
        }


        public async Task Add(MovieComment e)
        {
            _context.MovieComment.Add(e);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int Id)
        {
            var movieComment = await _context.MovieComment.FindAsync(Id);
            if (movieComment != null)
            {
                _context.MovieComment.Remove(movieComment);
                await _context.SaveChangesAsync(); ;
            }
        }

        public async Task<IEnumerable<MovieComment>> GetAll()
        {
            List<MovieComment> list = await _context.MovieComment.ToListAsync();

            return list.OrderByDescending(x => x.DateCreated).ToList();
        }

        public Task<IEnumerable<MovieComment>> GetByActor(string movieActor)
        {
            throw new NotImplementedException();
        }

        public async Task<MovieComment> GetById(int? Id)
        {
            return await _context.MovieComment
                                .SingleOrDefaultAsync(item => item.MovieCommentId == Id.Value);
        }

        public Task<IEnumerable<MovieComment>> GetByTitle(string movieTitle)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> isExists(int Id)
        {
            return await _context.MovieComment.AnyAsync<MovieComment>(c => c.MovieCommentId == Id);
        }

        public Task Update(MovieComment e)
        {
            throw new NotImplementedException();
        }
    }
}
