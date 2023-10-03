using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopWave.Entity
{
	[Table("Avatar")]
	public class Avatar
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int AvatarId { get; set; }



		[Required]
		public byte[] Data { get; set; }


		[NotMapped]
		public ICollection<UserAvatar> UserAvatars { get; set; }
	}
}
