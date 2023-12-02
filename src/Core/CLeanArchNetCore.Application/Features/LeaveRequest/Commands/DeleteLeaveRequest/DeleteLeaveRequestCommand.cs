using MediatR;

public class DeleteLeaveRequestCommand : IRequest<Unit>
{
    public int Id { get; set; }
}