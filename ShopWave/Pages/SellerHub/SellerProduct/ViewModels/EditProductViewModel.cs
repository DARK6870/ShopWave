using ShopWave.Entity;

namespace ShopWave.Pages.SellerHub.SellerProduct.ViewModels
{
    public class EditProductViewModel
    {
        public List<Categories> categories { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public short CategoryId { get; set; }
    }
}
