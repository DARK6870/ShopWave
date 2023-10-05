using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopWave.Entity
{
	[Table("UserData")]
	public class UserData
	{
		[ForeignKey(nameof(AppUsers))]
		public string AppUserId { get; set; }
		public virtual AppUser AppUsers { get; set; }


		[Required, ForeignKey(nameof(Avatars))]
		public int AvatarId { get; set; }
		public virtual Avatar Avatars { get; set; }



		[Required, Column(TypeName = "varchar(15)")]
		[StringLength(15)]
		public string PhoneNumber { get; set; }


		[Required, Column(TypeName = "varchar(60)")]
		[StringLength(60)]
		public string FIO { get; set; }


		[Required, ForeignKey(nameof(Countryess))]
		public byte CountryId { get; set; }
		public virtual Countryes Countryess { get; set; }


		[Required, Column(TypeName = "varchar(35)")]
		[StringLength(35)]
		public string Location { get; set; }


		[Required, Column(TypeName = "varchar(30)")]
		[StringLength(30)]
		public string Street { get; set; }


		[Required, Column(TypeName = "varchar(10)")]
		[StringLength(10)]
		public string PostalCode { get; set; }
	}
}
