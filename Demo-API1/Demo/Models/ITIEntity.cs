using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Demo.Models
{
    public class ITIEntity:IdentityDbContext<ApplicationUser>
    {
        public ITIEntity()
        {
        }
        public ITIEntity(DbContextOptions options):base(options)
        {
        }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}
