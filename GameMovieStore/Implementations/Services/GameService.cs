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

        public async Task CreateGameAsync(GameDto gameDto)
        {
            Game newGame = new Game()
            {
                Name = gameDto.Name,
                Description = gameDto.Description,
                Genre = gameDto.Genre,
            };
            await _gameRepository.AddAsync(newGame);
        }

        public async Task DeleteGameAsync(long gameId)
        {
            Game? game = await _gameRepository.GetByIdAsync(gameId);
            await _gameRepository.DeleteAsync(game);
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