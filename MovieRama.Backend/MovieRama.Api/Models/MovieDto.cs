using System.ComponentModel.DataAnnotations;

namespace MovieRama.Api.Models
{
    public class MovieDto
    {
        [MaxLength(50)]
        public string Title { get; set; } = String.Empty;
        [MaxLength(100)]
        public string ImageUrl { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public Guid UserID { get; set; }
    }
}
