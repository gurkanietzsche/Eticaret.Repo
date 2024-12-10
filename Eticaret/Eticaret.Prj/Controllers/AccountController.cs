using Eticaret.Prj.Database;
using Eticaret.Prj.Entities;
using Eticaret.Prj.Models;
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
        private readonly DatabaseContext _context;
        private readonly IConfiguration _config;

        public AccountController(DatabaseContext context, IConfiguration config)
        {
            _context = context;
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
 

        public IActionResult Index()
        {
            return View();
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
                    var account = await _context.AppUsers.FirstOrDefaultAsync(x =>
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

                await _context.AddAsync(appUser);
                await _context.SaveChangesAsync();
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
