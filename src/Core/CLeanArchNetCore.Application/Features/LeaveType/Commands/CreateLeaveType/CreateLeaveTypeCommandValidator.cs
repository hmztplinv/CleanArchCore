using FluentValidation;

public class CreateLeaveTypeCommandValidator : AbstractValidator<CreateLeaveTypeCommand>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    public CreateLeaveTypeCommandValidator(ILeaveTypeRepository leaveTypeRepository)
    {
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .MaximumLength(70).WithMessage("{PropertyName} must not exceed 70 characters.");

        RuleFor(p => p.DefaultDays)
            // .NotEmpty().WithMessage("{PropertyName} is required.")
            // .NotNull()
            .GreaterThan(1).WithMessage("{PropertyName} must be greater than or equal to 1.")
            .LessThan(100).WithMessage("{PropertyName} must be less than or equal to 100.");
        
        RuleFor(q => q)
            .MustAsync(LeaveTypeNameUnique)
            .WithMessage("Leave Type Name must be unique");
        this._leaveTypeRepository = leaveTypeRepository;

    }

    private Task<bool> LeaveTypeNameUnique(CreateLeaveTypeCommand command, CancellationToken token)
    {
        return _leaveTypeRepository.IsLeaveTypeNameUnique(command.Name);
    }
}