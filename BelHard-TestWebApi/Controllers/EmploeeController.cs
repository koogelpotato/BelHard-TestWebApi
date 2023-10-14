using AutoMapper;
using BelHard_TestWebApi.DTO;
using BelHard_TestWebApi.Interfaces;
using BelHard_TestWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BelHard_TestWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        public EmployeeController(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        [HttpGet("{employeeId}")]
        [ProducesResponseType(200, Type = typeof(Employee))]
        [ProducesResponseType(400)]
        [SwaggerOperation(Summary = "Retrieves an employee by specified Id")]
        public async Task<IActionResult> GetEmployeeById(int employeeId)
        {
            if (!await _employeeRepository.EmployeeExists(employeeId))
                return NotFound();

            var employee = await _employeeRepository.GetEmployeeById(employeeId);
            var employeeDTO = _mapper.Map<EmployeeDTO>(employee);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(employeeDTO);
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Employee>))]
        [SwaggerOperation(Summary = "Retrieves a list of employees")]
        public async Task<IActionResult> GetEmployees()
        {
            var employees = await _employeeRepository.GetEmployees();
            var employeesDTO = _mapper.Map<List<EmployeeDTO>>(employees);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(employeesDTO);
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [SwaggerOperation(Summary = "Creates a new employee")]
        public async Task<IActionResult> CreateEmployee([FromBody] EmployeeDTO employeeCreate)
        {
            if (employeeCreate == null)
                return BadRequest(ModelState);

            var employees = await _employeeRepository.GetEmployees();
            var employee = employees.Where(e => e.FirstName.Trim().ToUpper() == employeeCreate.FirstName.TrimEnd().ToUpper())
                .Where(e => e.MiddleName.Trim().ToUpper() == employeeCreate.MiddleName.TrimEnd().ToUpper())
                .Where(e => e.LastName.Trim().ToUpper() == employeeCreate.LastName.TrimEnd().ToUpper()).FirstOrDefault();

            if(employee != null)
            {
                ModelState.AddModelError("", "Employee already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var employeeMap = _mapper.Map<Employee>(employeeCreate);

            if (!await _employeeRepository.CreateEmployee(employeeMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Employee succesfully created!");
        }

        [HttpPut("{employeeId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [SwaggerOperation(Summary = "Updates an employee by a certain Id")]
        public async Task<IActionResult> UpdateEmployee(int employeeId, [FromBody] EmployeeDTO employeeUpdate)
        {
            if (employeeUpdate == null)
                return BadRequest(ModelState);

            if(employeeId != employeeUpdate.Id)
                return BadRequest(ModelState);

            if(!await _employeeRepository.EmployeeExists(employeeId))
                return NotFound();

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var employeeMap = _mapper.Map<Employee>(employeeUpdate);

            if(! await _employeeRepository.UpdateEmployee(employeeMap))
            {
                ModelState.AddModelError("", "Something went wrong updating emploee");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
        [HttpDelete("{employeeId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [SwaggerOperation(Summary = "Deletes an employee by a certain Id")]
        public async Task<IActionResult> DeleteEmployee(int employeeId)
        {
            if(! await _employeeRepository.EmployeeExists(employeeId))
            {
                return NotFound();
            }

            var employeeToDelete = await _employeeRepository.GetEmployeeById(employeeId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if(! await _employeeRepository.DeleteEmployee(employeeToDelete))
            {
                ModelState.AddModelError("", "Something went wrong when deleting emploee");
            }

            return NoContent(); 
        }
    }
}
