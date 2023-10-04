using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopWave.Entity
{
	[Table("ProductCategory")]
	public class ProductCategory
	{
		[Required, ForeignKey(nameof(Products))]
		public int ProductId { get; set; }
		public virtual Product Products { get; set; }




		[Required, ForeignKey(nameof(Categories))]
		public short CategoryId { get; set; }
		public virtual Categories Categories { get; set; }
	}
}
