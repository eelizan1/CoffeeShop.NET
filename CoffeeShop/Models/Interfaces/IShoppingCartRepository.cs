using System;
namespace CoffeeShop.Models.Interfaces
{
	public interface IShoppingCartRepository
	{
        public List<ShoppingCartItem>? ShoppingCartItems { get; set; }

        void AddToCart(Product product);
		int RemoveFromCart(Product product);
		List<ShoppingCartItem> GetShoppingCartItems();
		void ClearShoppingCart();
		decimal GetShoppingCartTotal();
	}
}

