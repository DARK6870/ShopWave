using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopWave.Entity
{
	[Table("Categories")]
	public class Categories
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column(TypeName = "smallint")]
		public short CategoryId { get; set; }

		[Required, Column(TypeName = "varchar(25)")]
		[StringLength(25)]
		public string CategoryName { get; set; }


		[NotMapped]
		public ICollection<ProductCategory> ProductCategories { get; set; }
	}
}
