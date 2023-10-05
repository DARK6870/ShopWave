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



		[Required, ForeignKey(nameof(AppUsers))]
		public string AppUserId { get; set; }
		public virtual AppUser AppUsers { get; set; }


		[Required, ForeignKey(nameof(Variations))]
		public int VartiationId { get; set; }
		public virtual Variation Variations { get; set; }


		[Required, ForeignKey(nameof(Statuses))]
		public byte StatusId { get; set; }
		public virtual Status Statuses { get; set; }
	}
}
