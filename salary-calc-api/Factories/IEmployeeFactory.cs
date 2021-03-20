using salary_calc_api.Models;
using salary_calc_api.Services.EmployeeService;

namespace salary_calc_api.Factories
{
    public interface IEmployeeFactory
    {
         IComputeSalaryService GetEmployeeService(EmployeeType employeeType = EmployeeType.None);
    }
}