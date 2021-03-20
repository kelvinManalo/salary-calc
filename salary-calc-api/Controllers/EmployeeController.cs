using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;

using salary_calc_api.Models;
using salary_calc_api.Dtos;
using salary_calc_api.Services.EmployeeService;
using salary_calc_api.Factories;

namespace salary_calc_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors("AllowAllHeaders")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IEmployeeFactory _employeeFactory;

        public EmployeeController(IEmployeeService employeeService, IEmployeeFactory employeeFactory){
            _employeeService = employeeService;
            _employeeFactory = employeeFactory;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees(){            
            ServiceResponse<List<GetEmployeeListDto>> response = await _employeeService.GetAllEmployees();
            
            if (response.Data == null)
            {
                return NotFound(response);
            }
            
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            ServiceResponse<GetEmployeeDto> response = await _employeeService.GetEmployeeById(id);
            
            if (response.Data == null)
            {
                return NotFound(response);
            }
            
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ComputeEmployeeSalary(ComputeSalaryEmployeeDto computeEmployee)
        {
            IComputeSalaryService EmployeeService = _employeeFactory.GetEmployeeService(computeEmployee.employeeType);
            ServiceResponse<GetEmployeeDto> response = await EmployeeService.CalculateSalary(computeEmployee);

             if (response.Data == null)
            {
                return NotFound(response);
            }
            
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee(AddEmployeeDto newEmployee)
        {
            IComputeSalaryService EmployeeService = _employeeFactory.GetEmployeeService(newEmployee.employeeType);
            ServiceResponse<List<GetEmployeeListDto>> response = await EmployeeService.AddEmployee(newEmployee);

             if (response.Data == null)
            {
                return NotFound(response);
            }
            
            return Ok(response);
        }   
             
        
    }
}