using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MovieRama.Api.Models
{
    public class Movie
    {
        public Guid Id { get; set; }        
        public string Title { get; set; }=String.Empty;        
        public string Description { get; set; }=String.Empty;   
        public string NameOfUser { get; set; }=String.Empty;
        public string ImageUrl { get; set; } = String.Empty;
        public string CreatedDate { get; set; } = DateTime.Now.Date.ToString("dd/MM/yyyy");
        public int LikesNum { get; set; }
        public int HatesNum { get; set; }
        public Guid UserId { get; set; }
        [JsonIgnore]
        public User User { get; set; }
    }
}
