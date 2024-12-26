using Eticaret.Prj.Database;
using Eticaret.Prj.Entities;
using Eticaret.Prj.Models;
using Eticaret.Prj.Models.Entities;
using Eticaret.Prj.Repositories;
using Eticaret.Prj.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Eticaret.Prj.Controllers
{
    public class HomeController : Controller
    {
        private readonly GenericRepository<Product> _serviceProduct;
        private readonly GenericRepository<Slider> _serviceSlider;
        private readonly GenericRepository<News> _serviceNews;
        private readonly GenericRepository<Contact> _serviceContact;

        public HomeController(GenericRepository<Product> serviceProduct, GenericRepository<Slider> serviceSlider, GenericRepository<News> serviceNews, GenericRepository<Contact> serviceContact)
        {
            _serviceProduct = serviceProduct;
            _serviceSlider = serviceSlider;
            _serviceNews = serviceNews;
            _serviceContact = serviceContact;
        }

        public async Task<IActionResult> Index()
        {
            var model = new HomePageViewModel()
            {
                Sliders = await _serviceSlider.GetAllAsync(),
                Products = await _serviceProduct.GetAllAsync(p=> p.IsActive && p.IsHome),
                News = await _serviceNews.GetAllAsync()
            };
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [Route("AccessDenied")]
        public IActionResult AccessDenied()
        {
            return View();
        }
        public IActionResult ContactUs()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ContactUsAsync(Contact contact)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _serviceContact.AddAsync(contact);
                    var sonuc = await _serviceContact.SaveChangesAsync();
                    if (sonuc > 0)
                    {
                        TempData["Message"] = @"<div class=""alert alert-success alert-dismissible fade show"" role=""alert"">
                         <strong>Mesajınız Gönderilmiştir!</strong>
                         <button type=""button"" class=""btn-close"" data-bs-dismiss=""alert"" aria-label=""Close""></button>
                         </div>";
                        //await MailHelper.SendMailAsync(contact);
                        return RedirectToAction("ContactUs");
                    }
                }
                catch (Exception)
                {

                    ModelState.AddModelError("", "Bir hata oluştu.");
                }
            }
            return View(contact);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
