using ShopWave.Entity;

namespace ShopWave.Pages.OrderPage.ViewModels
{
    public class OrderViewModel
    {
        public List<Order> Order { get; set; }
        public int totalpages { get; set; }
        public int page { get ; set; }
    }
}
