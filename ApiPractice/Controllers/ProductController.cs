using ApiPractice.Data;
using ApiPractice.Dtos.ProductDtos;
using ApiPractice.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPractice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet("{id}")]
        public IActionResult GetOne(int id)
        {
            Product product = _context.Products.Where(p => p.isActive).FirstOrDefault(p=>p.Id==id);
            if (product == null)
            {
                return NotFound();
            }
            ProductReturnDto productReturnDto = new ProductReturnDto();
            productReturnDto.Name = product.Name;
            productReturnDto.Price = product.Price;
            productReturnDto.isActive = product.isActive;

            return Ok(productReturnDto);
         }
        [HttpGet]
        public IActionResult GetAll()
        {
            var query = _context.Products.Where(p => !p.isDeleted);
            ProductListDto productListDto = new ProductListDto();
            productListDto.Items = query.Select(p=>new ProductReturnDto
            {
                Name = p.Name,
                Price = p.Price,
                isActive = p.isActive
            }).Skip(1).Take(1).ToList();
            productListDto.TotalCount = query.Count();
             return Ok(productListDto);
        }
        [HttpPost]
        public IActionResult Create(ProductCreateDto productCreateDto)
        {
            Product p = new Product
            {
                Name = productCreateDto.Name,
                Price = productCreateDto.Price,
                isActive = productCreateDto.isActive
            };
            if (productCreateDto==null)
            {
                return NotFound();
            }
            _context.Products.Add(p);
            _context.SaveChanges();
            return Ok(p);
        }
        [HttpPut("{id}")]
        public IActionResult Update(ProductUpdateDto productUpdateDto, int id)
        {
            Product newProduct = _context.Products.FirstOrDefault(p => p.Id == id);
            if (newProduct == null)
            {
                return NotFound();
            }
            newProduct.Name = productUpdateDto.Name;
            newProduct.Price = productUpdateDto.Price;
            newProduct.isActive = productUpdateDto.isActive;
            _context.SaveChanges();
            return Ok(newProduct);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int? id)
        {
            if (id==null)
            {
                return NotFound();
            }
            Product dbProduct = _context.Products.FirstOrDefault(p => p.Id == id);
            if (dbProduct == null)
            {
                return NotFound();
            }
            _context.Products.Remove(dbProduct);
            _context.SaveChanges();
            return Ok(dbProduct);
        }
        [HttpGet("{id}")]

        public IActionResult Detail(int? id)
        {
            if (id == null) return NotFound();
            Product dbProduct = _context.Products.FirstOrDefault(p => p.Id == id);
            if (dbProduct == null) return NotFound();
            return Ok(dbProduct);
        }
    }
}
