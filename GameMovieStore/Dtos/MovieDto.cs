namespace GameMovieStore.Dtos
{
    public class MovieDto
    {
        public long Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public string Url { get; set; } = default!;
    }
}