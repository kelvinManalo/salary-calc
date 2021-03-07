using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

using salary_calc_api.Dtos;
using salary_calc_api.Models;
using salary_calc_api.Data;
using System.Linq;

namespace salary_calc_api.Services.EmployeeService
{
    public class ContractualEmployeeService : EmployeeService
    {
        public ContractualEmployeeService(IMapper mapper, DataContext context) : base(mapper, context)
        {
        }

        public override async Task<ServiceResponse<List<GetEmployeeListDto>>> ComputeSalaryEmployee(ComputeSalaryEmployeeDto computeEmployee)
        {
            ServiceResponse<List<GetEmployeeListDto>> serviceResponse = new ServiceResponse<List<GetEmployeeListDto>>();

            try 
            { 
                Employee employee = await _context.Employees.FirstOrDefaultAsync(employee => employee.employeeId == computeEmployee.employeeId && employee.employeeType == computeEmployee.employeeType);

                employee.effectiveDays = computeEmployee.effectiveDays;
                employee.computedSalary = ComputeSalaryContractual(employee.baseSalary, computeEmployee.effectiveDays);
                employee.completed = computeEmployee.completed;

                _context.Employees.Update(employee);
                await _context.SaveChangesAsync();

                serviceResponse.Data = await _context.Employees.Select(emp => _mapper.Map<GetEmployeeListDto>(emp)).ToListAsync();

            }catch(Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        private decimal ComputeSalaryContractual(decimal salary, decimal daysReported)
        {
            decimal computedSalary;
            
            computedSalary = salary * daysReported;

            return Math.Round(computedSalary,2);
        }
    }
}