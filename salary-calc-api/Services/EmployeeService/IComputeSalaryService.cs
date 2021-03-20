using System.Collections.Generic;
using System.Threading.Tasks;
using salary_calc_api.Dtos;
using salary_calc_api.Models;

namespace salary_calc_api.Services.EmployeeService
{
    public interface IComputeSalaryService
    {
        Task<ServiceResponse<GetEmployeeDto>> CalculateSalary(ComputeSalaryEmployeeDto computeEmployee);
        Task<ServiceResponse<List<GetEmployeeListDto>>> AddEmployee(AddEmployeeDto newEmployee);         
    }
}