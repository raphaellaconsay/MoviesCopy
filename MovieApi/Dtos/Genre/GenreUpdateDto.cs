using System.ComponentModel.DataAnnotations;

namespace MovieApi.Dtos.Genre
{
    public class GenreUpdateDto
    {
        [Required(ErrorMessage = "Genre Name is required.")]
        [MaxLength(50, ErrorMessage = "Maximum Name length is 50 characters")]
        public string? Name { get; set; }
    }
}
