using ShopWave.Entity;

namespace ShopWave.Pages.CartPage.Models
{
    public class CartViewModel
    {
        public List<Cart>? Carts { get; set; }
        public string? CountryName { get; set; }
        public byte DeliveryPrice { get; set; }
    }
}
