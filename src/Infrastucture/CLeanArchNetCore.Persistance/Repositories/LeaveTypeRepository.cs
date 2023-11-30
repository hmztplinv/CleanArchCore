
using Microsoft.EntityFrameworkCore;

public class LeaveTypeRepository:GenericRepository<LeaveType>,ILeaveTypeRepository
{

    public LeaveTypeRepository(CleanDatabaseContext dbContext) : base(dbContext)
    {
        
    }

    public async Task<bool> IsLeaveTypeNameUnique(string name)
    {
        return await _dbContext.LeaveTypes.AnyAsync(lt => lt.Name == name)==false;
    }
}