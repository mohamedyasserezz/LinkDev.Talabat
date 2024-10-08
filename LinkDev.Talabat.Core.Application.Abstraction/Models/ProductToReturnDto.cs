﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Application.Abstraction.Models
{
    public class ProductToReturnDto
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public string? PictureUrl { get; set; }
        public decimal Price { get; set; }
        public int? BrandId { get; set; } // FK --> for Product and Brand Relationship
        public int? CategoryId { get; set; } // FK --> for Product and Category Relationship
        public virtual string? Brand { get; set; }
        public virtual string? Category { get; set; }
    }

}
