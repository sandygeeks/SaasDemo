using Microsoft.AspNetCore.Mvc;
using SAAS.Web.Application.Customers.Commands.RegisterCustomer;
using SAAS.Web.Application.Customers.Commands.SubscribeService;
using SAAS.Web.Application.Customers.Queries.GetAllCustomers;
using SAAS.Web.Application.Customers.Queries.GetSubscriptionDetail;
using SharedKernel.BaseController;

namespace SAAS.Web.Controllers;

public class CustomerController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<CustomerDTO>>> GetAll()
    {
        return await Mediator.Send(new GetAllCustomersQuery());
    }

    [HttpGet("GetSubscriptionDetail")]
    public async Task<ActionResult<List<SubscriptionDTO>>> GetSubscriptionDetail([FromQuery] GetSubscriptionDetailsQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpPost]
    public async Task<ActionResult<int>> RegisterCustomer(RegisterCustomerCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPut]
    public async Task<ActionResult<int>> SubscribeService(SubscribeServiceCommand command)
    {
        return await Mediator.Send(command);
    }
}
