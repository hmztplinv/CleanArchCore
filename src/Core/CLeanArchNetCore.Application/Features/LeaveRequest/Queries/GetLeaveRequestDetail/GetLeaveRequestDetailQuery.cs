using MediatR;

public class GetLeaveRequestDetailQuery : IRequest<LeaveRequestDetailsDto>
{
    public int Id { get; set; }
}