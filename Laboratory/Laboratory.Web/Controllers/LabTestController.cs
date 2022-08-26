using Laboratory.Web.Application.LabTests.Commands.Add;
using Laboratory.Web.Application.LabTests.Queries.GetAll;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.BaseController;

namespace Laboratory.Web.Controllers;

public class LabTestController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<LabTestDTO>>> GetAll()
    {
        return await Mediator.Send(new GetAllLabTestsQuery());
    }

    [HttpPost]
    public async Task<ActionResult<int>> Post([FromBody] AddLabTestCommand command)
    {
        return await Mediator.Send(command);
    }
}
