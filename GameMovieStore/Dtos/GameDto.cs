namespace GameMovieStore.Dtos
{
    public class GameDto
    {
        public long Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public string Genre { get; set; } = default!;
    }
}