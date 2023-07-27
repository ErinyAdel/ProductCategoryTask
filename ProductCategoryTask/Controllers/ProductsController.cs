using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductCategoryTask.Dtos;
using ProductCategoryTask.Models;
using ProductCategoryTask.Services;

namespace ProductCategoryTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _productsService;
        private readonly ICategoriesService _categoriesService;

        public ProductsController(IProductsService productsService, ICategoriesService categoriesService)
        {
            _productsService = productsService;
            _categoriesService = categoriesService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var products = await _productsService.GetAllProducts();
                if (products == null)
                    return NotFound("No products found!");
                return Ok(products);
            }
            catch { return NotFound("No products found!"); }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                var product = await _productsService.GetProductById(id);
                if (product == null)
                    return NotFound($"No product found with {id} ID!");
                return Ok(product);
            }
            catch { return NotFound($"No product found with {id} ID!"); }
        }

        [HttpGet("name")]
        public async Task<IActionResult> GetByNameAsync(string name)
        {
            try
            {
                var product = await _productsService.GetProductByName(name);
                if (product == null)
                    return NotFound("No product found with this name!");
                return Ok(product);
            }
            catch { return NotFound("No product found with this name!"); }
        }

        [HttpGet("contains")]
        public async Task<IActionResult> GetAllByNameAsync(string name)
        {
            try
            {
                var products = await _productsService.GetProductsByName(name);
                if (products == null)
                    return NotFound("No products found with this name!");
                return Ok(products);
            }
            catch { return NotFound("No product found with this name!"); }
        }

        [HttpGet]
        [Route("categoryId/{id}")]
        public async Task<IActionResult> GetByCategoryIdAsync(int id)
        {
            try
            {
                var products = await _productsService.GetProductsByCategoryId(id);
                if (products == null)
                    return NotFound($"No products found with {id} category ID!");
                return Ok(products);
            }
            catch { return NotFound($"No product found with {id} category ID!"); }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(ProductDto dto)
        {
            try
            {
                if (_categoriesService.GetCategoryById(dto.CategoryId).Result == null)
                    return NotFound($"No category with {dto.CategoryId} ID");

                var product = new Product { 
                    Name = dto.Name,
                    CategoryId = dto.CategoryId
                };

                await _productsService.AddProduct(product);

                return Ok(product);
            }
            catch { return NotFound("Error while adding new product!"); }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody]ProductDto dto)
        {
            try
            {
                var product = await _productsService.GetProductById(id);

                if (product == null) { return NotFound($"No product was found with {id} ID"); }

                product.Name = dto.Name;
                product.CategoryId = dto.CategoryId;
                _productsService.UpdateProduct(product);

                return Ok(product);
            }
            catch { return NotFound("Error while updating this product!"); }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                var product = await _productsService.GetProductById(id);

                if (product == null) { return NotFound($"No product was found with {id} ID"); }

                _productsService.DeleteProduct(product);

                return Ok(product);
            }
            catch { return NotFound("Error while removing this product!"); }
        }
    }
}
