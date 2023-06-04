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
        /// Filter product
        /// </summary>
        [Route("filter")]
        [HttpPost]
        [ProducesResponseType(typeof(ProductFilterDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> RegisterCustomer([FromBody] FilterProductRequest request)
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
