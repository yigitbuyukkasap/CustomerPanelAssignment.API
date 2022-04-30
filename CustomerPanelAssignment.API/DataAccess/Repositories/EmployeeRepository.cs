using CustomerPanelAssignment.API.Models;
using DataAccess.Data;
using DataAccess.Repositories.IRepository;
using System;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        private readonly ApplicationDbContext _db;

        public EmployeeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Employee> Update(Employee obj, Guid employeeId)
        {
            var employee = await FirstOrDefault(c => c.Id.Equals(employeeId));
            if (employee != null)
            {
                employee.Name= obj.Name;
                employee.LastName= obj.LastName;
                employee.Email= obj.Email;
                employee.PhoneNumber= obj.PhoneNumber;
                employee.CustomerId= obj.CustomerId;

                await _db.SaveChangesAsync();
                return employee;
            }
            return null;
        }
    }
}
