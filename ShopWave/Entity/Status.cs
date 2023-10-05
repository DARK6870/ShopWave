using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopWave.Entity
{
	[Table("Status")]
	public class Status
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column(TypeName = "tinyint")]
		public byte StatusId { get; set; }

		[Required, Column(TypeName = "varchar(20)")]
		[StringLength(20)]
		public string StatusName { get; set; }


		[NotMapped]
		public ICollection<Product> Products { get; set; }
		[NotMapped]
		public ICollection<Order> Orders { get; set; }
	}
}
