using MediatR;
using SampleProject.Application.Configuration.Commands;
using System;

namespace SampleProject.Application.Products.FilterProduct
{
    public class FilterProductCommand : CommandBase<ProductFilterDto>
    {
        public decimal? MinPrice { get; set; }

        public decimal? MaxPrice { get; set; }

        public string Size { get; set; }

        public string Highlight { get; set; }

        public FilterProductCommand(decimal? minPrice,
            decimal? maxPrice,
            string size,
            string highlight)
        {
            this.MinPrice = minPrice;
            this.MaxPrice = maxPrice;
            this.Size = size;  
            this.Highlight = highlight;
        }      
    }
}