using System.ComponentModel.DataAnnotations;

namespace SpareParts.Vehicle.Api.Models.Movie
{
    public class PostModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public int Year { get; set; }
    }
}
