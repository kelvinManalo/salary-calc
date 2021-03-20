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
    public class ContractualEmployeeService : IComputeSalaryService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public ContractualEmployeeService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ServiceResponse<List<GetEmployeeListDto>>> AddEmployee(AddEmployeeDto newEmployee)
        {
            ServiceResponse<List<GetEmployeeListDto>> serviceResponse = new ServiceResponse<List<GetEmployeeListDto>>();

           try{
                var _computeEmployee = newEmployee as AddContractualEmployeeDto;

                Employee employee = new Employee {
                        name = newEmployee.name,
                        birthdate = newEmployee.birthdate,
                        tin = newEmployee.tin,
                        employeeType = newEmployee.employeeType,
                        computedSalary = 0,
                        completed = 0
                };

                ContractualEmployee contractualEmployee = new ContractualEmployee{
                    Employee = employee,
                    ratePerDay = _computeEmployee.ratePerDay,
                    workedDays = _computeEmployee.workedDays
                };

                await _context.ContractualEmployees.AddAsync(contractualEmployee);
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
                var _computeEmployee = computeEmployee as ContractualComputeSalaryDto;                

                ContractualEmployee contractualEmployee = await _context.ContractualEmployees.Include(employee => employee.Employee).FirstOrDefaultAsync(employee => employee.Id == _computeEmployee.Id);

                contractualEmployee.workedDays = _computeEmployee.workedDays;
                contractualEmployee.ratePerDay = _computeEmployee.ratePerDay;
                contractualEmployee.Employee.computedSalary = ComputeSalaryContractual(_computeEmployee.ratePerDay, _computeEmployee.workedDays);
                contractualEmployee.Employee.completed = 1;

                _context.ContractualEmployees.Update(contractualEmployee);
                await _context.SaveChangesAsync();

                ContractualEmployee dbCEmployee = await _context.ContractualEmployees.Include(employee => employee.Employee).FirstOrDefaultAsync(employee => employee.Id == _computeEmployee.Id);

                 serviceResponse.Data = _mapper.Map<ContractualGetEmployeeDto>(dbCEmployee);


            }catch(Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        private decimal ComputeSalaryContractual(decimal ratePerDay, decimal workedDays)
        {
            decimal computedSalary;            
            computedSalary = ratePerDay * workedDays;

            return Math.Round(computedSalary,2);
        }
    }
}