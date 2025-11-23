using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using GameMovieStore.Contracts.Services;
using GameMovieStore.Models;
using GameMovieStore.Persistence.DbContext;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace GameMovieStore.Implementations.Services
{
    public class AuthService : IAuthService
    {
        private readonly GameMovieStoreDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(GameMovieStoreDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task LoginAsync(string username, string password)
        {
            var hashed = HashPassword(password);

            var user = _dbContext.Users.FirstOrDefault(u =>
                u.Username == username &&
                u.PasswordHash == hashed);

            if (user == null)
                throw new ArgumentException("Invalid username or password");

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.ContentRole)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await _httpContextAccessor.HttpContext!.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal
            );
        }

        public async Task RegisterAsync(string name, string surname, string username, string password)
        {
            if (_dbContext.Users.Any(u => u.Username == username))
                throw new ArgumentException("Username already taken");

            var hashed = HashPassword(password);

            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = name,
                Surname = surname,
                Username = username,
                PasswordHash = hashed,
                ContentRole = "Gamer"
            };

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task LogoutAsync()
        {
            await _httpContextAccessor.HttpContext!.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }


        private string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha.ComputeHash(bytes);
            return Convert.ToHexString(hash);
        }
    }
}