namespace GameMovieStore.Models
{
    public class Purchase
    {
        public long Id { get; set; }
        public DateTime CreateDate { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; } = default!;
        
        public long? MovieId { get; set; }
        public Movie? Movie { get; set; }

        public long? GameId { get; set; }
        public Game? Game { get; set; }
    }
}