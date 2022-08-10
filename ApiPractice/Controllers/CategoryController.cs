using ApiPractice.Data;
using ApiPractice.Dtos.CategoryDtos;
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
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Create(CategoryCreateDto categoryCreateDto)
        {
            Category category = new Category
            {
                Name = categoryCreateDto.Name
            };
            if (category == null)
            {
                return NotFound();
            }
            _context.Categories.Add(category);
            _context.SaveChanges();
            return StatusCode(201);
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, CategoryUpdateDto categoryUpdateDto)
        {
            Category category = _context.Categories.FirstOrDefault(c=>c.Id==id);
            if (category==null)
            {
                return NotFound();
            }

            category.Name = categoryUpdateDto.Name;
            _context.SaveChanges();
            return Ok(category);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Category dbCategory = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (dbCategory==null)
            {
                NotFound();
            }
            return Ok(dbCategory);
        }
    }
}
