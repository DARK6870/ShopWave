using System.ComponentModel.DataAnnotations;

namespace ShopWave.Pages.AuthorizePage.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }

}
