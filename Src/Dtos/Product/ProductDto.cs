using System.ComponentModel.DataAnnotations;
namespace Taller.Src.Dtos
{
    public class ProductDto
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }
        [Required]
        public string Category { get; set; }
        public string Brand { get; set; }
        [Range(0, int.MaxValue)]
        public int Stock { get; set; }
        public List<string> ImageUrls { get; set; }
    }
}