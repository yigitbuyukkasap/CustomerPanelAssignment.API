using CustomerPanelAssignment.API.Models;
using DataAccess.Data;
using DataAccess.Repositories.IRepository;
using System;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        private readonly ApplicationDbContext _db;

        public CustomerRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Customer> Update(Customer obj, Guid customerId)
        {
            var customer = await FirstOrDefault(c => c.Id.Equals(customerId), includeProperties:"Address");
            if (customer != null)
            {
                customer.Name = obj.Name;
                customer.Description = obj.Description;
                customer.PhoneNumber = obj.PhoneNumber;
                customer.Address.PhysicalAddress = obj.Address.PhysicalAddress;
                customer.Address.PostalAddress = obj.Address.PostalAddress;

                await _db.SaveChangesAsync();
                return customer;
            }
            return null;
        }
    }
}
