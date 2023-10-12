using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ShopWave.Entity
{
    [Table("ProductVariation")]
    public class ProductVariation
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VariationId { get; set; }

        [Required, ForeignKey(nameof(Products))]
        public int ProductId { get; set; }
        public virtual Product Products { get; set; }


        [Required, Column(TypeName = "varchar(10)")]
        [StringLength(10)]
        public string VariationName { get; set; }


        [Required]
        public int quantity { get; set; }


        [Required, Column(TypeName = "Decimal(6,2)")]
        public decimal price { get; set; }


        public virtual ICollection<Order> Orders { get; set; }
    }
}
