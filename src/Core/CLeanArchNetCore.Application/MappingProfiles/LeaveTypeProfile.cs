using AutoMapper;

public class LeaveTypeProfile:Profile
{
    public LeaveTypeProfile()
    {
        CreateMap<LeaveType, LeaveTypeDto>().ReverseMap();
        CreateMap<LeaveType,LeaveTypeDetailsDto>();
        CreateMap<CreateLeaveTypeCommand,LeaveType>();
        CreateMap<UpdateLeaveTypeCommand,LeaveType>();
    }
}