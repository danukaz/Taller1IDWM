using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Taller.Src.Interfaces;

namespace Taller.Src.Data;
public class UnitOfWork(StoreContext context, IProductRepository productRepository, IUserRepository userRepository, IShippingAddressRepository shippingAddressRepository)
{
    private readonly StoreContext _context = context;
    public IUserRepository UserRepository { get; set; } = userRepository;
    public IProductRepository ProductRepository { get; set; } = productRepository;
    public IShippingAddressRepository ShippingAddressRepository { get; set; } = shippingAddressRepository;


    public async Task<int> SaveChangeAsync()
    {
        return await _context.SaveChangesAsync();
    }
}