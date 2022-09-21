using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieRama.Api.Data;
using MovieRama.Api.Models;
using MovieRama.Api.Utils;

namespace MovieRama.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly DataContext _context;

        public MoviesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies(string? name , Guid? userID,string? sortBy)
        {
            var movies=await _context.Movies.ToListAsync();
            if (!string.IsNullOrEmpty(userID.ToString()))
            {
                movies = movies.Where(m => m.UserId==userID).ToList();  
            }
            if (!string.IsNullOrEmpty(name))
            {              
                movies=movies.Where(m => m.Title.IsSimilarTo(name) || m.Title.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
                             
            }
            if (!string.IsNullOrEmpty(sortBy))
            {

                switch (sortBy)
                {
                    case "likes":
                        movies =movies.OrderByDescending(m=>m.LikesNum).ToList();
                    break;
                    case "hates":
                        movies = movies.OrderByDescending(m => m.HatesNum).ToList();
                        break;
                    case "date":
                        movies = movies.OrderByDescending(m => m.CreatedDate).ToList();
                        break;
                }

            }
            if (movies == null)
            {
                return NotFound();
            }
            return Ok(movies);
        }

        // GET: api/Movies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovie(Guid id)
        {
          if (_context.Movies == null)
          {
              return NotFound();
          }
            var movie = await _context.Movies.FindAsync(id);

            if (movie == null)
            {
                return NotFound();
            }

            return movie;
        }

        // PUT: api/Movies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(Guid id, Movie movie)
        {
            if (id != movie.Id)
            {
                return BadRequest();
            }

            _context.Entry(movie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(id))
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

        // POST: api/Movies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovie(MovieDto movie)
        {
          if (_context.Movies == null)
          {
              return Problem("Entity set 'DataContext.Movies'  is null.");
          }
            var user = _context.Users.Find(movie.UserID);

            var movieToCreate = new Movie()
            {
                Title= movie.Title, 
                Description= movie.Description,
                ImageUrl=movie.ImageUrl,
                UserId=user.Id,
                User = user,
                NameOfUser=user.FirstName+" "+user.LastName,
            };

            _context.Movies.Add(movieToCreate);
            await _context.SaveChangesAsync();

            return Ok("Movie created");
        }

        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(Guid id)
        {
            if (_context.Movies == null)
            {
                return NotFound();
            }
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MovieExists(Guid id)
        {
            return (_context.Movies?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
