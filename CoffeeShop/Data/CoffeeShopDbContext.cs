using System;
using Microsoft.EntityFrameworkCore;
using CoffeeShop.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore; 

namespace CoffeeShop.Data
{
	public class CoffeeShopDbContext : IdentityDbContext
	{
		public CoffeeShopDbContext(DbContextOptions<CoffeeShopDbContext> options) : base(options)
		{
		}

		// return the dbset of products
		// at runtime the entity framework will map this DbSet property with our database table name
		// so db will create a table name "Products" since we named this property "Products"
		public DbSet<Product> Products { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Name).HasColumnType("text");
                entity.Property(e => e.Detail).HasColumnType("text");
                entity.Property(e => e.ImageUrl).HasColumnType("text");
                entity.Property(e => e.Price).HasColumnType("decimal(18,2)");
                entity.Property(e => e.IsTrendingProduct).HasColumnType("tinyint(1)"); // MySQL uses tinyint(1) to represent boolean values
            });

            // Seed data for the Products table
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "Americano",
                    Detail = "The Americano coffee is a classic espresso-based drink that is simple yet satisfying. It is made by adding hot water to a shot of espresso, which dilutes the intensity and results in a rich, bold coffee with a smooth finish. This versatile drink can be enjoyed on its own or with a splash of cream, making it a popular choice for coffee lovers everywhere. Whether you need a pick-me-up in the morning or a midday boost, the Americano is a dependable choice that never fails to deliver.",
                    Price = 25,
                    IsTrendingProduct = false,
                    ImageUrl = "~/images/anna-bratiychuk-3w2aurzeesu-unsplash-815x1223.jpg"
                },
                new Product
                {
                    Id = 2,
                    Name = "Cortado",
                    Detail = "Cortado coffee is a traditional Spanish coffee beverage that has gained popularity worldwide. It is a smooth and creamy coffee that combines equal parts of espresso and warm milk, creating a perfect balance of intense coffee flavor and rich creaminess. This coffee is perfect for coffee lovers who want a bit of sweetness in their coffee without sacrificing its robust flavor. Cortado coffee is made using high-quality espresso beans, freshly brewed and combined with steamed milk to create a velvety, smooth and flavorful coffee. Whether you are a coffee aficionado or a coffee lover, Cortado coffee is the perfect coffee to start your day with or to enjoy in the afternoon. Try our Cortado coffee today and experience the unique and satisfying taste of this traditional Spanish coffee beverage.",
                    Price = 25,
                    IsTrendingProduct = true,
                    ImageUrl = "~/images/giancarlo-duarte-rthw0pwclhw-unsplash-815x543.jpg"
                },
                new Product
                {
                    Id = 3,
                    Name = "Mocha",
                    Detail = "Mocha coffee is a rich and creamy blend that combines the bold flavor of coffee with the sweetness of chocolate...",
                    Price = 22,
                    IsTrendingProduct = false,
                    ImageUrl = "~/images/jeremy-yap-jn-hagwe4yw-unsplash-815x1223.jpg"
                },
                new Product
                {
                    Id = 4,
                    Name = "Macchiato",
                    Detail = "Macchiato Coffee is a classic espresso-based beverage with a rich, creamy flavor...",
                    Price = 15,
                    IsTrendingProduct = true,
                    ImageUrl = "~/images/mohammad-amirahmadi-tk2ifgn60xo-unsplash-815x1019.jpg"
                },
                new Product
                {
                    Id = 5,
                    Name = "Flat White",
                    Detail = "Flat White Coffee is a classic espresso-based beverage that is a staple in coffee shops all over the world...",
                    Price = 18,
                    IsTrendingProduct = false,
                    ImageUrl = "~/images/nathan-dumlao-nbjho6wmrww-unsplash-815x1223.jpg"
                },
                new Product
                {
                    Id = 6,
                    Name = "Decaf",
                    Detail = "Decaf Coffee, also known as decaffeinated coffee, is a coffee beverage that has had the majority of its caffeine content removed...",
                    Price = 25,
                    IsTrendingProduct = false,
                    ImageUrl = "~/images/brent-ninaber-vlpefxuqynm-unsplash-2000x1125.jpg"
                },
                new Product
                {
                    Id = 7,
                    Name = "Irish Coffee",
                    Detail = "Irish coffee is a warm, comforting drink that combines the bold flavor of coffee with the smooth sweetness of Irish whiskey and a touch of cream...",
                    Price = 15,
                    IsTrendingProduct = true,
                    ImageUrl = "~/images/tetiana-shyshkina-4lqjr8gu-bg-unsplash-815x1222.jpg"
                },
                new Product
                {
                    Id = 8,
                    Name = "Iced Coffee",
                    Detail = "Iced coffee is a refreshing and delicious way to enjoy your coffee, perfect for hot summer days or for anyone looking for a cool pick-me-up...",
                    Price = 13,
                    IsTrendingProduct = false,
                    ImageUrl = "~/images/mbr-1920x1372.jpg"
                }
            ); 
        }

    }
}

