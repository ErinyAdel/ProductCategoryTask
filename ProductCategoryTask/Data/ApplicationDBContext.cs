using Microsoft.EntityFrameworkCore;
using ProductCategoryTask.Models;

namespace ProductCategoryTask.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
                
        }

        public DbSet<Category> Categories { get; set; }
    }
}
