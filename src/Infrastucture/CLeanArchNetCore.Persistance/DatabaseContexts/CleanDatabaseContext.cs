using Microsoft.EntityFrameworkCore;

public class CleanDatabaseContext : DbContext
{
    public CleanDatabaseContext(DbContextOptions<CleanDatabaseContext> options) : base(options)
    {
    }

    public DbSet<LeaveAllocation> LeaveAllocations { get; set; }
    public DbSet<LeaveType> LeaveTypes { get; set; }
    public DbSet<LeaveRequest> LeaveRequests { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CleanDatabaseContext).Assembly);
        

        
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in base.ChangeTracker.Entries<BaseEntity>()
        .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified))
        {
            entry.Entity.DateModified = DateTime.Now;
            if (entry.State == EntityState.Added)
            {
                entry.Entity.DateCreated = DateTime.Now;
            }
        }
        
        return base.SaveChangesAsync(cancellationToken);
    }
    
}