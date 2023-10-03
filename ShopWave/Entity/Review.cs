using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopWave.Entity
{
	[Table("Review")]
	public class Review
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ReviewId { get; set; }


		[Required, ForeignKey(nameof(AppUsers))]
		public string AppUserId { get; set; }
		public virtual AppUser AppUsers { get; set; }



		[Required, ForeignKey(nameof(Products))]
		public int ProductId { get; set; }
		public virtual Product Products { get; set; }
	}
}
