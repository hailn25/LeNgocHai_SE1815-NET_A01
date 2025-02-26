using LeNgocHaiMVC.Models;

namespace LeNgocHaiMVC.DAO
{
    public class CategoryDAO : ICategoryDAO
    {
        private readonly FunewsManagementContext _context;

        public CategoryDAO(FunewsManagementContext context)
        {
            _context = context;
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _context.Categories.ToList();
        }

        public Category GetCategoryById(short categoryID)
        {
            return _context.Categories.Find(categoryID);
        }

        public void AddCategory(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }

        public void UpdateCategory(Category category)
        {
            _context.Categories.Update(category);
            _context.SaveChanges();
        }

        public void DeleteCategory(short categoryID)
        {

            {
                var category = _context.Categories.Find(categoryID);
                if (category != null)
                {
                    _context.Categories.Remove(category);
                    _context.SaveChanges();
                }
            }
        }
        public bool IsCategoryUsed(short categoryId)
        {
            
                return _context.NewsArticles.Any(n => n.CategoryId == categoryId);
 
        }


    }
}
