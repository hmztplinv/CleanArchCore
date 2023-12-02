using AutoMapper;
using MediatR;

public class ChangeLeaveRequestApprovalCommandHandler : IRequestHandler<ChangeLeaveRequestApprovalCommand, Unit>
{
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IEmailSender _emailSender;
    private readonly IMapper _mapper;
    private readonly IAppLogger<ChangeLeaveRequestApprovalCommandHandler> _appLogger;

    public ChangeLeaveRequestApprovalCommandHandler(ILeaveRequestRepository leaveRequestRepository,
     ILeaveTypeRepository leaveTypeRepository, IEmailSender emailSender, IMapper mapper, IAppLogger<ChangeLeaveRequestApprovalCommandHandler> appLogger)
    {
        _leaveRequestRepository = leaveRequestRepository;
        _leaveTypeRepository = leaveTypeRepository;
        _emailSender = emailSender;
        _mapper = mapper;
        _appLogger = appLogger;
    }

    public async Task<Unit> Handle(ChangeLeaveRequestApprovalCommand request, CancellationToken cancellationToken)
    {
        var leaveRequest = await _leaveRequestRepository.GetLeaveRequestWithDetails(request.Id);
        if (leaveRequest == null)
        {
            throw new NotFoundException(nameof(LeaveRequest), request.Id);
        }

        leaveRequest.Approved = request.Approved;
        await _leaveRequestRepository.UpdateAsync(leaveRequest);

        try
        {
            var email = new EmailMessage
            {
                To = string.Empty,
                Subject = "Leave Request Updated",
                Body = $"Your leave request for {leaveRequest.StartDate.ToShortDateString()} to {leaveRequest.EndDate.ToShortDateString()} has been updated"
            };
            await _emailSender.SendEmail(email);

        }
        catch (Exception ex)
        {
            _appLogger.LogWarning(ex.Message);
            throw;
        }
        return Unit.Value;
    }
}