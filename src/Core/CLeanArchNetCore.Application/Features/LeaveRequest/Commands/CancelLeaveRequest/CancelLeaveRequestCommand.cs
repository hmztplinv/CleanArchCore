using MediatR;

public class CancelLeaveRequestCommand: IRequest<Unit>
{
    public int Id { get; set; }
}