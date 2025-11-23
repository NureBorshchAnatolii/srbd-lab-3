namespace GameMovieStore.Contracts.Services
{
    public interface IAuthService
    {
        Task LoginAsync(string username, string password);
        Task RegisterAsync(string name, string surname, string username, string password);
        Task LogoutAsync();
    }
}