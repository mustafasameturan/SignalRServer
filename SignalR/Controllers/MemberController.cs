using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace SignalR.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MemberController : ControllerBase
{
    [HttpPost("updateActiveMembers")]
    public IActionResult UpdateActiveMembers([FromBody] List<Guid> activeMembers)
    {
        MemberHub.UpdateActiveMembersList(activeMembers);
        return Ok();
    }

    [HttpGet]
    public IActionResult GetConnectedClients()
    {
        var a = MemberHub.ConnectedClients;
        return Ok(a);
    }
}