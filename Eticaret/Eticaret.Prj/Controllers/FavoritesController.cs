using Microsoft.AspNetCore.Mvc;
using Eticaret.Prj.Entities;
using Eticaret.Prj.ExtensionMethods;
using Eticaret.Prj.Database;
using Eticaret.Prj.Repositories;

namespace Eticaret.Prj.Controllers
{
    public class FavoritesController : Controller
    {
        private readonly GenericRepository<Product> _service;

        public FavoritesController(GenericRepository<Product> service)
        {
            _service = service;
        }
        public IActionResult Index()
        {
            var favoriler = GetFavorites();
            return View(favoriler);
        }
        private List<Product> GetFavorites()
        {
            return HttpContext.Session.GetJson<List<Product>>("GetFavorites") ?? new List<Product>();
        }
        public IActionResult Add(int ProductId)
        {
            var favoriler = GetFavorites();
            var product = _service.Find(ProductId);
            if (product != null && !favoriler.Any(p=>p.Id == ProductId)) 
            {
                favoriler.Add(product);
                HttpContext.Session.SetJson("GetFavorites", favoriler);
            }
            return RedirectToAction("Index");
        }
        public IActionResult Remove(int ProductId)
        {
            var favoriler = GetFavorites();
            var product = _service.Find(ProductId);
            if (product != null && favoriler.Any(p=>p.Id == ProductId)) 
            {
                favoriler.RemoveAll(i => i.Id == ProductId);
                HttpContext.Session.SetJson("GetFavorites", favoriler);
            }
            return RedirectToAction("Index");
        }
    }
}
