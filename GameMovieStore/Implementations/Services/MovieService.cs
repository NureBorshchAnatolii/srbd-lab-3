using GameMovieStore.Contracts.Repositories;
using GameMovieStore.Contracts.Services;
using GameMovieStore.Dtos;
using GameMovieStore.Models;

namespace GameMovieStore.Implementations.Services
{
    public class MovieService : IMovieService
    {
        private readonly IGenericRepository<Movie> _movieRepository;

        public MovieService(IGenericRepository<Movie> movieRepository)
        {
            _movieRepository = movieRepository;
        }
        
        public async Task<IEnumerable<MovieDto>> GetMovieDtosAsync()
        {
            var movies = await _movieRepository.GetAllAsync();
            var mappedMovies = movies.Select(MapToMovieDto);
            return mappedMovies;
        }

        private static MovieDto MapToMovieDto(Movie movie)
        {
            return new MovieDto()
            {
                Id = movie.Id,
                Name = movie.Name,
                Description = movie.Description,
                Url = movie.Url,
            };
        }
    }
}