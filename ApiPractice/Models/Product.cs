﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPractice.Models
{
    public class Product
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public string Name { get; set; }
        public bool isActive { get; set; }
    }
}
