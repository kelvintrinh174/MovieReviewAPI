using MovieReviewAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieReviewAPI.Services
{
    public interface IMovieRepository
    {
        Task<bool> MovieExists(int movieId);
        Task<IEnumerable<Movie>> GetMovies();
        Task<Movie> GetMovieById(int? movieId);
        Task UpdateMovie(Movie movie);
        Task AddMovie(Movie movie);
        Task DeleteMovie(int movieId);
    }
}
