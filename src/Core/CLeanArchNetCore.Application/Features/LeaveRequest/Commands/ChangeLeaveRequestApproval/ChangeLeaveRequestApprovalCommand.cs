using MediatR;

public class ChangeLeaveRequestApprovalCommand: IRequest<Unit>
{
    public int Id { get; set; }
    public bool Approved { get; set; }
}