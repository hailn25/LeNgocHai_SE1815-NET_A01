using LeNgocHaiMVC.Models;
using LeNgocHaiMVC.Services;
using Microsoft.EntityFrameworkCore;

namespace LeNgocHaiMVC.DAO
{
    public class NewsDAO : INewsDAO
    {
        private readonly FunewsManagementContext _context;
        private readonly EmailService _emailService;

        public NewsDAO(FunewsManagementContext context)
        {
            _context = context;
        }

        public IEnumerable<NewsArticle> GetAllNewsArticles()
        {
            return _context.NewsArticles
                .Include(n => n.Category)
                .Include(n => n.CreatedBy)
                .ToList();
        }

        public IEnumerable<NewsArticle> GetActiveNewsArticles()
        {
            return _context.NewsArticles
                .Where(n => n.NewsStatus == true)
                .Include(n => n.Category)
                .Include(n => n.CreatedBy)
                .ToList();
        }


        public NewsArticle GetNewsArticleByID(string id)
        {
            return _context.NewsArticles.Find(id);
        }

        public void AddNewsArticle(NewsArticle newsArticle)
        {
            _context.NewsArticles.Add(newsArticle);
            _context.SaveChanges();
           
        }
        public NewsArticle GetLastNewsArticle()
        {
            var newsArticles = _context.NewsArticles.ToList(); 

            return newsArticles
                .OrderByDescending(n => int.TryParse(n.NewsArticleId, out int id) ? id : 0)
                .FirstOrDefault();
        }

        public void UpdatingNewsArticle(NewsArticle newsArticle)
        {
            _context.NewsArticles.Update(newsArticle);
            _context.SaveChanges();
        }



        public void DeleteNewsArticle(string id)
        {
            var news = _context.NewsArticles.Find(id);
            if (news != null)
            {
                _context.NewsArticles.Remove(news);
                _context.SaveChanges();
            }
        }
		public async Task<List<NewsArticle>> GetReportByDateRange(DateTime startDate, DateTime endDate)
		{
			return await _context.NewsArticles
				.Where(n => n.CreatedDate >= startDate && n.CreatedDate <= endDate)
				.OrderByDescending(n => n.CreatedDate)
				.ToListAsync();
		}



	}
}
