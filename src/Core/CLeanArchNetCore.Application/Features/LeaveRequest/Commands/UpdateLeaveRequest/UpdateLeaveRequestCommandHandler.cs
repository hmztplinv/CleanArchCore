using AutoMapper;
using MediatR;

public class UpdateLeaveRequestCommandHandler : IRequestHandler<UpdateLeaveRequestCommand, Unit>
{
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IEmailSender _emailSender;
    private readonly IMapper _mapper;
    private readonly IAppLogger<UpdateLeaveRequestCommandHandler> _appLogger;

    public UpdateLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository, IMapper mapper, 
    ILeaveTypeRepository leaveTypeRepository, IEmailSender emailSender,IAppLogger<UpdateLeaveRequestCommandHandler> appLogger)
    {
        _leaveRequestRepository = leaveRequestRepository;
        _mapper = mapper;
        _leaveTypeRepository = leaveTypeRepository;
        _emailSender = emailSender;
        _appLogger = appLogger;
    }


    public async Task<Unit> Handle(UpdateLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        var leaveRequest = await _leaveRequestRepository.GetByIdAsync(request.Id);
        if (leaveRequest == null)
        {
            throw new NotFoundException(nameof(LeaveRequest), request.Id);
        }
        var validator = new UpdateLeaveRequestCommandValidator(_leaveTypeRepository, _leaveRequestRepository);
        var validationResult = await validator.ValidateAsync(request);
        if (validationResult.Errors.Any())
        {
            throw new BadRequestException("Invalid request", validationResult);
        }

        _mapper.Map(request, leaveRequest);
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