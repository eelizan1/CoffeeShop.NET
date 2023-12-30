using System;
using CoffeeShop.Data;
using CoffeeShop.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.Models.Repository
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        public List<ShoppingCartItem>? ShoppingCartItems { get; set; }
        private CoffeeShopDbContext dbContext;
        public string? ShoppingCartId { get; set; }

        public ShoppingCartRepository(CoffeeShopDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // main purpose is to store the cartId in the session 
        public static ShoppingCartRepository GetCart(IServiceProvider services)
        {
            // access session from the IServieProvider 
            ISession? session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext?.Session;

            // acccess to DbContext 
            CoffeeShopDbContext context = services.GetService<CoffeeShopDbContext>() ?? throw new Exception("Error initializing coffeeshopdbcontext");

            // check if there's a cartId for the incoming user in the session if not then generate a GUID 
            string cartId = session?.GetString("CartId") ?? Guid.NewGuid().ToString();

            // store value of CartId with the GUID 
            session?.SetString("CartId", cartId);

            // return the dbContext with shoppingCartId 
            return new ShoppingCartRepository(context) { ShoppingCartId = cartId };
        }

        public void AddToCart(Product product)
        {
            // check if product is already in shopping cart before adding
            var shoppingCartItem = dbContext.ShoppingCartItems.FirstOrDefault(s => s.Product.Id == product.Id && s.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem == null)
            {
                // if not there then add to cart and start it's quantity to 1 
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = ShoppingCartId,
                    Product = product,
                    Qty = 1
                };

                // add new item to cart list 
                dbContext.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                // increment qty 
                shoppingCartItem.Qty++;
            }

            dbContext.SaveChanges(); // save changes 
        }

        public void ClearShoppingCart()
        {
            var cartItems = dbContext.ShoppingCartItems.Where(s => s.ShoppingCartId == ShoppingCartId);
            dbContext.ShoppingCartItems.RemoveRange(cartItems); // delete the list of items 
            dbContext.SaveChanges();
        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ??= dbContext.ShoppingCartItems.Where(s => s.ShoppingCartId == this.ShoppingCartId)
                 .Include(p => p.Product).ToList(); // add product related to each shopping cart item 
        }

        public decimal GetShoppingCartTotal()
        {
            var totalCost = dbContext.ShoppingCartItems.Where(s => s.ShoppingCartId == ShoppingCartId)
                  .Select(s => s.Product.Price * s.Qty).Sum(); // sum all the items in the shopping cart list 
            return totalCost;
        }

        public int RemoveFromCart(Product product)
        {
            var shoppingCartItem = dbContext.ShoppingCartItems.FirstOrDefault(s => s.Product.Id == product.Id && s.ShoppingCartId == ShoppingCartId);
            var quantity = 0;

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Qty > 1)
                {
                    shoppingCartItem.Qty--; // decrement quantity 
                    quantity = shoppingCartItem.Qty;
                }
                else
                {
                    // if there's one just remove 
                    dbContext.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }

            dbContext.SaveChanges();
            return quantity;
        }
    }
}

