using LeNgocHaiMVC.Models;
using LeNgocHaiMVC.Repositories;

namespace LeNgocHaiMVC.Services
{
    public class CategoryService 
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _categoryRepository.GetAllCategories();
        }

        public Category GetCategoryById(short categoryId)
        {
            return _categoryRepository.GetCategoryById(categoryId);
        }

        public void AddCategory(Category category)
        {
            _categoryRepository.AddCategory(category);
        }

        public void UpdateCategory(Category category)
        {
            _categoryRepository.UpdateCategory(category);
        }

        public void DeleteCategory(short categoryId)
        {
            if (!CanDeleteCategory(categoryId))
            {
                throw new InvalidOperationException("Không thể xóa danh mục vì nó đã được sử dụng.");
            }

            _categoryRepository.DeleteCategory(categoryId);
        }
        public bool CanDeleteCategory(short categoryId)
        {
            return _categoryRepository.CanDeleteCategory(categoryId);
        }
    }
}
