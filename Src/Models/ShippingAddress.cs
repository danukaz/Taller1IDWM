using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taller.Src.Models
{
    public class ShippingAddress
    {
        public int Id { get; set; }
        public required string Street { get; set; }
        public required string Number { get; set; }
        public required string Commune { get; set; }

        public required string Region { get; set; }
        public required string PostalCode { get; set; }

        // Navigation properties
        public string UserId { get; set; } = null!;
        public User User { get; set; } = null!;
    }
}