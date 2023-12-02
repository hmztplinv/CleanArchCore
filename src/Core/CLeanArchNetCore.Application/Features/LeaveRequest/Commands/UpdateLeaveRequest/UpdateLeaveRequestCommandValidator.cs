using FluentValidation;

public class UpdateLeaveRequestCommandValidator:AbstractValidator<UpdateLeaveRequestCommand>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly ILeaveRequestRepository _leaveRequestRepository;

    public UpdateLeaveRequestCommandValidator(ILeaveTypeRepository leaveTypeRepository,ILeaveRequestRepository leaveRequestRepository)
    {
        _leaveTypeRepository = leaveTypeRepository;
        _leaveRequestRepository = leaveRequestRepository;
        Include(new BaseLeaveRequestValidator(_leaveTypeRepository));

        RuleFor(p=>p.Id).NotNull().MustAsync(LeaveRequestMustExist).WithMessage("{PropertyName} does not exist");
    }

    private async Task<bool> LeaveRequestMustExist(int id, CancellationToken cancellationToken)
    {
        var leaveAllocation = await _leaveRequestRepository.GetByIdAsync(id);
        return leaveAllocation != null;
    }
}