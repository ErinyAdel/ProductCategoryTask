using Microsoft.EntityFrameworkCore;

namespace ProductCategoryTask.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly ApplicationDBContext _context;

        public CategoriesService(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            try { return await _context.Categories.OrderBy(c => c.Name).ToListAsync(); } 
            catch { return null; }
        }

        public async Task<Category> GetCategoryById(int id)
        {
            try { return await _context.Categories.SingleOrDefaultAsync(c => c.Id == id); }
            catch { return null; }
        }

        public async Task<Category> AddCategory(Category category)
        {
            try
            {
                await _context.Categories.AddAsync(category);
                _context.SaveChanges();

                return category;
            } 
            catch { return null; }
        }

        public Category UpdateCategory(Category category)
        {
            _context.Categories.Update(category);
            _context.SaveChanges();

            return category;
        }

        public Category DeleteCategory(Category category)
        {
            _context.Categories.Remove(category);
            _context.SaveChanges();

            return category;
        }
    }
}
