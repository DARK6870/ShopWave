using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopWave.Entity
{
	[Table("ProductImage")]
	public class ProductImage
	{
		[Required, ForeignKey(nameof(Products))]
		public int ProductId { get; set; }
		public virtual Product Products { get; set; }


		[Required, ForeignKey(nameof(Images))]
		public int ImageId { get; set; }
		public virtual Image Images { get; set; }
	}
}
