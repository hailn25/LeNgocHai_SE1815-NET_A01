using LeNgocHaiMVC.DAO;
using LeNgocHaiMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace LeNgocHaiMVC.Repositories
{
    public class NewsRepository : INewsRepository
    {
        private readonly INewsDAO _newsDAO;

        public NewsRepository(INewsDAO dao)
        {
            _newsDAO = dao;   
        }

        public IEnumerable<NewsArticle> GetAllNewsArticles()
        {
            return _newsDAO.GetAllNewsArticles();
        }
        public IEnumerable<NewsArticle> GetActiveNewsArticles()
        {
            return _newsDAO.GetActiveNewsArticles();
        }

        public NewsArticle GetNewsArticleByID(string id)
        {
            return _newsDAO.GetNewsArticleByID(id);
        }

        public void AddNewsArticle(NewsArticle article)
        {
            _newsDAO.AddNewsArticle(article);
        }
        public void UpdateNewsArticle(NewsArticle article)
        {
            _newsDAO.UpdatingNewsArticle(article);
        }
        public void DeleteNewsArticle(string id)
        {
            _newsDAO.DeleteNewsArticle(id);
        }

        public NewsArticle GetLastNewsArticle()
        {
            return  _newsDAO.GetLastNewsArticle();
        }
		public async Task<List<NewsArticle>> GetReportByDateRange(DateTime startDate, DateTime endDate)
		{
			return await _newsDAO.GetReportByDateRange(startDate, endDate);
		}


	}
}
