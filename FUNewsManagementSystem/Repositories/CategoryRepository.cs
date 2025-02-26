using LeNgocHaiMVC.DAO;
using LeNgocHaiMVC.Models;

namespace LeNgocHaiMVC.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ICategoryDAO _categoryDAO;

        public CategoryRepository(ICategoryDAO categoryDAO)
        {
            _categoryDAO = categoryDAO;
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _categoryDAO.GetAllCategories();
        }

        public Category GetCategoryById(short categoryID)
        {
            return _categoryDAO.GetCategoryById(categoryID);
        }

        public void AddCategory(Category category)
        {
            _categoryDAO.AddCategory(category);
        }

        public void UpdateCategory(Category category)
        {
            _categoryDAO.UpdateCategory(category);
        }

        public void DeleteCategory(short categoryID)
        {
            if (CanDeleteCategory(categoryID))
            {
                _categoryDAO.DeleteCategory(categoryID);
            }
            else
            {
                throw new InvalidOperationException("Không thể xóa danh mục vì nó đã được sử dụng trong bài viết.");
            }
        }
        public bool CanDeleteCategory(short categoryId)
        {
            return !_categoryDAO.IsCategoryUsed(categoryId);
        }


    }
}
