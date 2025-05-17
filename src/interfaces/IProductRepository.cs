using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Taller.src.models;

namespace Taller.src.interfaces;

public interface IProductRepository
{
    Task<Product> GetProductByIdAsync(int id);
    Task<IEnumerable<Product>> GetProductsAsync();
    Task AddProductAsync(Product product);
    Task DeleteProductAsync(Product product);
    Task UpdateProduct(Product product);
}