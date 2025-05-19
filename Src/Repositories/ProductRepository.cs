using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using Taller.Src.Data;
using Taller.Src.Dtos;
using Taller.Src.Interfaces;
using Taller.Src.Models;
using Taller.Src.Services;

namespace Taller.Src.Repositories;

public class ProductRepository(StoreContext store, ILogger<Product> logger) : IProductRepository
{
    private readonly StoreContext _context = store;
    private readonly ILogger<Product> _logger = logger;

    public async Task AddProductAsync(Product product)
    {
        await _context.Products.AddAsync(product);
    }

    public async Task<Product> GetProductByIdAsync(int id)
    {
        return await _context.Products.FindAsync(id) ?? throw new Exception("Product not found");
    }

    public async Task<IEnumerable<Product>> GetProductsAsync()
    {
        return await _context.Products.ToListAsync() ?? throw new Exception("Product not found");
    }

    public IQueryable<Product> GetQueryableProducts()
    {
        return _context.Products.AsQueryable();
    }

    public Task UpdateProductAsync(Product product)
    {
        _context.Products.Update(product);
        return Task.CompletedTask;
    }


    public Task DeleteProductAsync(Product product)
    {
        _context.Products.Remove(product);
        return Task.CompletedTask;
    }
}