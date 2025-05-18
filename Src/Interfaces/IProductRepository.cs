using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Taller.Src.Models;
using Taller.Src.Dtos;

namespace Taller.Src.Interfaces;

public interface IProductRepository
{
    Task<Product> GetProductByIdAsync(int id);
    Task<IEnumerable<Product>> GetProductsAsync();
    Task AddProductAsync(Product product);
    Task DeleteProductAsync(Product product);
    Task UpdateProductAsync(Product product);
    IQueryable<Product> GetQueryableProducts();
}