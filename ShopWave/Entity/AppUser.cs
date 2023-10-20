using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopWave.Entity
{
	public class AppUser : IdentityUser
	{
        public ICollection<Review> Reviews { get; set; }
		public ICollection<UserData> UserAvatars { get; set; }
		public  ICollection<Product> Products { get; set; }
		public ICollection<Cart> Cards { get; set; }
		public ICollection<Order> Orders { get; set; }
		public ICollection<SellerData> SellerDatas { get; set; }
		public ICollection<Avatar> Avatars { get; set; }
	}
}
