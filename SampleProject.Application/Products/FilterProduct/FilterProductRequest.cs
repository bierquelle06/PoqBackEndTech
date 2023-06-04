using System;

namespace SampleProject.Application.Products.FilterProduct
{
    public class FilterProductRequest
    {
        public decimal? MinPrice { get; set; }

        public decimal? MaxPrice { get; set; }

        public string Size { get; set; }
        
        public string Highlight { get; set; }
    }
}