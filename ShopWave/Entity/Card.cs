using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopWave.Entity
{
	[Table("Card")]
	public class Card
	{
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


		[Required, ForeignKey(nameof(AppUsers))]
		public string AppUserId { get; set; }
		public virtual AppUser AppUsers { get; set; }



		[Required, ForeignKey(nameof(Products)	)]
		public int ProductId { get; set; }
		public virtual Product Products { get; set; }
	}
}
