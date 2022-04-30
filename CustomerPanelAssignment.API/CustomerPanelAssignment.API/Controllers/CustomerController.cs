using AutoMapper;
using CustomerPanelAssignment.API.Models.DomainModels;
using API = CustomerPanelAssignment.API.Models;
using DataAccess.Repositories.IRepository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace CustomerPanelAssignment.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _customerRepo;
        private readonly IMapper _mapper;
        private readonly IImageRepository _imageRepo;

        public CustomerController(ICustomerRepository customerRepo, IMapper mapper, IImageRepository imageRepo)
        {
            _customerRepo = customerRepo;
            _mapper = mapper;
            _imageRepo = imageRepo;
        }

        [HttpPost("AddCustomer")]
        public async Task<IActionResult> PostAddCustomer([FromBody] UpdateCustomerRequest request)
        {
            var exist = await _customerRepo.FirstOrDefault(c => c.Name.Equals(request.Name));

            if (exist == null)
            {
                var customer = await _customerRepo.Add(_mapper.Map<API.Models.Customer>(request));
                _customerRepo.Save();
                return CreatedAtAction(
                    nameof(GetCustomer),
                    new { customerId = customer.Id },
                    _mapper.Map<Customer>(customer));
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var customers = await _customerRepo.GetAll(includeProperties: "Address");

            return Ok(_mapper.Map<List<Customer>>(customers));
        }

        [HttpGet("GetCustomer/{customerId:guid}")]
        [ActionName("GetCustomer")]
        public async Task<IActionResult> GetCustomer([FromRoute] Guid customerId)
        {
            var customer = await _customerRepo.Find(customerId);

            if (customer == null)
                return NotFound();

            return Ok(_mapper.Map<Customer>(customer));
        }

        [HttpPut("UpdateCustomer/{customerId:guid}")]
        public async Task<IActionResult> UpdateCustomer([FromRoute] Guid customerId, [FromBody] UpdateCustomerRequest request)
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

        [HttpDelete("DeleteCustomer/{customerId:guid}")]
        public async Task<IActionResult> DeleteCustomer([FromRoute] Guid customerId)
        {
            var exist = await _customerRepo.Find(customerId);

            if (exist != null)
            {
                _customerRepo.Remove(exist);
                _customerRepo.Save();
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost("UploadImage/{customerId:guid}")]
        public async Task<IActionResult> UploadImage([FromRoute] Guid customerId, IFormFile image)
        {
            var validExtension = new List<String>
            {
                ".jpeg",
                ".png",
                ".gif",
                ".jpg"
            };
            if (image != null && image.Length > 0)
            {
                var extension = Path.GetExtension(image.FileName);
                if (validExtension.Contains(extension))
                {
                    var exist = await _customerRepo.Find(customerId);
                    if (exist != null)
                    {
                        var fileName = Guid.NewGuid() + Path.GetExtension(image.FileName);
                        var fileImagePath = await _imageRepo.Upload(image, fileName);
                        if (await _customerRepo.UpdateImage(customerId, fileImagePath))
                            return Ok(fileImagePath);

                        return StatusCode(StatusCodes.Status500InternalServerError, "Error Resimin Guncellenmesi");
                    }
                }
                return BadRequest("Image Format UYGUN DEGIL");
            }

            return NotFound();

        }
    }
}
