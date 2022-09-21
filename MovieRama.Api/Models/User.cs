using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MovieRama.Api.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        public string Username { get; set; } = String.Empty;
        [JsonIgnore]
        public byte[] PasswordHash { get; set; }
        [JsonIgnore]
        public byte[] PasswordSalt { get; set; }
        public string FirstName { get; set; }= String.Empty;
        public string LastName { get; set; } = String.Empty;

        public ICollection<Movie> SubmittedMovies { get; set; }
    }
}
