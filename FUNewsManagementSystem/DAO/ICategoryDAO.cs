using LeNgocHaiMVC.Models;

namespace LeNgocHaiMVC.DAO
{
    public interface ICategoryDAO
    {
        IEnumerable<Category> GetAllCategories();
        Category GetCategoryById(short categoryId);
        void AddCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(short categoryId);
        bool IsCategoryUsed(short categoryId);
    }
}
