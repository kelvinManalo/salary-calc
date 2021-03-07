using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;

using salary_calc_api.Dtos;
using salary_calc_api.Models;
using salary_calc_api.Data;
using System.Linq;
using System.Collections.Generic;

namespace salary_calc_api.Services.EmployeeService
{
    public class RegularEmployeeService : EmployeeService
    {
       
        private static int WORKDAYS = 22;
        private static decimal TAX = 0.12M;
        public RegularEmployeeService(IMapper mapper, DataContext context) : base(mapper, context)
        {}

        public override async Task<ServiceResponse<List<GetEmployeeListDto>>> ComputeSalaryEmployee(ComputeSalaryEmployeeDto computeEmployee)
        {
            ServiceResponse<List<GetEmployeeListDto>> serviceResponse = new ServiceResponse<List<GetEmployeeListDto>>();

            try 
            { 
                Employee employee = await _context.Employees.FirstOrDefaultAsync(employee => employee.employeeId == computeEmployee.employeeId && employee.employeeType == computeEmployee.employeeType);

                employee.effectiveDays = computeEmployee.effectiveDays;
                employee.computedSalary = ComputeSalaryRegular(employee.baseSalary, computeEmployee.effectiveDays);
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

        private decimal ComputeSalaryRegular(decimal salary, decimal daysAbsent)
        {
            decimal taxDeduction;
            decimal deductionDueToAbsences;
            decimal computedSalary;

            taxDeduction = salary * TAX;
            deductionDueToAbsences = daysAbsent * (salary / WORKDAYS);
            computedSalary = salary - deductionDueToAbsences - taxDeduction;

            return Math.Round(computedSalary,2);
        }
    }
}