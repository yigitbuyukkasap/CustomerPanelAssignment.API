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
    [Route("[controller]")]
    public class AddressController : Controller
    {
        private readonly IAddressRepository _addressRepo;
        private readonly IMapper _mapper;

        public AddressController(IAddressRepository addressRepo, IMapper mapper)
        {
            _addressRepo = addressRepo;
            _mapper = mapper;
        }

        [HttpGet("GetAddress/{customerId:guid}")]
        public async Task<IActionResult> GetAddress([FromRoute] Guid customerId)
        {
            var address = await _addressRepo.FirstOrDefault(a => a.CustomerId.Equals(customerId));
            if (address == null)
                return NotFound();

            return Ok(_mapper.Map<Address>(address));
        }
    }
}
