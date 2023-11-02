using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopWave.Entity
{
	[Table("Order")]
	public class Order
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int OrderId { get; set; }


		[Required, ForeignKey(nameof(Products))]
		public int ProductId { get; set; }
		public virtual Product Products { get; set; }


		[Required, ForeignKey(nameof(ProductVariations))]
		public int VariationId { get; set; }
		public virtual ProductVariation ProductVariations { get; set; }


		[Required, ForeignKey(nameof(AppUsers))]
		public string AppUserId { get; set; }
		public virtual AppUser AppUsers { get; set; }


		[Required, ForeignKey(nameof(Statuses))]
		public byte StatusId { get; set; }
		public virtual Status Statuses { get; set; }


		[Required, Column(TypeName = "datetime")]
		public DateTime Date { get; set; }


		[Required, Column(TypeName = "Decimal(6,2)")]
		public decimal TotalPrice { get; set; }

	}
}
