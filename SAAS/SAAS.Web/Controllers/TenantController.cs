using Microsoft.AspNetCore.Mvc;
using SAAS.Web.Application.Tenants.Commands.InitialiseTenant;
using SAAS.Web.Application.Tenants.Commands.UpdateUrl;
using SAAS.Web.Application.Tenants.Queries.GetAllTenants;
using SAAS.Web.Application.Tenants.Queries.GetTenant;
using SharedKernel.BaseController;

namespace SAAS.Web.Controllers;

public class TenantController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<TenantDTO>>> GetAll()
    {
        return await Mediator.Send(new GetAllTenantsQuery());
    }

    [HttpGet("{Id}")]
    public async Task<ActionResult<TenantDTO>> GetById([FromRoute] GetTenantQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpPost]
    public async Task<ActionResult<int>> InitialiseTenant([FromBody] InitialiseTenantCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPut]
    [Route("UpdateUrl")]
    public async Task<ActionResult<int>> UpdateUrl([FromBody] UpdateUrlCommand command)
    {
        return await Mediator.Send(command);
    }
}
