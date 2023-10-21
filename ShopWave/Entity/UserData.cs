using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopWave.Entity
{
	[Table("UserData")]
	public class UserData
	{
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }


        [ForeignKey(nameof(AppUsers))]
		public string AppUserId { get; set; }
		public virtual AppUser AppUsers { get; set; }


		[Required, Column(TypeName = "varchar(15)")]
		[StringLength(15)]
		public string PhoneNumber { get; set; }


		[Required, Column(TypeName = "varchar(20)")]
		[StringLength(20)]
		public string FirtName { get; set; }

        [Required, Column(TypeName = "varchar(20)")]
        [StringLength(20)]
        public string LastName { get; set; }


        [Required, ForeignKey(nameof(Countryess))]
		public byte CountryId { get; set; }
		public virtual Countryes Countryess { get; set; }


		[Required, Column(TypeName = "varchar(35)")]
		[StringLength(35)]
		public string Location { get; set; }


		[Required, Column(TypeName = "varchar(40)")]
		[StringLength(40)]
		public string Address { get; set; }


		[Required, Column(TypeName = "varchar(10)")]
		[StringLength(10)]
		public string PostalCode { get; set; }
	}
}
