using System.Drawing;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

using SampleProject.Application.Products;
using SampleProject.Application.Products.FilterProduct;

namespace SampleProject.API.Customers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : Controller
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        /// <summary>
        /// Filter with parameter 
        /// URL : filter?minprice=5&maxprice=20&size=medium&highlight=green,blue
        /// </summary>
        /// <param name="minprice"></param>
        /// <param name="maxprice"></param>
        /// <param name="size"></param>
        /// <param name="highlight"></param>
        /// <returns></returns>
        [Route("filter")]
        [HttpGet]
        [ProducesResponseType(typeof(ProductFilterDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Filter(decimal? minprice, decimal? maxprice, string size, string highlight)
        {
            var request = new FilterProductRequest() {
                Size = size,
                MinPrice = minprice,
                MaxPrice = maxprice,
                Highlight = highlight
            };

            var result = await _mediator.Send(new FilterProductCommand(
                minPrice: request.MinPrice,
                maxPrice: request.MaxPrice,
                size: request.Size,
                highlight: request.Highlight));

            return Ok(result);
        }

        /// <summary>
        /// Filter product
        /// </summary>
        [Route("filterproduct")]
        [HttpPost]
        [ProducesResponseType(typeof(ProductFilterDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> FilterProduct([FromBody] FilterProductRequest request)
        {
            var result = await _mediator.Send(new FilterProductCommand(
                minPrice: request.MinPrice,
                maxPrice: request.MaxPrice,
                size: request.Size,
                highlight: request.Highlight));

            return Ok(result);
        }
    }
}
