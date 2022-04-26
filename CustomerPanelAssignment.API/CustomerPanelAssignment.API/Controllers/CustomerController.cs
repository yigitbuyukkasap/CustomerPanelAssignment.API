using CustomerPanelAssignment.API.Models.DataModels;
using DataAccess.Repositories.IRepository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerPanelAssignment.API.Controllers
{
    [ApiController]
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _customerRepo;

        public CustomerController(ICustomerRepository customerRepo)
        {
            _customerRepo = customerRepo;
        }

        [HttpGet]
        [Route("[controller]")]
        public IActionResult GetAllCustomers()
        {
            var customers = _customerRepo.GetAll(includeProperties:"Address");

            //Populating Domain Models dm - DomainModal
            var dmCustomers = new List<Customer>();
            //dmCustomers = customers;

            return Ok(customers);
        }
    }
}
