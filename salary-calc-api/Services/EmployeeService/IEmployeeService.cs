using System.Threading.Tasks;
using System.Collections.Generic;
using salary_calc_api.Models;
using salary_calc_api.Dtos;

namespace salary_calc_api.Services.EmployeeService
{
    public interface IEmployeeService
    {
         Task<ServiceResponse<List<GetEmployeeListDto>>> GetAllEmployees();
         Task<ServiceResponse<GetEmployeeDto>> GetEmployeeById(int id);
         
    }
}