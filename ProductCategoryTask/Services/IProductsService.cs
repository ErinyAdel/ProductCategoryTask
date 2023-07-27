namespace ProductCategoryTask.Services
{
    public interface IProductsService
    {
        Task<IEnumerable<Product>> GetAllProducts();
        Task<Product> GetProductById(int id);
        Task<Product> GetProductByName(string name);
        Task<IEnumerable<Product>> GetProductsByName(string name);
        Task<IEnumerable<Product>> GetProductsByCategoryId(int catId);
        Task<Product> AddProduct(Product product);
        Product UpdateProduct(Product product);
        Product DeleteProduct(Product product);
    }
}
