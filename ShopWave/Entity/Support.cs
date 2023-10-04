using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopWave.Entity
{
	[Table("Support")]
	public class Support
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int SupportId { get; set; }


		[Required, Column(TypeName = "varchar(50)")]
		[StringLength(50)]
		public string Email { get; set; }


		[Required, Column(TypeName = "varchar(50)")]
		[StringLength(50)]
		public string FullName { get; set; }



		[Required, Column(TypeName = "varchar(600)")]
		[StringLength(600)]
		public string ProblemText { get; set; }


		[Required, Column(TypeName = "datetime")]
		public DateTime SupportDate { get; set; }
	}
}
