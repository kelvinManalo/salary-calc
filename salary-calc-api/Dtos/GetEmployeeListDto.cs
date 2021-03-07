using System;
using salary_calc_api.Models;

namespace salary_calc_api.Dtos
{
    public class GetEmployeeListDto
    {
        public int employeeId {get;set;}
        public string name {get;set;}
        public DateTime birthdate {get;set;}
        public string tin {get;set;}
        public EmployeeType employeeType {get;set;}
        public decimal baseSalary {get;set;}
        public decimal effectiveDays {get;set;}
        public decimal computedSalary {get;set;}
        public int completed {get;set;} = 0;
        
    }
}