using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using Taller.Src.Data;
using Taller.Src.Interfaces;
using Taller.Src.Models;

namespace Taller.Src.Repositories;

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

    public IQueryable<Product> GetQueryableProducts()
    {
        throw new NotImplementedException();
    }

    public async Task UpdateProductAsync(Product product)
    {
        var existingProduct = await _context.Products.FindAsync(product.Id) ?? throw new Exception("Product not found");
        existingProduct.Name = product.Name;
        existingProduct.Description = product.Description;
        existingProduct.Price = product.Price;
        existingProduct.Stock = product.Stock;
        existingProduct.Urls = product.Urls;
        existingProduct.Brand = product.Brand;
        _context.Products.Update(existingProduct);
    }

    Task IProductRepository.DeleteProductAsync(Product product)
    {
        throw new NotImplementedException();
    }


}