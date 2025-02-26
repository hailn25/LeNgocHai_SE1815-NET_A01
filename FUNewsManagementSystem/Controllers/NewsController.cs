using LeNgocHaiMVC.Models;
using LeNgocHaiMVC.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LeNgocHaiMVC.Controllers
{
    public class NewsController : Controller
    {
        private readonly NewsService _newsService;
        private readonly EmailService _emailService;
        // GET: NewsController
        public NewsController(NewsService newsService, EmailService emailService) // Inject Service vào Controller
        {
            _newsService = newsService ?? throw new ArgumentNullException(nameof(newsService));
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
        }


        public ActionResult Index()
        {
            // Lấy role từ cookies (Claim trong Authentication)
            var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            IEnumerable<NewsArticle> news;

            if (userRole == "Lecturer") // Role = 2
            {
                news = _newsService.GetActiveNews(); // Lấy bài viết active
            }
            else
            {
                news = _newsService.GetAllNewsArticle(); // Lấy tất cả bài viết
            }

            return View(news);
        }
        public ActionResult Search(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return View("Index", _newsService.GetAllNewsArticle());
            }

            var result = _newsService.SearchNews(query);
            return View("Index", result); // Hiển thị kết quả trong trang Index
        }



        // GET: NewsController/Details/5
        [AllowAnonymous]
        public ActionResult Details(string id)
        {
            var news = _newsService.GetNewsArticleByID(id);
            if(news == null) return NotFound();
            return View(news);
        }

        // GET: NewsController/Create
        [Authorize(Roles = "Staff")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: NewsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Staff")]
        public ActionResult Create(NewsArticle news)
        {
            if(!ModelState.IsValid)
                return View(news);

            _newsService.AddNewsArticle(news);
            string adminEmail = "hailnhe173577@fpt.edu.vn"; // Thay bằng email thực tế của Admin
            string subject = "📰 New Article Published: " + news.NewsTitle;
            string body = $@"
            <h3>A new article has been published</h3>
            <p><b>Title:</b> {news.NewsTitle}</p>
            <p><b>Author:</b> {news.CreatedBy?.AccountName}</p>
            <p><a href='https://yourwebsite.com/news/{news.NewsArticleId}'>Click here to read</a></p>
            <br/>
            <p>Thank you!</p>
            ";

            // Gửi email (không chặn luồng chính)
            Task.Run(() => _emailService.SendEmailAsync(adminEmail, subject, body));
            return RedirectToAction(nameof(Index));

            
        }

        // GET: NewsController/Edit/5
        [Authorize(Roles = "Staff")]
        public ActionResult Edit(string id)
        {
            if (id == null) return NotFound();

            var news = _newsService.GetNewsArticleByID(id);
            if (news == null) return NotFound();

            return View(news);
        }

        // POST: NewsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Staff")]
        public ActionResult Edit(string id, NewsArticle news)
        {

            if (!id.Equals(news.NewsArticleId)) return NotFound();

            if (ModelState.IsValid)
            {
                 _newsService.UpdateNewsArticle(news);
                return RedirectToAction(nameof(Index));
            }

            return View(news);
        }

        // GET: NewsController/Delete/5
        [Authorize(Roles = "Staff")]
        public ActionResult Delete(string id)
        {
            if(id == null) return NotFound();
            var news = _newsService.GetNewsArticleByID(id);
            if (news == null) return NotFound();
            return View();
        }

        // POST: NewsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Staff")]
        public ActionResult Delete(string id, NewsArticle news)
        {
            if (!id.Equals(news.NewsArticleId)) return NotFound();

            if (ModelState.IsValid)
            {
                _newsService.DeleteNewsArticle(id);
                return RedirectToAction(nameof(Index));
            }

            return View(news);
        }
		[Authorize(Roles = "Admin")]
		public IActionResult Report()
		{
			return View();
		}

		[HttpPost]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Report(DateTime startDate, DateTime endDate)
		{
			var reportData = await _newsService.GetReportByDateRange(startDate, endDate);
			return View(reportData);
		}
	}
}
