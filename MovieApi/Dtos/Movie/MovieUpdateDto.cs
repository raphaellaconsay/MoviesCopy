using System.ComponentModel.DataAnnotations;

namespace MovieApi.Dtos.Movie
{
    public class MovieUpdateDto
    {
        [Required(ErrorMessage = "Movie Release Date is required.")]
        public string? ReleaseDate { get; set; }

        [Required(ErrorMessage = "Movie Rate is required.")]
        [Range(40, 100, ErrorMessage = "Movie Rate must be between 40 and 100.")]
        public int Rate { get; set; }
    }
}
