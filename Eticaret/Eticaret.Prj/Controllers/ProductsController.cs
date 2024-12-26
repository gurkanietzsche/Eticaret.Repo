using Eticaret.Prj.Database;
using Eticaret.Prj.Entities;
using Eticaret.Prj.Models;
using Eticaret.Prj.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Eticaret.Prj.Controllers
{
    public class ProductsController : Controller
    {
        private readonly GenericRepository<Product> _serviceProduct;

        public ProductsController(GenericRepository<Product> serviceProduct)
        {
            _serviceProduct = serviceProduct;
        }

        public async Task<IActionResult> Index(string q = "")
        {
            var databaseContext = _serviceProduct.GetAllAsync(p => p.IsActive && p.Name.Contains(q) || p.Description.Contains(q));
            return View(await databaseContext);
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _serviceProduct.GetQueryable()
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            var model = new ProductDetailViewModel()
            {
                Product = product,
                RelatedProducts = _serviceProduct.GetQueryable().Where(p => p.IsActive && p.CategoryId == product.CategoryId && p.Id != product.Id)
            };
            return View(model);
        }

    }
}
