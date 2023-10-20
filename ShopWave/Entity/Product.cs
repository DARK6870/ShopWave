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
		[StringLength(60)]
		public string ProductName { get; set; }


		[Required, Column(TypeName = "varchar(800)")]
		[StringLength(800)]
		public string Description { get; set; }



		[Column(TypeName = "Decimal(3,2)")]
		public decimal AvgStars { get; set; }


		[Required, DefaultValue(0)]
		public int OrderCounts { get; set; }


        [Required]
        public bool Admitered { get; set; }

        public DateTime ProductDate { get; set; }


        [Required, ForeignKey(nameof(Categoriess))]
		public short CategoryId { get; set; }
		public virtual Categories Categoriess { get; set; }



        public ICollection<ProductImages> ProductImages { get; set; }
		public ICollection<Review> Reviews { get; set; }
        public ICollection<ProductVariation> ProductVariations { get; set; }
    }
}
