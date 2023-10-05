using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopWave.Entity
{
	[Table("Countryes")]
	public class Countryes
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column(TypeName = "tinyint")]
		public byte CountryId { get; set; }



		[Required, Column(TypeName = "varchar(30)")]
		[StringLength(30)]
		public string CountryName { get; set; }
		
		
		[NotMapped]		
		public virtual UserData UserDatas { get; set; }
		
	}
}
