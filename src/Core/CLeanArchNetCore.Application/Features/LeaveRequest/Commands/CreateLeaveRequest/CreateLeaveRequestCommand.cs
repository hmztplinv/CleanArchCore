using MediatR;

public class CreateLeaveRequestCommand: BaseLeaveRequest,IRequest<int>
{
    public string RequestComments { get; set; }=string.Empty;
}