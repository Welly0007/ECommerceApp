namespace DomainLayer.Models
{
    public class Product : BaseEntity
    {
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public string Description { get; set; } = null!;
        public string PictureUrl { get; set; } = null!;
        public ProductType ProductType { get; set; } = null!;
        public int TypeId { get; set; }
        public ProductBrand ProductBrand { get; set; } = null!;
        public int BrandId { get; set; }

    }
}
