using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;

namespace Taller.src.models
{
    public class User : IdentityUser
    {
        public int Id { get; set; }
        public required string FirtsName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string Thelephone { get; set; }
        public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;
        public DateTime? LastAccess { get; set; } 
        public bool IsActive { get; set; } = true; 
        public string? DeactivationReason { get; set; }

        // Navigation properties
        public ShippingAddres? ShippingAddres { get; set; }
    }
}