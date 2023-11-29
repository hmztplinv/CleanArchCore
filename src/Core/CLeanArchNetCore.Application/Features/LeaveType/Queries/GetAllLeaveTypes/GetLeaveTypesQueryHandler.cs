using AutoMapper;
using MediatR;

public class GetLeaveTypesQueryHandler:IRequestHandler<GetLeaveTypesQuery,List<LeaveTypeDto>>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IMapper _mapper;

    public GetLeaveTypesQueryHandler(ILeaveTypeRepository leaveTypeRepository, IMapper mapper)
    {
        _leaveTypeRepository = leaveTypeRepository;
        _mapper = mapper;
    }

    public async Task<List<LeaveTypeDto>> Handle(GetLeaveTypesQuery request, CancellationToken cancellationToken)
    {
        var leaveTypes = await _leaveTypeRepository.GetAsync();
        return _mapper.Map<List<LeaveTypeDto>>(leaveTypes);
    }
}