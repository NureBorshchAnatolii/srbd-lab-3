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

        public async Task CreateMovieAsync(MovieDto movieDto)
        {
            Movie newMovie = new Movie()
            {
                Name = movieDto.Name,
                Description = movieDto.Description,
                Url = movieDto.Url,
            };
            await _movieRepository.AddAsync(newMovie);
        }

        public async Task DeleteMovieAsync(long movieId)
        {
            Movie movie = await _movieRepository.GetByIdAsync(movieId);
            await _movieRepository.DeleteAsync(movie);
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