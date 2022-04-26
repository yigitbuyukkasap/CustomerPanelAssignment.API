using CustomerPanelAssignment.API.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerPanelAssignment.API.DataAccess
{
    public class CustomerPanelContext : DbContext
    {
        public CustomerPanelContext(DbContextOptions<CustomerPanelContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customer { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<CustomerEmployee> CustomerEmployee { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Department> Department { get; set; }
    }
}
