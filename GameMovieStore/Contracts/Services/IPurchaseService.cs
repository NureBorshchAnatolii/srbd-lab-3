using GameMovieStore.Dtos;
using GameMovieStore.Enums;
using GameMovieStore.Models;

namespace GameMovieStore.Contracts.Services
{
    public interface IPurchaseService
    {   
        Task<IEnumerable<PurchaseDto>> GetUsersPurchases(Guid userId);
        Task CreatePurchase(Guid userId, long productId, ProductTypes productType);
    }
}