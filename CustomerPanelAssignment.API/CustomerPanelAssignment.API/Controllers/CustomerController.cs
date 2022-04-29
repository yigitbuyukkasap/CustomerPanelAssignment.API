using AutoMapper;
using CustomerPanelAssignment.API.Models.DomainModels;
using API = CustomerPanelAssignment.API.Models;
using DataAccess.Repositories.IRepository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerPanelAssignment.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _customerRepo;
        private readonly IMapper _mapper;

        public CustomerController(ICustomerRepository customerRepo, IMapper mapper)
        {
            _customerRepo = customerRepo;
            _mapper = mapper;

        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            //int id = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);

            var customers = await _customerRepo.GetAll(includeProperties: "Address");

            return Ok(_mapper.Map<List<Customer>>(customers));
        }
    
        [HttpGet("GetCustomer/{customerId:guid}")]
        public async Task<IActionResult> GetCustomer([FromRoute] Guid customerId) 
        {
            var customer = await _customerRepo.Find(customerId);

            if (customer == null)
                return NotFound();

            return Ok(_mapper.Map<Customer>(customer));
        }

        [HttpPut(("UpdateCustomer/{customerId:guid}"))]
        public async Task<IActionResult> UpdateCustomer([FromRoute] Guid customerId, [FromBody] UpdateCustomerRequest request ) 
        {
           var exist = await _customerRepo.Find(customerId);

            if (exist != null)
            {
                var updatedCustomer = await _customerRepo.Update(_mapper.Map<API.Models.Customer>(request), customerId);
                return Ok(_mapper.Map<Customer>(updatedCustomer));
            }
            else 
            {
                return NotFound();
            }

        }
    }
}
