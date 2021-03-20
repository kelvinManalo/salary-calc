using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;

using salary_calc_api.Dtos;
using salary_calc_api.Models;
using salary_calc_api.Data;


namespace salary_calc_api.Services.EmployeeService
{
    public class EmployeeService : IEmployeeService
    {
        public readonly IMapper _mapper;
        public readonly DataContext _context;

        public EmployeeService(IMapper mapper, DataContext context){
            
            _mapper = mapper;
            _context = context;
        }
        

        public async Task<ServiceResponse<List<GetEmployeeListDto>>> GetAllEmployees()
        {
            ServiceResponse<List<GetEmployeeListDto>> serviceResponse = new ServiceResponse<List<GetEmployeeListDto>>();

            //fetch all data
            List<Employee> dbEmployeeList = await _context.Employees.ToListAsync();
            serviceResponse.Data = dbEmployeeList.Select(employee => _mapper.Map<GetEmployeeListDto>(employee)).ToList();

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetEmployeeDto>> GetEmployeeById(int Id)
        {
            ServiceResponse<GetEmployeeDto> serviceResponse = new ServiceResponse<GetEmployeeDto>();

            //fetch single data
            Employee dbEmployee = await _context.Employees.FirstOrDefaultAsync(employee => employee.Id == Id);
            serviceResponse.Data = _mapper.Map<GetEmployeeDto>(dbEmployee);

            return serviceResponse;
        }
    }
}