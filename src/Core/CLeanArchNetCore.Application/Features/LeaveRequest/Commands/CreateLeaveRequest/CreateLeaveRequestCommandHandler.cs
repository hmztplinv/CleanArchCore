using AutoMapper;
using MediatR;

namespace CleanArchNetCore.Application.Features.LeaveRequests.Commands.CreateLeaveRequest
{
    public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand, int>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IEmailSender _emailSender;
        private readonly IMapper _mapper;
        private readonly IAppLogger<CreateLeaveRequestCommandHandler> _appLogger;

        public CreateLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository, IMapper mapper, 
        ILeaveTypeRepository leaveTypeRepository, IEmailSender emailSender,IAppLogger<CreateLeaveRequestCommandHandler> appLogger)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _mapper = mapper;
            _leaveTypeRepository = leaveTypeRepository;
            _emailSender = emailSender;
            _appLogger = appLogger;
        }

        public async Task<int> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateLeaveRequestCommandValidator(_leaveTypeRepository);
            var validationResult = await validator.ValidateAsync(request);
            if (validationResult.Errors.Any())
            {
                throw new BadRequestException("Invalid request", validationResult);
            }

            var leaveRequest = _mapper.Map<LeaveRequest>(request);
            await _leaveRequestRepository.CreateAsync(leaveRequest);

            try
            {
                var email = new EmailMessage
                {
                    To = string.Empty,
                    Subject = "New Leave Request Submitted",
                    Body = $"A new leave request for {leaveRequest.StartDate.ToShortDateString()} to {leaveRequest.EndDate.ToShortDateString()} has been submitted"
                };
                await _emailSender.SendEmail(email);

            }
            catch (Exception ex)
            {
                _appLogger.LogWarning(ex.Message);
                throw;
            }
            return leaveRequest.Id;
        }
    }
}