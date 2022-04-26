using CustomerPanelAssignment.API.Models;
using DataAccess.Data;
using DataAccess.Repositories.IRepository;
using System;

namespace DataAccess.Repositories
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        private readonly ApplicationDbContext _db;

        public CustomerRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Customer obj)
        {
            _db.Customer.Update(obj);
        }
    }
}
