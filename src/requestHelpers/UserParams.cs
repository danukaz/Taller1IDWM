using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taller.src.requesthelpers
{
    public class UserParams : PaginationParams
    {
        public bool? IsActive { get; set; }
        public DateTime? RegisteredFrom { get; set; }
        public DateTime? RegisteredTo { get; set; }
        public string? SearchTerm { get; set; }
    }
}