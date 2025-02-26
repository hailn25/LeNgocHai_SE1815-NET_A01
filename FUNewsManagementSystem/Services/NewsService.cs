using LeNgocHaiMVC.Models;
using LeNgocHaiMVC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LeNgocHaiMVC.Services
{
    public class NewsService
    {
        private readonly INewsRepository _newsRepository;

        public NewsService (INewsRepository newsRepository)
        {
            _newsRepository = newsRepository;
        }

        public IEnumerable<NewsArticle> GetAllNewsArticle()
        {
            return _newsRepository.GetAllNewsArticles();
        }

        public IEnumerable<NewsArticle> GetActiveNews()
        {
            return _newsRepository.GetActiveNewsArticles();
        }

        public NewsArticle GetNewsArticleByID(string id)
        {
            return _newsRepository.GetNewsArticleByID(id);
        }

        public void AddNewsArticle(NewsArticle article)
        {
            if (article == null) throw new ArgumentNullException(nameof(article));


            var lastNews =  _newsRepository.GetLastNewsArticle();

            int lastId = (lastNews != null && int.TryParse(lastNews.NewsArticleId, out int parsedId)) ? parsedId : 0;

            int newId = lastId + 1;
            article.NewsArticleId = newId.ToString();
            try
            {
                _newsRepository.AddNewsArticle(article);

                Console.WriteLine($"News '{article.NewsTitle}' added to database successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to add news to database: {ex.Message}");
            }
        }

        public void UpdateNewsArticle(NewsArticle article)
        {
            _newsRepository.UpdateNewsArticle(article);
        }

        public void DeleteNewsArticle(string id)
        {
            _newsRepository.DeleteNewsArticle(id);
        }
        public IEnumerable<NewsArticle> SearchNews(string query)
        {
            return _newsRepository.GetAllNewsArticles()
                .Where(n => n.NewsTitle.Contains(query, StringComparison.OrdinalIgnoreCase)
                         || n.NewsContent.Contains(query, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
		public async Task<List<NewsArticle>> GetReportByDateRange(DateTime startDate, DateTime endDate)
		{
			return await _newsRepository.GetReportByDateRange(startDate, endDate);
		}


	}
}
