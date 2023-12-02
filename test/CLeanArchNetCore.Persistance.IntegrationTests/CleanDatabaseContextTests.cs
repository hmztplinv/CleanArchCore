using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace CLeanArchNetCore.Persistance.IntegrationTests
{
    public class CleanDatabaseContextTests
    {
        private readonly CleanDatabaseContext _context;
        public CleanDatabaseContextTests()
        {
            var options = new DbContextOptionsBuilder<CleanDatabaseContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new CleanDatabaseContext(options);
        }

        [Fact]
        public async void Save_SetDateCreatedValue()
        {
            var leaveType= new LeaveType
            {
                Id = 1,
                DefaultDays = 10,
                Name="Test Vacation"
            };

            await _context.LeaveTypes.AddAsync(leaveType);
            await _context.SaveChangesAsync();

            leaveType.DateCreated.ShouldNotBeNull();
        }

        [Fact]

        public async void Save_SetDateModifiedValue()
        {
            var leaveType= new LeaveType
            {
                Id = 1,
                DefaultDays = 10,
                Name="Test Vacation"
            };

            await _context.LeaveTypes.AddAsync(leaveType);
            await _context.SaveChangesAsync();

            leaveType.DateModified.ShouldNotBeNull();
        }
    }
}