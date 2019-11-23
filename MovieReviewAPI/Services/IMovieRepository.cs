using MovieReviewAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieReviewAPI.Services
{
    public interface IMovieRepository<E>
    {
        Task<bool> isExists(int Id);
        Task<IEnumerable<E>> GetAll();

        Task<IEnumerable<E>> GetByTitle(string movieTitle);
        Task<IEnumerable<E>> GetByActor(string movieActor);
        Task<E> GetById(int? Id);
        Task Update(E e);
        Task Add(E e);
        Task Delete(int Id);
    }
}
