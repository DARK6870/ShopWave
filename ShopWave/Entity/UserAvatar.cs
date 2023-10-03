using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopWave.Entity
{
	[Table("UserAvatar")]
	public class UserAvatar
	{
		[ForeignKey(nameof(AppUsers))]
		public string AppUserId { get; set; }
		public virtual AppUser AppUsers { get; set; }


		[Required, ForeignKey(nameof(Avatars))]
		public int AvatarId { get; set; }
		public virtual Avatar Avatars { get; set; }
	}
}
