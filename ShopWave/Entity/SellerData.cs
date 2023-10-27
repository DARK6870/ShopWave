using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopWave.Entity
{
	[Table("SellerData")]
	public class SellerData
	{
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }


        [Required, ForeignKey(nameof(AppUsers))]
		public string AppUserId { get; set; }
		public virtual AppUser AppUsers { get; set; }


		[Required, Column(TypeName = "varchar(30)")]
		[StringLength(30)]
		public string FirstName { get; set; }
		
		[Required, Column(TypeName = "varchar(30)")]
		[StringLength(30)]
		public string LastName { get; set; }


		[Required, Column(TypeName = "varchar(13)")]
		[StringLength(13)]
		public string IDPN { get; set; }

		[Required, Column(TypeName = "date")]
		public DateTime PassSDate { get; set; }

        [Required, Column(TypeName = "date")]
        public DateTime PassEDate { get; set; }

        [Required, Column(TypeName = "date")]
        public DateTime DateBrth { get; set; }

		[Required, Column(TypeName = "varchar(15)")]
		[StringLength(15)]
		public string PassNr { get; set; }


        [Required]
        [MaxLength]
        public string Data { get; set; }
    }
}
