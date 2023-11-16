using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;

namespace ShopWave.Pages.SellerHub.SellerOrders.ViewModels
{
    public class SellerOrderViewModel
    {
        public int ID { get; set; }
        public string? ProductName { get; set; }
        public string? VariationName { get; set; }
        public Decimal totalPrice { get; set; }
        public string? Address { get; set; }
        public string? PostalCode { get; set; }
        public string? CountryName { get; set; }
        public string? Phone { get; set; }
        public string? Status { get; set; }
        public string? SellerId { get; set; }

        [MaxLength]
        public string? ImageData { get; set; }
    }
}
