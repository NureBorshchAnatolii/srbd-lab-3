using GameMovieStore.Dtos;
using GameMovieStore.Models;

namespace GameMovieStore.Contracts.Services
{
    public interface IMovieService
    {
        Task<IEnumerable<MovieDto>> GetMovieDtosAsync();
        Task CreateMovieAsync(MovieDto movieDto);
        Task DeleteMovieAsync(long movieId);
    }
}