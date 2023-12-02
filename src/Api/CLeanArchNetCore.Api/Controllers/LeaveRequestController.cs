using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
// [Authorize]
public class LeaveRequestController:ControllerBase
{
    private readonly IMediator _mediator;

    public LeaveRequestController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<LeaveRequestListDto>>> Get(bool isLoggedInUser = false)
    {
        var leaveRequests = await _mediator.Send(new GetLeaveRequestListQuery());
        return Ok(leaveRequests);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<LeaveRequestListDto>> Get(int id)
    {
        var leaveRequest = await _mediator.Send(new GetLeaveRequestDetailQuery { Id = id });
        return Ok(leaveRequest);
    }

    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Post(CreateLeaveRequestCommand createLeaveRequestCommand)
    {
        var result = await _mediator.Send(createLeaveRequestCommand);
        return CreatedAtAction(nameof(Get), new { id = result });
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(400)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Put(UpdateLeaveRequestCommand updateLeaveRequestCommand)
    {
        await _mediator.Send(updateLeaveRequestCommand);
        return NoContent();
    }

    [HttpPut]
    [Route("CancelRequest")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(400)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> CancelRequest(CancelLeaveRequestCommand cancelLeaveRequestCommand)
    {
        await _mediator.Send(cancelLeaveRequestCommand);
        return NoContent();
    }

    [HttpPut]
    [Route("UpdateApproval")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(400)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> UpdateApproval(ChangeLeaveRequestApprovalCommand updateLeaveRequestApprovalCommand)
    {
        await _mediator.Send(updateLeaveRequestApprovalCommand);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Delete(int id)
    {
        var command=new DeleteLeaveRequestCommand {Id = id};
        await _mediator.Send(command);
        return NoContent();
    }
}