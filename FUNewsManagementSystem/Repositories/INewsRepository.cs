using LeNgocHaiMVC.Models;

namespace LeNgocHaiMVC.Repositories
{
    public interface INewsRepository
    {
        IEnumerable<NewsArticle> GetAllNewsArticles();
        IEnumerable<NewsArticle> GetActiveNewsArticles();
       
        NewsArticle GetNewsArticleByID(string id);
        void AddNewsArticle(NewsArticle article);
        void UpdateNewsArticle(NewsArticle article);
        void DeleteNewsArticle(string id);
        NewsArticle GetLastNewsArticle();
        Task<List<NewsArticle>> GetReportByDateRange(DateTime startDate, DateTime endDate);


	}
}
