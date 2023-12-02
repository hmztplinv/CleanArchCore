using MediatR;

public class DeleteLeaveAllocationCommand:IRequest<Unit>
{
    public int Id { get; set; }
}