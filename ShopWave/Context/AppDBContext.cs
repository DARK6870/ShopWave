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
		//public DbSet<Review> Reviews { get; set; }
		public DbSet<Status> Statuses { get; set; }
		public DbSet<Avatar> Avatars { get; set; }
		public DbSet<UserData> UserDatas { get; set; }
		public DbSet<Variation> Variations { get; set; }
		public DbSet<ProductVariation> ProductVariations { get; set; }
		public DbSet<ProductCategory> ProductCategories { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<ProductImage>().HasNoKey();
			modelBuilder.Entity<UserData>().HasNoKey();
			modelBuilder.Entity<ProductVariation>().HasNoKey();
			modelBuilder.Entity<ProductCategory>().HasNoKey();

			base.OnModelCreating(modelBuilder);
		}
	}
}
