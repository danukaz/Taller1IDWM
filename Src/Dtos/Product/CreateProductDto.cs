using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Taller.Src.Dtos
{
    public class CreateProductDto {
        [Required, StringLength(100, ErrorMessage = "El nombre es requerido y debe tener menos de 100 caracteres")]
        public string Name { get; set; }

        [Required, StringLength(500, ErrorMessage = "La descripción es requerida y debe tener menos de 500 caracteres")]
        public string Description { get; set; }

        [Required, Range(1, double.MaxValue, ErrorMessage = "El precio es requerido y debe ser mayor a 0")]
        public decimal Price { get; set; }

        [Required, StringLength(100, ErrorMessage = "La categoría es requerida y debe tener menos de 100 caracteres")]
        public string Category { get; set; }

        [Required, StringLength(100, ErrorMessage = "La marca es requerida y debe tener menos de 100 caracteres")]
        public string Brand { get; set; }

        [Required, Range(0, int.MaxValue, ErrorMessage = "El stock es requerido y debe ser mayor o igual a 0")]
        public int Stock { get; set; }

        [Required]
        public List<IFormFile> Images { get; set; } = []; 
    } 
}