using GameMovieStore.Contracts.Repositories;
using GameMovieStore.Contracts.Services;
using GameMovieStore.Dtos;
using GameMovieStore.Models;

namespace GameMovieStore.Implementations.Services
{
    public class GameService : IGameService
    {
        private readonly IGenericRepository<Game> _gameRepository;
        
        public GameService(IGenericRepository<Game> gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public async Task<IEnumerable<GameDto>> GetGamesDtosAsync()
        {
            var games = await _gameRepository.GetAllAsync();
            var mappedGames = games.Select(MapToGameDto);
            return mappedGames;
        }

        private static GameDto MapToGameDto(Game game)
        {
            return new GameDto()
            {
                Id = game.Id,
                Name = game.Name,
                Description = game.Description,
                Genre = game.Genre,
            };
        }
    }
}