using GameMovieStore.Dtos;

namespace GameMovieStore.Contracts.Services
{
    public interface IGameService
    {
        Task<IEnumerable<GameDto>> GetGamesDtosAsync();
    }
}