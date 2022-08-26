using Microsoft.AspNetCore.Mvc;
using Pharmacy.Web.Application.Products.Commands.AddProductCommand;
using Pharmacy.Web.Application.Products.Queries.GetAllProducts;
using Pharmacy.Web.Application.Products.Queries.GetProductById;
using SharedKernel.BaseController;

namespace Pharmacy.Web.Controllers;

public class ProductController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<ProductDTO>>> GetAsync()
    {
        return await Mediator.Send(new GetAllProductQuery());
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<GetProductByIdDTO>> GetAsync([FromRoute] GetProductByIdQuery query)
    {
        return await Mediator.Send(query);

    }
    [HttpPost]
    public async Task<ActionResult<int>> CreateAsync([FromBody] AddProductCommand command)
    {
        return await Mediator.Send(command);
    }
}
