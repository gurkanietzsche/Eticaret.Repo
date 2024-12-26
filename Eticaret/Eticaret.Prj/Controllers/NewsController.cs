using Eticaret.Prj.Database;
using Eticaret.Prj.Entities;
using Eticaret.Prj.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Eticaret.Prj.Controllers
{
    public class NewsController : Controller
    {
        private readonly GenericRepository<News> _service;

        public NewsController(GenericRepository<News> service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAllAsync());

        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = await _service
                .GetAsync(m => m.Id == id);
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }
    }
}
