using GameMovieStore.Enums;

namespace GameMovieStore.Dtos
{
    public class PurchaseDto
    {
        public long Id { get; set; }
        public DateTime CreateDate { get; set; }
        public ProductTypes ProductType { get; set; }
        public object Item { get; set; } = null!;
    }
}