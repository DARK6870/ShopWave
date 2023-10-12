using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopWave.Entity
{
	[Table("ProductImage")]
	public class ProductImages
	{
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        [Required, ForeignKey(nameof(Products))]
		public int ProductId { get; set; }
		public virtual Product Products { get; set; }


        [MaxLength]
        public string Data { get; set; }
    }
}
