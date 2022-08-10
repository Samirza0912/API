using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPractice.Dtos.CategoryDtos
{
    public class CategoryListDto
    {
        public int TotalCount { get; set; }
        public List<CategoryReturnDto> Items { get; set; }
    }
}
