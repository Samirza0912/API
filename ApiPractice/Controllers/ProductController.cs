using ApiPractice.Data;
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
            Product product = _context.Products.FirstOrDefault(p=>p.Id==id);
            if (product==null)
            {
                return NotFound();
            }
            return Ok(product);
         }
        [HttpGet]
        public IActionResult GetAll()
        {
             return Ok( _context.Products.Where(p=>p.isActive).ToList());
        }
        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (product==null)
            {
                return NotFound();
            }
            _context.Products.Add(product);
            _context.SaveChanges();
            return Ok(product);
        }
        [HttpPut("{id}")]
        public IActionResult Update(Product product, int id)
        {
            Product newProduct = _context.Products.FirstOrDefault(p => p.Id == id);
            if (newProduct == null)
            {
                return NotFound();
            }
            newProduct.Name = product.Name;
            newProduct.Price = product.Price;
            newProduct.isActive = product.isActive;
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
