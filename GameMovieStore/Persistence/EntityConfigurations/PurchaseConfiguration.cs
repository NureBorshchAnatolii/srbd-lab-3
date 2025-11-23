using GameMovieStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameMovieStore.Persistence.EntityConfigurations
{
    public class PurchaseConfiguration : IEntityTypeConfiguration<Purchase>
    {
        public void Configure(EntityTypeBuilder<Purchase> builder)
        {
            builder.ToTable("Purchases", table =>
            {
                table.HasCheckConstraint(
                    "CK_Purchases_Movie_Game",
                    "(MovieId IS NOT NULL AND GameId IS NULL) OR (MovieId IS NULL AND GameId IS NOT NULL)"
                );
            });

            builder.HasKey(x => x.Id);

            builder.Property(x => x.CreateDate)
                .HasColumnType("date")
                .IsRequired();

            builder.HasOne(x => x.User)
                .WithMany(x => x.Purchases)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Movie)
                .WithMany(x => x.Purchases)
                .HasForeignKey(x => x.MovieId);

            builder.HasOne(x => x.Game)
                .WithMany(x => x.Purchases)
                .HasForeignKey(x => x.GameId);
            
        }
    }

}