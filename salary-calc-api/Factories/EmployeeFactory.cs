using salary_calc_api.Services.EmployeeService;
using salary_calc_api.Models;
using AutoMapper;
using salary_calc_api.Data;

namespace salary_calc_api.Factories
{
    public class EmployeeFactory : IEmployeeFactory
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        public EmployeeFactory(IMapper mapper, DataContext context)
        {
             _mapper = mapper;
            _context = context;
        }

        public IEmployeeService GetEmployeeService(EmployeeType employeeType = EmployeeType.None)
        {
            switch (employeeType)
            {
                case EmployeeType.Contractual:
                    return new ContractualEmployeeService(_mapper,_context);
                case EmployeeType.Regular:
                    return new RegularEmployeeService(_mapper,_context);
                default:
                    return new EmployeeService(_mapper,_context);
            }
        }
    }
}