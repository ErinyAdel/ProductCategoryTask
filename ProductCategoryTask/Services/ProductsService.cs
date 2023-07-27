using Microsoft.EntityFrameworkCore;
using ProductCategoryTask.Models;

namespace ProductCategoryTask.Services
{
    public class ProductsService : IProductsService
    {
        private readonly ApplicationDBContext _context;

        public ProductsService(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            try { return await _context.Products.OrderBy(p => p.Name).Include(c => c.Category).ToListAsync();  }
            catch { return null; }
        }

        public async Task<Product> GetProductById(int id)
        {
            try { return await _context.Products.Include(p => p.Category).SingleOrDefaultAsync(p => p.Id == id); }
            catch { return null; }
        }

        public async Task<Product> GetProductByName(string name)
        {
            try { return await _context.Products.Include(p => p.Category).SingleOrDefaultAsync(p => p.Name == name); }
            catch { return null; }
        }

        public async Task<IEnumerable<Product>> GetProductsByName(string name)
        {
            try { return await _context.Products.Include(p => p.Category).Where(p => p.Name.Contains(name)).ToListAsync(); }
            catch { return null; }
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryId(int catId)
        {
            try { return await _context.Products.Include(p => p.Category).Where(p => p.CategoryId == catId).ToListAsync(); }
            catch { return null; }
        }

        public async Task<Product> AddProduct(Product product)
        {
            try
            {
                await _context.Products.AddAsync(product);
                _context.SaveChanges();

                return product;
            }
            catch { return null; }
        }

        public Product UpdateProduct(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();

            return product;
        }

        public Product DeleteProduct(Product product)
        {
            _context.Products.Remove(product);
            _context.SaveChanges();

            return product;
        }
    }
}
