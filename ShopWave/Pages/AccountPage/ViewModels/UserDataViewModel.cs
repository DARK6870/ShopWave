using ShopWave.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ShopWave.Pages.AccountPage.ViewModels
{
    public class UserDataViewModel
    {
        public List<Countryes> countryes {  get; set; }
        public UserData UserDatas { get; set; }
        public string AppUserId { get; set; }
        public string? PhoneNumber { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public byte CountryId { get; set; }
        public string? Location { get; set; }
        public string? Address { get; set; }
        public string? PostalCode { get; set; }
    }
}
