using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPractice.Models
{
    public class Product: BaseEntity
    {
        public double Price { get; set; }
        public string Name { get; set; }
        public bool isActive { get; set; }
        public int CategoryId { get; set; }
        public Category Categories { get; set; }
        public string ImageUrl { get; set; }
    }
}
