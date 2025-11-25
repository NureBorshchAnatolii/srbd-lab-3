using GameMovieStore.Enums;

namespace GameMovieStore.Dtos
{
    public interface IPurchaseDto
    {
        long Id { get; }
        DateTime CreateDate { get; }
        ProductTypes ProductType { get; }
        object Item { get; }
    }
}