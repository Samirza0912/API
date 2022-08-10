using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPractice.Models
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime UpdatededTime { get; set; }
    }
}
