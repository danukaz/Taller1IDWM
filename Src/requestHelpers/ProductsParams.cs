using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Taller.Src.Models;

namespace Taller.Src.Requesthelpers
{
    public class ProductParams : PaginationParams
    {
        public string? OrderBy { get; set; } = "name";
        public string? Search { get; set; }
        public string? Brands { get; set; }
        public string? Categories { get; set; }
        public ProductCondition? Condition { get; set; }
    }
}