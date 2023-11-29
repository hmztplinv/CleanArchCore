using MediatR;

public class DeleteLeaveTypeCommand : IRequest<Unit>
{
    public int Id { get; set; }
}