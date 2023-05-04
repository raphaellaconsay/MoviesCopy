using System.ComponentModel.DataAnnotations;

namespace MovieApi.Dtos.Actor
{
    public class ActorCreationDto
    {
        [Required(ErrorMessage = "Actor Name is required.")]
        [MaxLength(50, ErrorMessage = "Maximum Name length is 50 characters")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Actor Gender is required.")]
        public string? Gender { get; set; }

        [Required(ErrorMessage = "Actor Birthday is required.")]
        public string? Birthday { get; set; }
    }
}
