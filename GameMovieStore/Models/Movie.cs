namespace GameMovieStore.Models
{
    public class Movie
    {
        public long Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public string Url { get; set; } = default!;

        public ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();
    }
}