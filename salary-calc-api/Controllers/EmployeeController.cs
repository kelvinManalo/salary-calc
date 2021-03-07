using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;

using salary_calc_api.Factories;
using salary_calc_api.Models;
using salary_calc_api.Dtos;
using salary_calc_api.Services.EmployeeService;




namespace salary_calc_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors("AllowAllHeaders")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeFactory _employeeFactory;

        public EmployeeController(IEmployeeFactory employeeFactory){
            _employeeFactory = employeeFactory;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees(){
            IEmployeeService _employeeService = _employeeFactory.GetEmployeeService();
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
            IEmployeeService _employeeService = _employeeFactory.GetEmployeeService();
            ServiceResponse<GetEmployeeDto> response = await _employeeService.GetEmployeeById(id);
            
            if (response.Data == null)
            {
                return NotFound(response);
            }
            
            return Ok(response);
        }

        //Add
        [HttpPost]
        public async Task<IActionResult> AddEmployee(AddEmployeeDto newEmployee)
        {

            
            IEmployeeService _employeeService = _employeeFactory.GetEmployeeService();
            ServiceResponse<List<GetEmployeeListDto>> response = await _employeeService.AddEmployee(newEmployee);
            
            if (response.Data == null)
            {
                return NotFound(response);
            }
            
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ComputeEmployeeSalary(ComputeSalaryEmployeeDto computeEmployee)
        {
            IEmployeeService EmployeeService = _employeeFactory.GetEmployeeService(computeEmployee.employeeType);
            ServiceResponse<List<GetEmployeeListDto>> response = await EmployeeService.ComputeSalaryEmployee(computeEmployee);

             if (response.Data == null)
            {
                return NotFound(response);
            }
            
            return Ok(response);
        }
        
    }
}