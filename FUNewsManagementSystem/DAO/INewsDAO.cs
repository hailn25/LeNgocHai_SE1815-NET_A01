using LeNgocHaiMVC.Models;

namespace LeNgocHaiMVC.DAO
{
    public interface INewsDAO
    {
        IEnumerable<NewsArticle> GetAllNewsArticles();
        IEnumerable<NewsArticle> GetActiveNewsArticles();
        NewsArticle GetNewsArticleByID(string id);
        void AddNewsArticle(NewsArticle newsArticle);
        void UpdatingNewsArticle(NewsArticle newsArticle);
        void DeleteNewsArticle(string id);
        NewsArticle GetLastNewsArticle();
		Task<List<NewsArticle>> GetReportByDateRange(DateTime startDate, DateTime endDate);


	}
}
