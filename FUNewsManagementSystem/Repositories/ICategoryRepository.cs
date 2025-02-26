using LeNgocHaiMVC.Models;

namespace LeNgocHaiMVC.Repositories
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetAllCategories();
        Category GetCategoryById(short categoryID);
        void AddCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(short categoryID);
        bool CanDeleteCategory(short categoryID);
    }
}
