using Microsoft.AspNetCore.Mvc;
using Eticaret.Prj.Entities;
using Eticaret.Prj.ExtensionMethods;
using Eticaret.Prj.Database;
using Eticaret.Prj.Repositories;
using Microsoft.AspNetCore.SignalR;

namespace Eticaret.Prj.Controllers
{
    public class FavoritesController : Controller
    {
        private readonly GenericRepository<Product> _service;
        private readonly IHubContext<FavoritesHub> _hubContext;

        public FavoritesController(GenericRepository<Product> service, IHubContext<FavoritesHub> hubContext)
        {
            _service = service;
            _hubContext = hubContext;
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
                // Favorilerdeki toplam ürün sayısını alıyoruz
                var favoriteCount = favoriler.Count;

                // SignalR üzerinden tüm istemcilere favori sayısını güncelleyen mesaj gönderiyoruz
                _hubContext.Clients.All.SendAsync("UpdateFavoriteCount", favoriteCount);
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
