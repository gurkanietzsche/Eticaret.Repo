using Eticaret.Prj.Database;
using Eticaret.Prj.Entities;
using Eticaret.Prj.Models;
using Eticaret.Prj.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text;


namespace Eticaret.Prj.Controllers
{
    public class AccountController : Controller
    {
        //private readonly DatabaseContext _context;
        //private readonly IConfiguration _config;

        //public AccountController(DatabaseContext context, IConfiguration config)
        //{
        //_context = context;
        //  _config = config;
        //}
        private readonly GenericRepository<AppUser> _service;
        private readonly IConfiguration _config;

        public AccountController(GenericRepository<AppUser> service,IConfiguration config)
        {
            _service = service;
            _config = config;
        }

        [Authorize]
        // MD5 Hash Fonksiyonu
        private string MD5Hash(string pass)
        {
            // Salt değerini AppSettings'ten alıyoruz
            var salt = _config.GetValue<string>("AppSettings:MD5Salt");

            // Şifreyi salt ile birleştiriyoruz
            var password = pass + salt;

            using (var md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Hash'i hex string'e dönüştür
                StringBuilder sb = new StringBuilder();
                foreach (var b in hashBytes)
                {
                    sb.Append(b.ToString("x2"));
                }
                return sb.ToString();
            }
        }


        public async Task<IActionResult> Index()
        {
            AppUser user = await _service.GetAsync(x => x.UserGuid.ToString() ==
            HttpContext.User.FindFirst("UserGuid").Value);
            if (user is null)
            {
                return NotFound();
            }
            var model = new UserEditViewModel
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                Phone = user.Phone,
                Password = user.Password
            };
            return View(model);
        }

        [HttpPost, Authorize]
        public async Task<IActionResult> IndexAsync(UserEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    AppUser user = await _service.GetAsync(x => x.UserGuid.ToString() ==
                    HttpContext.User.FindFirst("UserGuid").Value);
                    if (user is not null)
                    {
                        user.Name = model.Name;
                        user.Surname = model.Surname;
                        user.Email = model.Email;
                        user.Phone = model.Phone;
                        user.Password = model.Password;
                        _service.Update(user);
                        var sonuc = _service.SaveChanges();
                        if (sonuc > 0)
                        {
                            TempData["Message"] = @"<div class=""alert alert-success alert-dismissible fade show"" role=""alert"">
                         <strong>Bilgileriniz Güncellenmiştir!</strong>
                         <button type=""button"" class=""btn-close"" data-bs-dismiss=""alert"" aria-label=""Close""></button>
                         </div>";
                            //await MailHelper.SendMailAsync(contact);
                            return RedirectToAction("Index");
                        }
                    }
                    

                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Hata OLuştu!");
                }
            }
            return View(model);
        }
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignInAsync(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Kullanıcıdan gelen şifreyi hash'le
                    var hashedPassword = MD5Hash(loginViewModel.Password);

                    // Veritabanında kullanıcıyı ara
                    var account = await _service.GetAsync(x =>
                        x.Email == loginViewModel.Email &&
                        x.Password == hashedPassword &&
                        x.IsActive == true);

                    if (account == null)
                    {
                        ModelState.AddModelError("", "Giriş Başarısız!");
                    }
                    else
                    {
                        var claims = new List<Claim>
                {
                    new(ClaimTypes.Name, account.Name),
                    new(ClaimTypes.Role, account.IsAdmin ? "Admin" : "User"),
                    new(ClaimTypes.Email, account.Email),
                    new("UserId", account.Id.ToString()),
                    new("UserGuid", account.UserGuid.ToString()),
                };
                        var userIdentity = new ClaimsIdentity(claims, "Login");
                        ClaimsPrincipal userPrincipal = new ClaimsPrincipal(userIdentity);
                        await HttpContext.SignInAsync(userPrincipal);
                        return Redirect(string.IsNullOrEmpty(loginViewModel.ReturnUrl) ? "/" : loginViewModel.ReturnUrl);
                    }
                }
                catch (Exception hata)
                {
                    ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı.");
                }
            }
            return View(loginViewModel);
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUpAsync(AppUser appUser)
        {
            appUser.IsAdmin = false;
            appUser.IsActive = true;

            if (ModelState.IsValid)
            {
                // Şifreyi hash'le
                if (!string.IsNullOrEmpty(appUser.Password))
                {
                    appUser.Password = MD5Hash(appUser.Password);
                }

                await _service.AddAsync(appUser);
                await _service.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(appUser);
        }

        public async Task<IActionResult> SignOutAsync()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("SignIn");
        }

    }
}
