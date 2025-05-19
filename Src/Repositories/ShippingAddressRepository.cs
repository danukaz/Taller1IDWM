using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using Taller.Src.Data;
using Taller.Src.Interfaces;
using Taller.Src.Models;

namespace Taller.Src.Repositories
{
    public class ShippingAddressRepository(StoreContext context) : IShippingAddressRepository
    {
        private readonly StoreContext _context = context;

        public async Task<ShippingAddress?> GetByUserIdAsync(string userId)
        {
            return await _context.ShippingAddress
                .FirstOrDefaultAsync(a => a.UserId == userId);
        }

        public async Task AddAsync(ShippingAddress address)
        {
            await _context.ShippingAddress.AddAsync(address);
        }
    }

}