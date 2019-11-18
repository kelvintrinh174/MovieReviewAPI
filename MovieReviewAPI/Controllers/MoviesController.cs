using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieReviewAPI.DTOModels;
using MovieReviewAPI.Models;
using MovieReviewAPI.Services;

namespace MovieReviewAPI.Controllers
{   
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        // private readonly MovieAPIDbContext _context;
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;

        public MoviesController(IMovieRepository movieRepository,IMapper mapper)
        {
            //_context = context;
            _movieRepository = movieRepository;
            _mapper = mapper;
        }

        // GET: api/Movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovie()
        {
            var movie = await _movieRepository.GetMovies();
            var results = _mapper.Map<IEnumerable<MovieDto>>(movie);
               
            return Ok(results);
        }

        // GET: api/Movies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovie(int id)
        {
            var movie = await _movieRepository.GetMovieById(id);

            if (movie == null)
            {
                return NotFound();
            }

            var results = _mapper.Map<MovieDto>(movie);

            return Ok(results);
        }

        // PUT: api/Movies/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(int id, MovieDto movieDto)
        {
            if (id != movieDto.MovieId)
            {
                return BadRequest();
            }
           
            try
            {
               var movie = await _movieRepository.GetMovieById(id);
               _mapper.Map(movieDto,movie);
               await _movieRepository.UpdateMovie(movie); 
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await MovieExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        // POST: api/Movies
        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovie(Movie movieDto)
        {
            if (movieDto == null) BadRequest();

            var movie = _mapper.Map<Movie>(movieDto);
            await _movieRepository.AddMovie(movie);
            return CreatedAtAction("GetMovie", new { id = movie.MovieId }, movie);
        }

        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Movie>> DeleteMovie(int id)
        {
            var movie = _movieRepository.GetMovieById(id);
            if (movie == null)
            {
                return NotFound();
            }
            //var movie = _mapper.Map<Movie>(movieDt);
            await _movieRepository.DeleteMovie(id);

            return Ok();
        }

        private async Task<bool> MovieExists(int id)
        {           
            return await _movieRepository.MovieExists(id);
        }
    
    }
}
