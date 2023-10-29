using ShopWave.Entity;

namespace ShopWave.Pages.ProductPage.ViewModels
{
    public class ProductViewModel
    {
        public List<Product> products {  get; set; }
        public List<Categories> categories { get; set; }
        public string? categoryname { get; set; }
        public int page { get; set; }
        public int totalPages { get; set; }
    }
}
