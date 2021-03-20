using AutoMapper;
using salary_calc_api.Models;
using salary_calc_api.Dtos;
namespace salary_calc_api
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Employee,GetEmployeeDto>();
            CreateMap<Employee,GetEmployeeListDto>();
            CreateMap<Employee,ComputeSalaryEmployeeDto>();
            CreateMap<AddEmployeeDto,Employee>();
            CreateMap<Employee,ContractualEmployee>();
            CreateMap<Employee,RegularEmployee>();
            CreateMap<ContractualEmployee,ContractualGetEmployeeDto>();
            
        }
    }
}