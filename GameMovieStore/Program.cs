using GameMovieStore.Components;
using GameMovieStore.Contracts.Services;
using GameMovieStore.Implementations.Services;
using GameMovieStore.Persistence.DbContext;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

#region Myservices

builder.Services.AddDbContext<GameMovieStoreDbContext>(options => {
    string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    
    if (connectionString == null)
        throw new NullReferenceException("Connection string is null");
    
    options.UseSqlServer(connectionString);
});

// Security
builder.Services.AddHttpContextAccessor(); 
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/login";
        options.LogoutPath = "/logout";
        options.ExpireTimeSpan = TimeSpan.FromHours(1);
    });
builder.Services.AddAuthorization();

//Dependencies
builder.Services.AddScoped<IAuthService, AuthService>();

#endregion


builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();