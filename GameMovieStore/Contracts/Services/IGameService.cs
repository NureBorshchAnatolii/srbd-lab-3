using GameMovieStore.Dtos;

namespace GameMovieStore.Contracts.Services
{
    public interface IGameService
    {
        Task<IEnumerable<GameDto>> GetGamesDtosAsync();
        Task CreateGameAsync(GameDto gameDto);
        Task DeleteGameAsync(long gameId);
    }
}