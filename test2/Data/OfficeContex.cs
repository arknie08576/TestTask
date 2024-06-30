
using Microsoft.EntityFrameworkCore;
using test2.Models;

namespace test2.Data
{
    public class OfficeContex : DbContext
    {
        public OfficeContex(DbContextOptions<OfficeContex> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Employee> Employes { get; set; }
        public DbSet<ApprovalRequest> ApprovalRequests { get; set; }
        public DbSet<LeaveRequest> LeaveRequests { get; set; }
        public DbSet<Project> Projects { get; set; }
    }
}
