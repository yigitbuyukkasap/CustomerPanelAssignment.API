using AutoMapper;
using CustomerPanelAssignment.API.Models.DomainModels;
using DataAccess.Repositories.IRepository;
using Microsoft.AspNetCore.Mvc;
using Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerPanelAssignment.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepo;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeRepository employeeRepo, IMapper mapper)
        {
            _employeeRepo = employeeRepo;
            _mapper = mapper;
        }

        [HttpPost("AddEmployee")]
        public async Task<IActionResult> PostAddEmployee([FromBody] EmployeeRequest request) 
        {
            var exist = await _employeeRepo.FirstOrDefault(e => e.Email.Equals(request.Email));

            if (exist == null)
            {
                var employee = await _employeeRepo.Add(_mapper.Map<API.Models.Employee>(request));
                _employeeRepo.Save();
                return CreatedAtAction(
                    nameof(GetEmployee),
                    new { EmployeeId = employee.Id },
                    _mapper.Map<Employee>(employee));
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("GetEmployee/{employeeId:guid}")]
        [ActionName("GetEmployee")]
        public async Task<IActionResult> GetEmployee([FromRoute] Guid employeeId)
        {
            var employee = await _employeeRepo.Find(employeeId);

            if (employee == null)
                return NotFound();

            return Ok(_mapper.Map<Employee>(employee));
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var employee = await _employeeRepo.GetAll();

            return Ok(_mapper.Map<List<Employee>>(employee));
        }

        [HttpPut("UpdateEmployee/{employeeId:guid}")]
        public async Task<IActionResult> UpdateEmployee([FromRoute] Guid employeeId, [FromBody] EmployeeRequest request)
        {
            var exist = await _employeeRepo.Find(employeeId);

            if (exist != null)
            {
                var updatedEmployee = await _employeeRepo.Update(_mapper.Map<API.Models.Employee>(request), employeeId);
                return Ok(_mapper.Map<Employee>(updatedEmployee));
            }
            else
            {
                return NotFound();
            }

        }

        [HttpDelete("DeleteEmployee/{employeeId:guid}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] Guid employeeId)
        {
            var exist = await _employeeRepo.Find(employeeId);

            if (exist != null)
            {
                _employeeRepo.Remove(exist);
                _employeeRepo.Save();
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

    }
}
