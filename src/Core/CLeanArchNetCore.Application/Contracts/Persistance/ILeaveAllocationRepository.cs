public interface ILeaveAllocationRepository:IGenericRepository<LeaveAllocation>
{
    Task<LeaveAllocation>GetLeaveAllocationWithDetails(int id);
    Task<List<LeaveAllocation>>GetLeaveAllocationsWithDetails();
    Task<List<LeaveAllocation>>GetLeaveAllocationsWithDetails(string userId);
    Task<bool>AllocationExists(int leaveTypeId, string userId, int period);
    Task AddAllocations(List<LeaveAllocation> allocations);
    Task<LeaveAllocation>GetUserAllocations(string userId, int leaveTypeId);
}