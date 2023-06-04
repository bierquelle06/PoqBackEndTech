using System;
using System.Collections.Generic;

namespace SampleProject.Application.Products
{
    public class ProductFilterDto
    {
        public IEnumerable<ProductDto> Product { get; set; }

        public decimal MinPrice { get; set; }

        public decimal MaxPrice { get; set; }

        public IEnumerable<string> Sizes { get; set; }

        public IEnumerable<string> CommonWords { get; set; }
    }
}