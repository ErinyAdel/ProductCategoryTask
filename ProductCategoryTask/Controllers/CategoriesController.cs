using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductCategoryTask.Services;

namespace ProductCategoryTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesService _categoriesService;

        public CategoriesController(ICategoriesService categoriesService)
        {
            _categoriesService = categoriesService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var categories = await _categoriesService.GetAllCategories();
                if (categories == null)
                    return NotFound("No categories was found!");
                return Ok(categories);
            }
            catch { return NotFound("No categories was found!"); }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CategoryDto dto)
        {
            try
            {
                var category = new Category { Name = dto.Name };
                await _categoriesService.AddCategory(category);

                return Ok(category);
            }
            catch { return NotFound("Error while adding new category!"); }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody]CategoryDto dto)
        {
            try
            {
                var category = await _categoriesService.GetCategoryById(id);

                if(category == null) { return NotFound($"No category was found with ID: {id}"); }

                category.Name = dto.Name;
                _categoriesService.UpdateCategory(category);

                return Ok(category);
            }
            catch { return NotFound("Error while updating this category!"); }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                var category = await _categoriesService.GetCategoryById(id);

                if (category == null) { return NotFound($"No category was found with ID: {id}"); }

                _categoriesService.DeleteCategory(category);

                return Ok(category);
            }
            catch { return NotFound("Error while removing this category!"); }
        }
    }
}
