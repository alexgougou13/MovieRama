using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MovieRama.Api.Models
{
    public class MovieRating
    {
        [Key, Column(Order = 0)]
        public Guid MovieID { get; set; }
        [Key, Column(Order = 1)]
        public Guid UserID { get; set; }
        public bool Rating { get; set; }

    }
}
