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


		[Required, Column(TypeName = "tinyint")]
		public byte NumOfStarts { get; set; }


		[Column(TypeName = "varchar(400)")]
		public string? ReviewText { get; set; }

		[Required, ForeignKey(nameof(Product))]
		public int ProductId { get; set; }
		public virtual Product Products { get; set; }


        [NotMapped]
        [MaxLength]
        public string? Avatar { get; set; }

    }
}
