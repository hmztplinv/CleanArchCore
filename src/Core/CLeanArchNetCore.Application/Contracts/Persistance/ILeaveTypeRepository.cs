public interface ILeaveTypeRepository:IGenericRepository<LeaveType>
{
    Task<bool> IsLeaveTypeNameUnique(string name);
}