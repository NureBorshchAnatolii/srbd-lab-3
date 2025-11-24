using GameMovieStore.Contracts.Repositories;
using GameMovieStore.Contracts.Services;
using GameMovieStore.Dtos;
using GameMovieStore.Enums;
using GameMovieStore.Models;
using GameMovieStore.Persistence.DbContext;
using Microsoft.EntityFrameworkCore;

namespace GameMovieStore.Implementations.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly GameMovieStoreDbContext _context;
        private readonly IGenericRepository<Purchase> _purchaseRepository;
        private readonly IGenericRepository<Game> _gameRepository;
        private readonly IGenericRepository<Movie> _movieRepository;

        public PurchaseService(IGenericRepository<Purchase> purchaseRepository
        , IGenericRepository<Game> gameRepository
        , IGenericRepository<Movie> movieRepository
        , GameMovieStoreDbContext context)
        {
            _context = context;
            _movieRepository = movieRepository;
            _gameRepository = gameRepository;
            _purchaseRepository = purchaseRepository;
        }

        public async Task<IEnumerable<PurchaseDto>> GetUsersPurchases(Guid userId)
        {
            var usersPurchases = await _context.Purchases
                .AsNoTracking()
                .Include(x => x.Movie)
                .Include(x => x.Game)
                .Where(x => x.UserId == userId)
                .ToListAsync();

            var mappedPurchases = usersPurchases.Select(MapToPurchaseDto);
            
            return mappedPurchases;
        }

        public async Task CreatePurchase(Guid userId, long productId, ProductTypes productType)
        {
            if (productType == ProductTypes.Game)
            {
                var game = await CreateGamePurchase(userId, productId);
                
                await _purchaseRepository.AddAsync(game);
            }
            else if (productType == ProductTypes.Movie)
            {
                var movie = await CreateMoviePurchase(userId, productId);
                
                await _purchaseRepository.AddAsync(movie);
            }
        }

        private async Task<Purchase> CreateGamePurchase(Guid userId, long gameId)
        {
            var gameNotExist = await IsGameNotExist(gameId);
            if(gameNotExist)
                throw new ArgumentException("Game not exist");

            return new Purchase()
            {
                CreateDate = DateTime.Now,
                GameId = gameId,
                UserId = userId,
                MovieId = null
            };
        }
        
        private async Task<Purchase> CreateMoviePurchase(Guid userId, long movieId)
        {
            var movieNotExist = await IsMovieNotExist(movieId);
            if(movieNotExist)
                throw new ArgumentException("Movie not exist");

            return new Purchase()
            {
                CreateDate = DateTime.Now,
                GameId = null,
                UserId = userId,
                MovieId = movieId
            };
        }

        private async Task<bool> IsGameNotExist(long gameId)
        {
            var games = await _gameRepository.GetAllAsync();
            return games.All(x => x.Id != gameId);
        }
        
        private async Task<bool> IsMovieNotExist(long movieId)
        {
            var movies = await _movieRepository.GetAllAsync();
            return movies.All(x => x.Id != movieId);
        }

        private static PurchaseDto MapToPurchaseDto(Purchase purchase)
        {
            var purchaseDto = new PurchaseDto()
            {
                Id = purchase.Id,
                CreateDate = purchase.CreateDate,
            };
            
            object item = null;

            if (purchase.MovieId != null)
            {
                purchaseDto.ProductType = ProductTypes.Movie;
                purchaseDto.Item = new MovieDto()
                {
                    Id = purchase.MovieId.Value,
                    Name = purchase.Movie!.Name,
                    Description = purchase.Movie.Description,
                    Url = purchase.Movie!.Url,
                };
            }
            else if (purchase.GameId != null)
            {
                purchaseDto.ProductType = ProductTypes.Game;
                purchaseDto.Item = new GameDto()
                {
                    Id = purchase.GameId.Value,
                    Name = purchase.Game!.Name,
                    Description = purchase.Game.Description,
                    Genre = purchase.Game!.Genre,
                };
            }
            
            purchaseDto.Item = item;
            
            return purchaseDto;
        }
    }
}