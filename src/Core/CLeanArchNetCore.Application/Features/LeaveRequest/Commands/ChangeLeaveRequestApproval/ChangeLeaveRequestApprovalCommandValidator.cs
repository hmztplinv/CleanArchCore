using FluentValidation;

public class ChangeLeaveRequestApprovalCommandValidator:AbstractValidator<ChangeLeaveRequestApprovalCommand>{
    public ChangeLeaveRequestApprovalCommandValidator()
    {
        RuleFor(p=>p.Approved).NotNull().WithMessage("{PropertyName} must be provided");
    }
}