using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopWave.Entity
{
	public class AppUser : IdentityUser
	{
		[NotMapped]
		public virtual ICollection<Review> Reviews { get; set; }
		[NotMapped]
		public virtual ICollection<UserData> UserAvatars { get; set; }
		[NotMapped]
		public virtual ICollection<Product> Products { get; set; }
		[NotMapped]
		public virtual ICollection<Card> Cards { get; set; }
		[NotMapped]
		public virtual ICollection<Order> Orders { get; set; }
	}
}
