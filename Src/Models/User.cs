using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;

namespace Taller.Src.Models
{
    public class User : IdentityUser
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Telephone { get; set; }
        public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;
        public DateTime? LastAccess { get; set; }
        public bool IsActive { get; set; } = true;
        public string? DeactivationReason { get; set; }
        public DateOnly? BirthDate { get; set; }


        // Navigation properties
        public ShippingAddress? ShippingAddress { get; set; }
    }
}