using GameMovieStore.Dtos;
using GameMovieStore.Enums;
using GameMovieStore.Models;

namespace GameMovieStore.Contracts.Services
{
    public interface IPurchaseService
    {   
        Task<IEnumerable<IPurchaseDto>> GetUsersPurchases(Guid userId);
        Task CreatePurchase(Guid userId, long productId, ProductTypes productType);
        Task<IEnumerable<PurchasePerDay>> GetMostPopularGameDayAsync(DateTime start, DateTime end);
        Task<string> UpdateUserContentRoleAsync(Guid userId);
        Task<string> GetUserContentRoleAsync(Guid userId);
    }
}