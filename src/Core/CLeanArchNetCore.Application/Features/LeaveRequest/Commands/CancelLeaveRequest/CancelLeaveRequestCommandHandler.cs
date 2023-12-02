using MediatR;

public class CancelLeaveRequestCommandHandler : IRequestHandler<CancelLeaveRequestCommand, Unit>
{
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly IEmailSender _emailSender;
    private readonly IAppLogger<CancelLeaveRequestCommandHandler> _appLogger;

    public CancelLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository, IEmailSender emailSender, IAppLogger<CancelLeaveRequestCommandHandler> appLogger)
    {
        _leaveRequestRepository = leaveRequestRepository;
        _emailSender = emailSender;
        _appLogger = appLogger;
    }
 

    public async Task<Unit> Handle(CancelLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        var leaveRequest = await _leaveRequestRepository.GetByIdAsync(request.Id);
        if (leaveRequest == null)
        {
            throw new NotFoundException(nameof(LeaveRequest), request.Id);
        }
        leaveRequest.Cancelled = true;

        try
        {
            var email = new EmailMessage
            {
                To = string.Empty,
                Subject = "Leave Request Cancelled",
                Body = $"Your leave request for {leaveRequest.StartDate.ToShortDateString()} to {leaveRequest.EndDate.ToShortDateString()} has been cancelled"
            };
            await _emailSender.SendEmail(email);

        }
        catch (Exception ex)
        {
            _appLogger.LogWarning(ex.Message);
            throw;
        }

        await _leaveRequestRepository.UpdateAsync(leaveRequest);
        return Unit.Value;
    }
}