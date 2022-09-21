using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieRama.Api.Data;
using MovieRama.Api.Models;

namespace MovieRama.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieRatingsController : ControllerBase
    {
        private readonly DataContext _context;

        public MovieRatingsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/MovieRatings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieRating>>> GetMovieRatings()
        {
            return await _context.MovieRatings.ToListAsync();
        }

        // POST: api/MovieRatings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<MovieRating>> PostMovieRating(MovieRating movieRating)
        {

            var changed = false;
            var rating = await _context.MovieRatings.FirstOrDefaultAsync(x => x.MovieID == movieRating.MovieID && x.UserID == movieRating.UserID);
            var movie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == movieRating.MovieID);
            if (movie.UserId == movieRating.UserID)
            {
                return Ok("Not allowed");
            }
            if (rating == null)
            {
                _context.MovieRatings.Add(movieRating);
            }
            else
            {
                if (rating.Rating==movieRating.Rating)
                {
                    return Ok(false);
                }
                rating.Rating = movieRating.Rating;
                changed = true;
                if (movieRating.Rating)
                {
                    movie.HatesNum--;
                }
                else
                {
                    movie.LikesNum--;
                }
            }
            if (movieRating.Rating)
            {
                movie.LikesNum++;
            }
            else
            {
                movie.HatesNum++;
            }
            try
            {
                await _context.SaveChangesAsync();
                if (changed)
                {
                    return Ok(true);
                }
            }
            catch (DbUpdateException)
            {
                throw;
            }

            return Ok("added");
        }
        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<MovieRating>>> UserMovieRatings(Guid userId)
        {
            return await _context.MovieRatings.Where(x=> x.UserID==userId).ToListAsync();
        }
        // DELETE: api/MovieRatings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovieRating(Guid id)
        {
            var movieRating = await _context.MovieRatings.FindAsync(id);
            if (movieRating == null)
            {
                return NotFound();
            }

            _context.MovieRatings.Remove(movieRating);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
