using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using salary_calc_api.Data;
using salary_calc_api.Dtos;
using salary_calc_api.Models;

namespace salary_calc_api.Services.EmployeeService
{
    public class RegularEmployeeService : IComputeSalaryService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        private static int WORKDAYS = 22;
        private static decimal TAX = 0.12M;

        public RegularEmployeeService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;            
        }

        public async Task<ServiceResponse<List<GetEmployeeListDto>>> AddEmployee(AddEmployeeDto newEmployee)
        {
            ServiceResponse<List<GetEmployeeListDto>> serviceResponse = new ServiceResponse<List<GetEmployeeListDto>>();

           try{
                var _computeEmployee = newEmployee as AddRegularEmployeeDto;

                Employee employee = new Employee {
                        name = newEmployee.name,
                        birthdate = newEmployee.birthdate,
                        tin = newEmployee.tin,
                        employeeType = newEmployee.employeeType,
                        computedSalary = 0,
                        completed = 0
                };

                RegularEmployee regularEmployee = new RegularEmployee{
                    Employee = employee,
                    baseSalary = _computeEmployee.baseSalary,
                    daysAbsent = _computeEmployee.daysAbsent
                };

                await _context.RegularEmployees.AddAsync(regularEmployee);
                await _context.SaveChangesAsync();

                serviceResponse.Data = await _context.Employees.Select(employee => _mapper.Map<GetEmployeeListDto>(employee)).ToListAsync();
            } catch (Exception ex) {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
           
           return serviceResponse; 
        }

        public async Task<ServiceResponse<GetEmployeeDto>> CalculateSalary(ComputeSalaryEmployeeDto computeEmployee)
        {
            ServiceResponse<GetEmployeeDto> serviceResponse = new ServiceResponse<GetEmployeeDto>();

            try 
            {
                var _computeEmployee = computeEmployee as RegularComputeSalaryDto;                

                RegularEmployee regularEmployee = await _context.RegularEmployees.Include(employee => employee.Employee).FirstOrDefaultAsync(employee => employee.Id == _computeEmployee.Id);

                regularEmployee.daysAbsent = _computeEmployee.daysAbsent;
                regularEmployee.baseSalary = _computeEmployee.baseSalary;
                regularEmployee.Employee.computedSalary = ComputeSalaryRegular(_computeEmployee.daysAbsent, _computeEmployee.baseSalary);
                regularEmployee.Employee.completed = 1;

                _context.RegularEmployees.Update(regularEmployee);
                await _context.SaveChangesAsync();

                RegularEmployee dbREmployee = await _context.RegularEmployees.Include(employee => employee.Employee).FirstOrDefaultAsync(employee => employee.Id == _computeEmployee.Id);

                serviceResponse.Data = _mapper.Map<RegularGetEmployeeDto>(dbREmployee);


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