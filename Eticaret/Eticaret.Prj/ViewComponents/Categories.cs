using Eticaret.Prj.Database;
using Eticaret.Prj.Entities;
using Eticaret.Prj.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Eticaret.Prj.ViewComponents
{
    public class Categories : ViewComponent
    {
        private readonly GenericRepository<Category> _service;
        public Categories(GenericRepository<Category> service)
        {
            _service = service;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _service.GetAllAsync(C => C.IsActive && C.IsTopMenu));             
        }
    }
}
