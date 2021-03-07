using System;
namespace salary_calc_api.Models
{
    public class Employee
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