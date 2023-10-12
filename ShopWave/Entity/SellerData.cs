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


		[Required, Column(TypeName = "varchar(100)")]
		[StringLength(100)]
		public string FIO { get; set; }


		[Required, Column(TypeName = "varchar(13)")]
		[StringLength(13)]
		public string IDPN { get; set; }


		[Required, Column(TypeName = "varchar(60)")]
		[StringLength(60)]
		public string FullAddress { get; set; }

        [Required]
        [MaxLength]
        public string Data { get; set; }
    }
}
