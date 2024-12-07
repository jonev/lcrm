using LCrm.Domain.Entries.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LCrm.Web.Controllers;

[ApiController]
[Route("api/command/[controller]")]
public class EntriesController(ISender sender) : ControllerBase
{
    [HttpPost]
    [Route("create")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    public IActionResult CreateBox([FromBody]CreateEntryCommand command)
    {
        // You should probably map your external contracts.
        sender.Send(command);
        return Accepted();
    }
    
    [HttpPost]
    [Route("change-status")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    public IActionResult ChangeStatus([FromBody]ChangeEntryStatusCommand command)
    {
        sender.Send(command);
        return Accepted();
    }
}
