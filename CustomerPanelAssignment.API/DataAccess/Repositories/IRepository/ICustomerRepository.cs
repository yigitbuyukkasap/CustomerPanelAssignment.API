using CustomerPanelAssignment.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.IRepository
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<Customer> Update(Customer obj, Guid customerId);
    }
}
