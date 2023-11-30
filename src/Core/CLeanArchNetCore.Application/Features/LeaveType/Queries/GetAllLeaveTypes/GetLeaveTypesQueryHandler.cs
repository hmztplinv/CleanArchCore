using AutoMapper;
using MediatR;

public class GetLeaveTypesQueryHandler:IRequestHandler<GetLeaveTypesQuery,List<LeaveTypeDto>>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IMapper _mapper;
    private readonly IAppLogger<GetLeaveTypesQueryHandler> _logger;

    public GetLeaveTypesQueryHandler(ILeaveTypeRepository leaveTypeRepository, IMapper mapper, IAppLogger<GetLeaveTypesQueryHandler> logger)
    {
        _leaveTypeRepository = leaveTypeRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<List<LeaveTypeDto>> Handle(GetLeaveTypesQuery request, CancellationToken cancellationToken)
    {
        var leaveTypes = await _leaveTypeRepository.GetAsync();
        var leaveTypesDto = _mapper.Map<List<LeaveTypeDto>>(leaveTypes);
        _logger.LogInformation("Leave Types Returned");
        return leaveTypesDto;
    }
}