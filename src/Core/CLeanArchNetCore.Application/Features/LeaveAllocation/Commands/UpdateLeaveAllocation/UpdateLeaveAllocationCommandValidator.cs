using System.Data;
using FluentValidation;
namespace CLeanArchNetCore.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation

{
    public class UpdateLeaveAllocationCommandValidator : AbstractValidator<UpdateLeaveAllocationCommand>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;
        public UpdateLeaveAllocationCommandValidator(ILeaveTypeRepository leaveTypeRepository, ILeaveAllocationRepository leaveAllocationRepository)
        {
            _leaveTypeRepository = leaveTypeRepository;

            RuleFor(p => p.NumberOfDays).GreaterThan(0).NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(p => p.Period).GreaterThanOrEqualTo(DateTime.Now.Year).NotEmpty().WithMessage("{PropertyName} is required and cannot be less than current year.");
            RuleFor(p => p.LeaveTypeId).GreaterThan(0).MustAsync(LeaveTypeMustExist).WithMessage("{PropertyName} does not exist.");

            RuleFor(p => p.Id).NotNull().NotEmpty().MustAsync(LeaveAllocationMustExist).WithMessage("{PropertyName} is required.");
            _leaveAllocationRepository = leaveAllocationRepository;
        }

        private async Task<bool> LeaveTypeMustExist(int leaveTypeId, CancellationToken cancellationToken)
        {
            var leaveType = await _leaveTypeRepository.GetByIdAsync(leaveTypeId);
            return leaveType != null;
        }

        private async Task<bool> LeaveAllocationMustExist(int id, CancellationToken cancellationToken)
        {
            var leaveAllocation = await _leaveAllocationRepository.GetByIdAsync(id);
            return leaveAllocation != null;
        }
        
    }
}