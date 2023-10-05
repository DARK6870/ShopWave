using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShopWave.Entity;

namespace ShopWave.Context
{
	public class AppDBContext : IdentityDbContext
	{
		private readonly DbContextOptions _options;

		public AppDBContext(DbContextOptions options) : base(options)
		{
			_options = options;
		}

		public DbSet<Support> Supports { get; set; }
		public DbSet<AppUser> AppUsers { get; set; }
		public DbSet<Categories> Categories { get; set; }
		public DbSet<Image> Images { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<ProductImage> ProductImages { get; set; }
		public DbSet<Review> Reviews { get; set; }
		public DbSet<Status> Statuses { get; set; }
		public DbSet<Avatar> Avatars { get; set; }
		public DbSet<Variation> Variations { get; set; }
		public DbSet<ProductVariation> ProductVariations { get; set; }
		public DbSet<ProductCategory> ProductCategories { get; set; }
		public DbSet<UserData> UserDatas { get; set; }
		public DbSet<Countryes> Countryes { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<Card> Cards { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<ProductImage>().HasNoKey();
			modelBuilder.Entity<UserData>().HasNoKey();
			modelBuilder.Entity<ProductVariation>().HasNoKey();
			modelBuilder.Entity<ProductCategory>().HasNoKey();


			modelBuilder.Entity<Review>()
		.HasOne(r => r.Products)
		.WithMany() // Assuming there is a navigation property in the Product class pointing back to reviews
		.HasForeignKey(r => r.ProductId)
		.OnDelete(DeleteBehavior.Restrict);


			modelBuilder.Entity<Card>()
		.HasOne(c => c.Products)
		.WithMany() // Assuming there is a navigation property in the Product class pointing back to cards
		.HasForeignKey(c => c.ProductId)
		.OnDelete(DeleteBehavior.Restrict);

		  modelBuilder.Entity<Card>().HasNoKey();

		   modelBuilder.Entity<Order>()
		.HasOne(o => o.Products)
		.WithMany() // Assuming there is a navigation property in the Product class pointing back to orders
		.HasForeignKey(o => o.ProductId)
		.OnDelete(DeleteBehavior.Restrict); // Use DeleteBehavior.Restrict to prevent cascading delete

			modelBuilder.Entity<Order>()
				.HasOne(o => o.AppUsers)
				.WithMany() // Assuming there is a navigation property in the AppUser class pointing back to orders
				.HasForeignKey(o => o.AppUserId)
				.OnDelete(DeleteBehavior.Restrict); // Use DeleteBehavior.Restrict to prevent cascading delete

			modelBuilder.Entity<Order>()
				.HasOne(o => o.Variations)
				.WithMany() // Assuming there is a navigation property in the Variation class pointing back to orders
				.HasForeignKey(o => o.VartiationId)
				.OnDelete(DeleteBehavior.Restrict); // Use DeleteBehavior.Restrict to prevent cascading delete

			modelBuilder.Entity<Order>()
				.HasOne(o => o.Statuses)
				.WithMany() // Assuming there is a navigation property in the Status class pointing back to orders
				.HasForeignKey(o => o.StatusId)
				.OnDelete(DeleteBehavior.Restrict);

			base.OnModelCreating(modelBuilder);
		}
	}
}
