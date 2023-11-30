
using Microsoft.EntityFrameworkCore;

public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
{
    public LeaveAllocationRepository(CleanDatabaseContext dbContext) : base(dbContext)
    {
    }

    public async Task AddAllocations(List<LeaveAllocation> allocations)
    {
        await _dbContext.AddRangeAsync(allocations);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> AllocationExists(int leaveTypeId, string userId, int period)
    {
        return await _dbContext.LeaveAllocations.AnyAsync(q => q.LeaveTypeId == leaveTypeId
                                                                && q.EmployeeId == userId
                                                                && q.Period == period);
    }

    public async Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails()
    {
        var leaveAllocations=await _dbContext.LeaveAllocations
            .Include(q => q.LeaveType).ToListAsync();
        return leaveAllocations;
    }

    public async Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails(string userId)
    {
        var leaveAllocations= await _dbContext.LeaveAllocations.Where(q => q.EmployeeId == userId)
            .Include(q => q.LeaveType).ToListAsync();
        return leaveAllocations;
    }

    public async Task<LeaveAllocation> GetLeaveAllocationWithDetails(int id)
    {
        var leaveAllocation= await _dbContext.LeaveAllocations
            .Include(q => q.LeaveType).FirstOrDefaultAsync(q => q.Id == id);
        if (leaveAllocation == null)
        {
            throw new NotFoundException(nameof(LeaveAllocation), id);
        }
        return leaveAllocation;
    }

    public async Task<LeaveAllocation> GetUserAllocations(string userId, int leaveTypeId)
    {
        var period=DateTime.Now.Year;
        var res= await _dbContext.LeaveAllocations.FirstOrDefaultAsync(q => q.EmployeeId == userId
                                                                     && q.LeaveTypeId == leaveTypeId
                                                                     && q.Period == period);
        if (res == null)
        {
            throw new NotFoundException(nameof(LeaveAllocation), userId);
        }
        return res;
    }
}