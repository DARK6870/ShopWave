using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopWave.Entity
{
	[Table("Product")]
	public class Product
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ProductId { get; set; }


		[Required, ForeignKey(nameof(AppUsers))]
		public string AppUserId { get; set; }
		public virtual AppUser AppUsers { get; set; }


		[Required, Column(TypeName = "varchar(50)")]
		[StringLength(50)]
		public string ProductName { get; set; }


		[Required, Column(TypeName = "varchar(800)")]
		[StringLength(800)]
		public string Description { get; set; }



		[Column(TypeName = "Decimal(3,2)")]
		public decimal? AvgStars { get; set; }



		[Required, DefaultValue(0)]
		public int OrderCounts { get; set; }



		[Required, ForeignKey(nameof(Statuses))]
		public byte StatusId { get; set; }
		public virtual Status Statuses { get; set; }

		[Required, ForeignKey(nameof(Categoriess))]
		public short CategoryId { get; set; }
		public virtual Categories Categoriess { get; set; }



		[NotMapped]
		public ICollection<ProductImage> ProductImages { get; set; }
		[NotMapped]
		public ICollection<ProductVariation> ProductVariations { get; set; }
		[NotMapped]
		public ICollection<Order> Orders { get; set; }
	}
}
