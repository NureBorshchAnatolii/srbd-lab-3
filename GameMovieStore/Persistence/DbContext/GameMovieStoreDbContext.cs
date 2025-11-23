using GameMovieStore.Models;
using Microsoft.EntityFrameworkCore;

namespace GameMovieStore.Persistence.DbContext
{
    public class GameMovieStoreDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        
        public GameMovieStoreDbContext(DbContextOptions<GameMovieStoreDbContext> options) : base(options){}
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GameMovieStoreDbContext).Assembly);
        }
    }
}