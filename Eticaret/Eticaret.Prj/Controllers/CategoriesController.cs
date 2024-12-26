using Eticaret.Prj.Concrete;
using Eticaret.Prj.Database;
using Eticaret.Prj.Entities;
using Eticaret.Prj.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Eticaret.Prj.Controllers
{
    
    public class CategoriesController : Controller
    {
        private readonly GenericRepository<Category> _service;
        public CategoriesController(GenericRepository<Category> service)
        {
            _service = service;
        }
        public async Task<IActionResult> IndexAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _service.GetQueryable().Include(p => p.Products)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }
    }
}
