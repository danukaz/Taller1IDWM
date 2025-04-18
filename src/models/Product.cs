using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Taller.src.models
{
    public class Product
    {
        public int id { get; set;}
        public required string name { get; set; }
        public required string description { get; set; }
        public decimal price { get; set; }
        public required string category { get; set; }
        public string[]? urls { get; set; }
        public int stock { get; set; }
        public required string brand { get; set;}
    }
}