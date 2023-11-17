using ShopWave.Entity;

namespace ShopWave.Pages.SellerHub.SellerProduct.ViewModels
{
    public class NewProductViewModel
    {
        public List<Categories>? categoryes { get; set; }
        public string? ProductName { get; set; }
        public string? ProductDescription { get; set; }
        public short CategoryId { get; set; }
        public string? VariationName { get; set; }
        public int Quantity { get; set; }
        public Decimal Price { get; set; }
        public IFormFile? Image {get; set;}
        public string? SellerId { get; set; }

    }
}
