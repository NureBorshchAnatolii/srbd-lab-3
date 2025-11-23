namespace GameMovieStore.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Surname { get; set; } = default!;
        public string? Username { get; set; }
        public string PasswordHash { get; set; } = default!;
        public string ContentRole { get; set; } = default!;

        public ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();
    }
}