﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessService.Domain
{
    public class ProductDTO
    {
        public int IdProduct { get; set; }
        public string Description { get; set; }
        public CategoryDTO Category { get; set; }
    }
}
