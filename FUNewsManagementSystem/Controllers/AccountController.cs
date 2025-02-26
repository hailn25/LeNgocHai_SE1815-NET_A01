using LeNgocHaiMVC.Models;
using LeNgocHaiMVC.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static LeNgocHaiMVC.Services.AccountService;
using Microsoft.AspNetCore.Authorization;

namespace LeNgocHaiMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly AccountService _accountService;

        public AccountController(AccountService accountService)
        {
            _accountService = accountService;
        }
        // GET: AccountController
        public IActionResult Index()
        {
            var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            // Kiểm tra nếu không phải Staff thì chuyển hướng đến trang thông báo
            if (userRole != "Admin")
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            // Nếu đúng Staff, thì lấy danh sách tài khoản
            IEnumerable<SystemAccount> accounts = _accountService.GetAllAccounts();
            return View(accounts);
        }
        public ActionResult Login()
        {
            return View("~/Views/Login/Login.cshtml");
        }

        // GET: AccountController/Details/5
        public ActionResult Details(short id)
        {
            var account = _accountService.GetAccountById(id);
            if (account == null)
            {
                return NotFound();
            }
            return View(account);
        }

        // GET: AccountController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AccountController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SystemAccount account)
        {
            if (!ModelState.IsValid) return View(account);

            _accountService.AddAccount(account);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = await _accountService.AuthenticateUser(email, password);

            if (user == null)
            {
                ViewBag.Error = "Invalid email or password";
                return View();
            }

            // Lưu user vào session
            HttpContext.Session.SetInt32("UserId", user.AccountId);
            HttpContext.Session.SetInt32("UserRole", Convert.ToInt32(user.AccountRole));

            // Xác định role
            string role = user.AccountRole switch
            {
                1 => "Staff",
                2 => "Lecturer",
                0 => "Admin",
                3 => "Customer"// Lấy role admin từ appsettings.json nếu cần
            };

            // Tạo Claims để phục vụ phân quyền
            var claims = new List<Claim>
            {
            new Claim(ClaimTypes.Name, user.AccountEmail),
            new Claim(ClaimTypes.Role, role)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties { IsPersistent = true };

            // Đăng nhập bằng Authentication
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties
            );


            return RedirectToAction("Index", "Home");
        }


        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // Xóa session
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme); // Xóa cookies

            return RedirectToAction("Login");
        }

        // GET: AccountController/Edit/5
        public ActionResult Edit(short id)
        {
            var account = _accountService.GetAccountById(id);
            if (account == null)
            {
                return NotFound();
            }
            return View(account);
        }

        // POST: AccountController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SystemAccount account)
        {
            if (!ModelState.IsValid) return View(account);

            _accountService.UpdateAccount(account);
            return RedirectToAction(nameof(Index));
        }

        // GET: AccountController/Delete/5
        public ActionResult Delete(short id)
        {
            var category = _accountService.GetAccountById(id);
            if (category == null) return NotFound();
            return View(category);
        }

        // POST: AccountController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(short id, SystemAccount account)
        {
            try
            {
                _accountService.DeleteAccount(id); // Gọi service để xóa tài khoản
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Lỗi khi xóa tài khoản: " + ex.Message);
                return View();
            }
        }

        public ActionResult EditProfile()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login");
            }

            var account = _accountService.GetAccountById((short)userId);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile(SystemAccount account)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null || userId != account.AccountId)
            {
                return RedirectToAction("Login");
            }

            if (!ModelState.IsValid) return View(account);

            _accountService.UpdateAccount(account);
            return RedirectToAction("Index", "Home");
        }



    }
}
