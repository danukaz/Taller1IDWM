using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Taller.Src.Models;

namespace Taller.Src.Interfaces
{
    public interface IShippingAddressRepository
    {
        Task<ShippingAddress?> GetByUserIdAsync(string userId);
        Task AddAsync(ShippingAddress address);
    }
}