using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPractice.Dtos.ProductDtos
{
    public class ProductCreateDto
    {
        public double Price { get; set; }
        public string Name { get; set; }
        public bool isActive { get; set; }
        public IFormFile Photo { get; set; }
    }
    public class ProductCreateDtoValidator : AbstractValidator<ProductCreateDto>
    {

        public ProductCreateDtoValidator()
        {
            RuleFor(p => p.Name).NotEmpty().MaximumLength(30).WithMessage("name cannot be over than 30 symbols");
            RuleFor(p => p.Price).GreaterThan(50).WithMessage("price cannot be less than 50");
        }
    }
}
