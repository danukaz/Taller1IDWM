using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Taller.src.models;

using   Microsoft.EntityFrameworkCore;

namespace Taller.src.data
{
    public class StoreContext(DbContextOptions options) : DbContext(options)
    {
        public  required DbSet<Product> Products { get; set; }
    }
}