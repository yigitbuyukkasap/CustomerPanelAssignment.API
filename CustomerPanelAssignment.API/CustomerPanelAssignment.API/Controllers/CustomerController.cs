using AutoMapper;
using CustomerPanelAssignment.API.Models.DomainModels;
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
        private readonly IMapper _mapper;

        public CustomerController(ICustomerRepository customerRepo, IMapper mapper)
        {
            _customerRepo = customerRepo;
            _mapper = mapper;

        }

        [HttpGet]
        [Route("[controller]")]
        public async Task<IActionResult> GetAllCustomers()
        {
            var customers = await _customerRepo.GetAll(includeProperties: "Address");
            return Ok(_mapper.Map<List<Customer>>(customers));
        }
    }
}
