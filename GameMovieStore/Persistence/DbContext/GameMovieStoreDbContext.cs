using GameMovieStore.Models;
using Microsoft.EntityFrameworkCore;

namespace GameMovieStore.Persistence.DbContext
{
    public class GameMovieStoreDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public GameMovieStoreDbContext(DbContextOptions<GameMovieStoreDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Purchase> Purchases { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GameMovieStoreDbContext).Assembly);
        }

        public void EnsureStoredProceduresAndFunctions()
        {
            var sqlFunction = @"
                CREATE OR ALTER FUNCTION MostPopularGameDay
                (
                    @StartDate DATE,
                    @EndDate DATE
                )
                RETURNS TABLE
                AS
                RETURN
                (
                    SELECT
                        CreateDate AS PurchaseDate,
                        COUNT(*) AS PurchaseCount
                        FROM Purchases
                        WHERE GameId IS NOT NULL
                        AND CreateDate BETWEEN @StartDate AND @EndDate
                        GROUP BY CreateDate
                        HAVING COUNT(*) = ( SELECT MAX(PurchaseCount)
                        FROM (
                        SELECT COUNT(*) AS PurchaseCount
                        FROM Purchases
                        WHERE GameId IS NOT NULL
                        AND CreateDate BETWEEN @StartDate AND @EndDate
                        GROUP BY CreateDate
                        ) AS SubT
                    )
                );";

            var sqlProcedure = @"
                CREATE OR ALTER PROCEDURE UpdateUserContentRole
                    @UserId UNIQUEIDENTIFIER
                AS
                BEGIN
                    DECLARE @GameCount INT = (
                        SELECT COUNT(*) FROM Purchases
                        WHERE UserId = @UserId AND GameId IS NOT NULL
                    );

                    DECLARE @MovieCount INT = (
                        SELECT COUNT(*) FROM Purchases
                        WHERE UserId = @UserId AND MovieId IS NOT NULL
                    );

                    IF (@GameCount = 0 AND @MovieCount = 0) OR (@GameCount = @MovieCount)
                    BEGIN
                        PRINT 'Визначити не можливо';
                        RETURN;
                    END;

                    DECLARE @NewRole VARCHAR(50);

                    IF @GameCount > @MovieCount
                        SET @NewRole = 'Gamer';
                    ELSE
                        SET @NewRole = 'Watcher';

                    UPDATE Users
                    SET ContentRole = @NewRole
                    WHERE Id = @UserId;

                    PRINT 'User role updated to ' + @NewRole;
                END;";

            Database.ExecuteSqlRaw(sqlFunction);
            Database.ExecuteSqlRaw(sqlProcedure);
        }
    }
}