using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class LeaveTypeConfiguration:IEntityTypeConfiguration<LeaveType>
{
    public void Configure(EntityTypeBuilder<LeaveType> builder)
    {
        builder.HasData(
            new LeaveType
            {
                Id = 1,
                Name = "Vacation",
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
                DefaultDays = 10
            },
            new LeaveType
            {
                Id = 2,
                Name = "Sick",
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
                DefaultDays = 5
            }
        );

        builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
    }
}