using ProductCategoryTask.Models;

namespace ProductCategoryTask.Services
{
    public interface ICategoriesService
    {
        Task<IEnumerable<Category>> GetAllCategories();
        Task<Category> GetCategoryById(int id);
        Task<Category> AddCategory(Category category);
        Category UpdateCategory(Category category);
        Category DeleteCategory(Category category);
    }
}
