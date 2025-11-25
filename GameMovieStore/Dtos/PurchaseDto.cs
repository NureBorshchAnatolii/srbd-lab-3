using GameMovieStore.Enums;

namespace GameMovieStore.Dtos
{
    public class PurchaseDto<T> : IPurchaseDto
    {
        public long Id { get; set; }
        public DateTime CreateDate { get; set; }
        public ProductTypes ProductType { get; set; }
        public T Item { get; set; } = default!;

        object IPurchaseDto.Item => Item!;
    }
}