using System.ComponentModel.DataAnnotations;

namespace MovieApi.Dtos.Award
{
    public class AwardUpdateDto
    {
        [Required(ErrorMessage = "Award Name is required.")]
        [MaxLength(50, ErrorMessage = "Maximum Name length is 50 characters")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Award Year is required.")]
        [Range(1980, 2050, ErrorMessage = "Award Year must be between 1980 and 2050.")]
        public int Year { get; set; }
    }
}
