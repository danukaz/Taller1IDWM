using System.ComponentModel.DataAnnotations;

namespace Taller.Src.Dtos
{
    public class UpdateProductDto
    {
        [StringLength(100, MinimumLength = 3)]
        public string? Name { get; set; }
        [StringLength(500)]
        public string? Description { get; set; }
        [Range(0.01, double.MaxValue)]
        public decimal? Price { get; set; }
        public string? Category { get; set; }
        public string? Brand { get; set; }
        [Range(0, int.MaxValue)]
        public int? Stock { get; set; }
        public IFormFile? Images { get; set; }
        public List<string>? ImagesToDelete { get; set; }
    }
}