using ApiPractice.Data;
using ApiPractice.Dtos.ProductDtos;
using ApiPractice.Extentions;
using ApiPractice.Models;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPractice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;

        public ProductController(AppDbContext context, IWebHostEnvironment env, IMapper mapper)
        {
            _context = context;
            _env = env;
            _mapper = mapper;
        }
        [HttpGet("{id}")]
        public IActionResult GetOne(int id)
        {
            Product product = _context.Products.Where(p => p.isActive).FirstOrDefault(p=>p.Id==id);
            if (product == null)
            {
                return NotFound();
            }
            //ProductReturnDto productReturnDto = new ProductReturnDto();
            //productReturnDto.Name = product.Name;
            //productReturnDto.Price = product.Price;
            //productReturnDto.isActive = product.isActive;
            //productReturnDto.ImageUrl = "http://localhost:5635/img/" + product.ImageUrl;
            ProductReturnDto productReturnDto = _mapper.Map<ProductReturnDto>(product);

            return Ok(productReturnDto);
         }
        [HttpGet]
        public IActionResult GetAll()
        {
            var query = _context.Products.AsQueryable();
            ProductListDto productListDto = new ProductListDto();
            productListDto.Items = query.Select(p=>new ProductReturnDto
            {
                Name = p.Name,
                Price = p.Price,
                isActive = p.isActive,
                ImageUrl = "http://localhost:5635/img/" + p.ImageUrl
            }).ToList();
            productListDto.TotalCount = query.Count();
             return Ok(productListDto);
        }
        [HttpPost]
        public IActionResult Create([FromForm]ProductCreateDto productCreateDto)
        {
            bool isExist = _context.Categories.Any(p => p.Name.ToLower() == productCreateDto.Name.ToLower());
            if (productCreateDto.Photo.IsImage())
            {
                return BadRequest();
            }
            if (productCreateDto.Photo.ValidSize(200))
            {
                return BadRequest();
            }
            Product p = new Product
            {
                Name = productCreateDto.Name,
                Price = productCreateDto.Price,
                isActive = productCreateDto.isActive,
                ImageUrl = productCreateDto.Photo.SaveImage(_env, "img")
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
        public IActionResult Update([FromForm]ProductUpdateDto productUpdateDto, int id)
        {
            Product newProduct = _context.Products.FirstOrDefault(p => p.Id == id);
            if (_context.Categories.Any(p => p.Name.ToLower() == productUpdateDto.Name.ToLower() && newProduct.Id != id))
            {
                return BadRequest();

            }
            if (newProduct == null)
            {
                return NotFound();
            }
            string path = Path.Combine(_env.WebRootPath, "img", newProduct.ImageUrl);
            if (productUpdateDto.Photo != null)
            {
                Helpers.Helper.DeleteImage(path);
            }
            newProduct.Price = productUpdateDto.Price;
            newProduct.isActive = productUpdateDto.isActive;
            newProduct.Name = productUpdateDto.Name;
            _context.SaveChanges();
            return Ok();

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
