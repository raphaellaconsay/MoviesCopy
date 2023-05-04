namespace MovieApi.Dtos.Movie
{
    public class MovieDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Director { get; set; }
        public int Duration { get; set; }
        public string? ReleaseDate { get; set; }
        public int Rate { get; set; }
    }
}
