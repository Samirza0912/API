using ApiPractice.Data;
using ApiPractice.Dtos.CategoryDtos;
using ApiPractice.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly IMapper _mapper;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet("{Id}")]
        public IActionResult GetOne(int id)
        {
            Category c = _context.Categories.Include(c => c.Products).FirstOrDefault(p => p.Id == id && !p.isDeleted);
            if (c==null)
            {
                return NotFound();
            }
            CategoryReturnDto categoryReturnDto = _mapper.Map<CategoryReturnDto>(c);
            return Ok(categoryReturnDto);
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var query = _context.Categories.Where(c => !c.isDeleted);
            //CategoryListDto categoryListDto = new CategoryListDto();
            //categoryListDto.Items = query.Select(c => new CategoryReturnDto
            //{
            //    Name = c.Name
            //}).ToList();
            //categoryListDto.TotalCount = query.Count();
            CategoryReturnDto categoryReturnDto = _mapper.Map<CategoryReturnDto>(query);
            return Ok(categoryReturnDto);
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
