using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace SignalR.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class MemberController : ControllerBase
{
    [HttpGet]
    public IActionResult GetConnectedClients()
        => Ok(MemberHub.ConnectedClients);

    [HttpGet]
    public IActionResult GetActiveMemberList()
        => Ok(MemberHub.GetActiveMemberList());
    
    [HttpPost]
    public IActionResult AddActiveMember([FromBody] Guid memberId)
    {
        MemberHub.AddActiveMember(memberId);
        return Ok("added");
    }

    [HttpDelete]
    public IActionResult DeleteActiveMember([FromBody] Guid memberId)
    {
        MemberHub.DeleteActiveMember(memberId);
        return Ok("deleted");
    }

    [HttpDelete]
    public IActionResult DeleteAllActiveMember()
    {
        MemberHub.DeleteAllActiveMembers();
        return Ok("all active members deleted");
    }
}