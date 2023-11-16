using ShopWave.Entity;
using ShopWave.Pages.OrderPage.ViewModels;

namespace ShopWave.Pages.SellerHub.SellerOrders.ViewModels
{
    public class OrderStatusViewModel
    {
        public SellerOrderViewModel order { get; set; }
        public List<Status> statuses { get; set; }
    }
}
