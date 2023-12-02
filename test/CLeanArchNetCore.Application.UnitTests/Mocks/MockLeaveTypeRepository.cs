using Moq;

public class MockLeaveTypeRepository
{
    public static Mock<ILeaveTypeRepository> GetLeaveTypeMockLeaveTypeRepository()
    {
        var leaveTypes = new List<LeaveType>
        {
            new LeaveType
            {
                Id = 1,
                Name = "Test Vacation",
                DefaultDays = 10,
            },
            new LeaveType
            {
                Id = 2,
                Name = "Test Sick",
                DefaultDays = 10,
            }
        };

        var mockLeaveTypeRepository = new Mock<ILeaveTypeRepository>();
        mockLeaveTypeRepository.Setup(repo => repo.GetAsync()).ReturnsAsync(leaveTypes);
        mockLeaveTypeRepository.Setup(repo => repo.CreateAsync(It.IsAny<LeaveType>())).Returns((LeaveType leaveType) =>
        {
            leaveTypes.Add(leaveType);
            return Task.CompletedTask;
        });

        return mockLeaveTypeRepository;
    }
}