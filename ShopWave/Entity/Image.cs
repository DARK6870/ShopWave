﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopWave.Entity
{
	[Table("Image")]
	public class Image
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ImageId { get; set; }

		[Required]
		public byte[] Data { get; set; }


		[NotMapped]
		public ICollection<ProductImage> ProductImages { get; set; }
	}
}
