using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Taller.src.data;
using Taller.src.interfaces;
using Taller.src.models;

using Microsoft.EntityFrameworkCore;

namespace Taller.src.repositories;

public class ProductRepository(StoreContext store, ILogger<Product> logger) : IProductRepository
{
    private readonly StoreContext _context = store;
    private readonly ILogger<Product> _logger = logger;

    public async Task AddProductAsync(Product product)
    {
        await _context.Products.AddAsync(product);
    }

    public void DeleteProductAsync(Product product)
    {
        _context.Products.Remove(product);
    }

    public async Task<Product> GetProductByIdAsync(int id)
    {
        return await _context.Products.FindAsync(id) ?? throw new Exception("Product not found");
    }

    public async Task<IEnumerable<Product>> GetProductsAsync()
    {
        return await _context.Products.ToListAsync() ?? throw new Exception("Product not found");
    }

    public void UpdateProduct(Product product)
    {
        throw new NotImplementedException();
    }

    Task IProductRepository.DeleteProductAsync(Product product)
    {
        throw new NotImplementedException();
    }
}