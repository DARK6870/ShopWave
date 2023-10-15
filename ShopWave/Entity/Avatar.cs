using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopWave.Entity
{
	[Table("Avatar")]
	public class Avatar
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        [ForeignKey(nameof(AppUsers))]
        public string? AppUserId { get; set; }
        public virtual AppUser? AppUsers { get; set; }


        [MaxLength]
        public string? Data { get; set; }

    }
}
