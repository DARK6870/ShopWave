using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopWave.Entity
{
	[Table("Variation")]
	public class Variation
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int VariationId { get; set; }


		[Required, Column(TypeName = "varchar(10)")]
		[StringLength(10)]
		public string VariationName { get; set;}

		[NotMapped]
		public virtual ICollection<ProductVariation> ProductVariations { get; set; }
		[NotMapped]
		public virtual ICollection<Order> Orders { get; set; }
	}
}
