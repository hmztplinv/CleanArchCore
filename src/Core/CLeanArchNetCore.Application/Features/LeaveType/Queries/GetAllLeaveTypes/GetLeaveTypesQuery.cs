using MediatR;

public record GetLeaveTypesQuery : IRequest<List<LeaveTypeDto>>
{
    
}