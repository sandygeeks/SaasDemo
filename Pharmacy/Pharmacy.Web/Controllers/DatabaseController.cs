using Microsoft.AspNetCore.Mvc;
using SharedKernel.BaseController;
using Pharmacy.Web.Application.Commands.InitialiseDatabase;

namespace Pharmacy.Web.Controllers;

public class DatabaseController : ApiControllerBase
{
    [HttpPost]
    [Route("Initialise")]
    public async Task<ActionResult<int>> Initialise([FromBody] InitialiseDatabaseCommand command)
    {
        return await Mediator.Send(command);
    }
}
