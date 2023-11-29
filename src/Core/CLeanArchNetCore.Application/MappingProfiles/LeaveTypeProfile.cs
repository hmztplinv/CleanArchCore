using AutoMapper;

public class LeaveTypeProfile:Profile
{
    public LeaveTypeProfile()
    {
        CreateMap<LeaveType, LeaveTypeDto>().ReverseMap();
        // CreateMap<LeaveType, CreateLeaveTypeDto>().ReverseMap();
        // CreateMap<LeaveType, UpdateLeaveTypeDto>().ReverseMap();
    }
}