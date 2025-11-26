namespace GameMovieStore.Models
{
    public class Game
    {
        public long Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public string Genre { get; set; } = default!;
        
        public ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();
    }
}