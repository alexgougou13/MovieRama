using Microsoft.EntityFrameworkCore;
using MovieRama.Api.Models;

namespace MovieRama.Api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieRating> MovieRatings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MovieRating>()
                .HasKey(nameof(MovieRating.MovieID), nameof(MovieRating.UserID));

        }
    }
}
