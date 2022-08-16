using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPractice.Dtos.ProductDtos
{
    public class ProductReturnDto
    {

        public double Price { get; set; }
        public string Name { get; set; }
        public bool isActive { get; set; }
        public string ImageUrl { get; set; }
        public IFormFile Photo { get; set; }
        public ProductCategoryDto Category { get; set; }
    }
    public class ProductCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
