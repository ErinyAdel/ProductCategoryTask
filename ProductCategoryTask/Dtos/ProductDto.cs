namespace ProductCategoryTask.Dtos
{
    public class ProductDto
    {
        [MaxLength(100)]
        public string Name { get; set; }

        public int CategoryId { get; set; }
    }
}
