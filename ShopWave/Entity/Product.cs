using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopWave.Entity
{
	[Table("Product")]
	public class Product
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ProductId { get; set; }


		[Required, Column(TypeName = "varchar(50)")]
		[StringLength(50)]
		public string ProductName { get; set; }


		[Required, Column(TypeName = "varchar(600)")]
		[StringLength(600)]
		public string Description { get; set; }


		[Required, ForeignKey(nameof(Categories))]
		public short CategoryId { get; set; }
		public virtual Categories Categories { get; set; }


		[NotMapped]
		public ICollection<ProductImage> ProductImages { get; set; }
		[NotMapped]
		public ICollection<Review> Reviews { get; set; }
	}
}
