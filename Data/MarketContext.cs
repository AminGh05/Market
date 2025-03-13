using Market.Models;
using Microsoft.EntityFrameworkCore;

namespace Market.Data
{
	public class MarketContext : DbContext
	{
		public MarketContext(DbContextOptions<MarketContext> options) : base(options)
		{

		}

		public DbSet<Category> Categories { get; set; }
		public DbSet<CategoryToProduct> CategoryToProducts { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<Item> Items { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderDetails> OrderDetails { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<CategoryToProduct>().HasKey(c => new { c.ProductId, c.CategoryId });

			//modelBuilder.Entity<Product>(p =>
			//{
			//	p.HasKey(w => w.Id);
			//	p.OwnsOne<Item>(w => w.Item);
			//	p.HasOne<Item>(w => w.Item).WithOne(w => w.Product).HasForeignKey<Item>(w => w.Id);
			//});

			modelBuilder.Entity<Item>(i =>
			{
				i.Property(w => w.Price).HasColumnType("money");
				i.HasKey(w => w.Id);
			});

			#region Seed Data

			modelBuilder.Entity<Category>().HasData(
				new Category()
				{
					Id = 1,
					Name = "A",
					Description = "First Test Category",
				},
				new Category()
				{
					Id = 2,
					Name = "B",
					Description = "Second Test Category"
				},
				new Category()
				{
					Id = 3,
					Name = "C",
					Description = "Third Test Category"
				}
			);

			modelBuilder.Entity<Item>().HasData(
				new Item()
				{
					Id = 1,
					Price = 1M,
					QuantityInStock = 1
				},
				new Item()
				{
					Id = 2,
					Price = 2M,
					QuantityInStock = 2
				},
				new Item()
				{
					Id = 3,
					Price = 10M,
					QuantityInStock = 0
				}
			);

			modelBuilder.Entity<Product>().HasData(
				new Product()
				{
					Id = 1,
					Name = "A",
					Description = "First test product",
					ItemId = 1
				},
				new Product()
				{
					Id = 2,
					Name = "B",
					Description = "Second test product",
					ItemId = 2
				},
				new Product()
				{
					Id = 3,
					Name = "C",
					Description = "Third test product",
					ItemId = 3
				}
			);

			modelBuilder.Entity<CategoryToProduct>().HasData(
				new CategoryToProduct() { CategoryId = 1, ProductId = 1 },
				new CategoryToProduct() { CategoryId = 2, ProductId = 2 },
				new CategoryToProduct() { CategoryId = 3, ProductId = 2 }
			);

			#endregion

			base.OnModelCreating(modelBuilder);
		}
	}
}
