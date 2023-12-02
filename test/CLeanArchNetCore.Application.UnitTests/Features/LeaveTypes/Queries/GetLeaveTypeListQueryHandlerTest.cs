using AutoMapper;
using Moq;
using Shouldly;

public class GetLeaveTypeListQueryHandlerTest
{
    private readonly IMapper _mapper;
    private readonly Mock<ILeaveTypeRepository> _mockLeaveTypeRepository;
    private Mock<IAppLogger<GetLeaveTypesQueryHandler>> _mockLogger;

    public GetLeaveTypeListQueryHandlerTest()
    {
        _mockLeaveTypeRepository = MockLeaveTypeRepository.GetLeaveTypeMockLeaveTypeRepository();
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<LeaveTypeProfile>();
        });
        _mapper = mapperConfig.CreateMapper();
        _mockLogger = new Mock<IAppLogger<GetLeaveTypesQueryHandler>>();
    }

    [Fact]
    public async Task GetLeaveTypeListTest()
    {
        var handler = new GetLeaveTypesQueryHandler(_mockLeaveTypeRepository.Object, _mapper, _mockLogger.Object);
        var result = await handler.Handle(new GetLeaveTypesQuery(), CancellationToken.None);

        result.ShouldBeOfType<List<LeaveTypeDto>>();
        result.Count.ShouldBe(2);
        // Assert.IsType<List<LeaveTypeDto>>(result);
        // Assert.Equal(2, result.Count);
    }
}
