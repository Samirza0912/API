using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPractice.Dtos.ProductDtos
{
    public class ProductUpdateDto
    {

        public double Price { get; set; }
        public string Name { get; set; }
        public bool isActive { get; set; }
        public IFormFile Photo { get; set; }
    }
}
