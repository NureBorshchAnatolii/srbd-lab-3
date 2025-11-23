using GameMovieStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameMovieStore.Persistence.EntityConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users", table =>
            {
                table.HasCheckConstraint("CK_Users_ContentRole",
                    "ContentRole IN ('Gamer', 'Watcher')");
            });

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasDefaultValueSql("NEWID()");

            builder.Property(x => x.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.Surname)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.Username)
                .HasMaxLength(50);

            builder.HasIndex(x => x.Username)
                .IsUnique();

            builder.Property(x => x.PasswordHash)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.ContentRole)
                .HasMaxLength(50);
        }
    }
}