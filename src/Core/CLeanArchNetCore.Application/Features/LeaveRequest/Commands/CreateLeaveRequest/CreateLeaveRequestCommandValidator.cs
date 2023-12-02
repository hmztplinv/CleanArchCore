using FluentValidation;

public class CreateLeaveRequestCommandValidator:AbstractValidator<CreateLeaveRequestCommand>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public CreateLeaveRequestCommandValidator(ILeaveTypeRepository leaveTypeRepository)
    {
        _leaveTypeRepository = leaveTypeRepository;
        Include(new BaseLeaveRequestValidator(_leaveTypeRepository));
    }
}