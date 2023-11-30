
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

public class LeaveRequestRepository : GenericRepository<LeaveRequest>, ILeaveRequestRepository
{
    public LeaveRequestRepository(CleanDatabaseContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<LeaveRequest>> GetLeaveRequestsWithDetails()
    {
        var leaveRequests=await _dbContext.LeaveRequests
            .Include(q => q.LeaveType).ToListAsync();
        return leaveRequests;
    }

    public async Task<List<LeaveRequest>> GetLeaveRequestsWithDetails(string userId)
    {
        var leaveRequests=await _dbContext.LeaveRequests.Where(q => q.RequestingEmployeeId == userId)
            .Include(q => q.LeaveType).ToListAsync();
        return leaveRequests;
    }

    public async Task<LeaveRequest> GetLeaveRequestWithDetails(int id)
    {
        var leaveRequest=await _dbContext.LeaveRequests
            .Include(q => q.LeaveType).FirstOrDefaultAsync(q => q.Id == id);
        if (leaveRequest == null)
        {
            throw new NotFoundException(nameof(LeaveRequest), id);
        }
        return leaveRequest;
    }
}