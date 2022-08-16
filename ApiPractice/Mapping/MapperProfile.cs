using ApiPractice.Dtos.CategoryDtos;
using ApiPractice.Dtos.ProductDtos;
using ApiPractice.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPractice.Mapping
{
    public class MapperProfile :Profile
    {
        public MapperProfile()
        {
            CreateMap<Category, ProductCategoryDto>()
                .ReverseMap();
            CreateMap<Product, ProductReturnDto>()
                .ForMember(d => d.ImageUrl, map => map.MapFrom(s => "http://localhost:5635/img/" + s.ImageUrl));
            CreateMap<Category, CategoryReturnDto>()
                .ForMember(d => d.ProductCount, map => map.MapFrom(s => s.Products.Count));
        }
    }
}
