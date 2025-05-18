using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Taller.Src.Dtos
{
    public class ToggleStatusDto
    {
        public bool IsActive { get; set; }
        [StringLength(255)]
        public string? Reason { get; set; }
    }
}