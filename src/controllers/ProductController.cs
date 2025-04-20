using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Taller.Src.Data;
using Taller.Src.Models;

namespace Taller.Src.Controllers
{
    [Route("[controller]")]
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly StoreContext _context;
        public ProductController(ILogger<ProductController> logger, StoreContext context)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<List<Product>> GetAll()
        {
            var products = _context.Products.ToList();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public ActionResult<Product> GetById(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();
            return Ok(product);
        }
    }
}