using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopWave.Entity
{
	[Table("ProductVariation")]
	public class ProductVariation
	{
		[Required, ForeignKey(nameof(Products))]
		public int ProductId { get; set; }
		public virtual Product Products { get; set; }


		[Required, ForeignKey(nameof(Variations))]
		public int VariationId { get; set; }
		public virtual Variation Variations { get; set; }
	}
}
