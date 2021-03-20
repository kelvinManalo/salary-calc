using System;
using salary_calc_api.Models;

namespace salary_calc_api.Dtos
{
    public class GetEmployeeDto
    {
        public Employee Employee {get;set;}
        public int EmployeeId { get; set; } 
    }
    public class RegularGetEmployeeDto : GetEmployeeDto
    {
        
        public decimal baseSalary {get;set;}
        public decimal daysAbsent {get;set;} 
        
    }

    public class ContractualGetEmployeeDto : GetEmployeeDto
    {
        
        public decimal ratePerDay {get;set;}
        public decimal workedDays {get;set;} 

    }
}