using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeShop.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoffeeShop.Controllers
{
    public class ProductsController : Controller
    {
        private IProductRepository productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            this.productRepository = productRepository; 
        }

        public IActionResult Shop()
        {
            // pass data to view 
            return View(productRepository.GetAllProducts()); 
        }

        public IActionResult Detail(int id)
        {
            var product = productRepository.GetProductById(id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product); 
        }
    }
}

