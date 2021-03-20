using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace salary_calc_api.Models
{
  
    public class Employee
    {
        public int Id {get;set;}
        public string name {get;set;}
        public DateTime birthdate {get;set;}
        public string tin {get;set;}
        public EmployeeType employeeType {get;set;}        
        public decimal computedSalary {get;set;}
        public int completed {get;set;} = 0;
    }

    public class ContractualEmployee
    {
        
        public int Id {get;set;}
        public decimal ratePerDay {get;set;}
        public decimal workedDays {get;set;} 
        public Employee Employee {get;set;}
        public int EmployeeId { get; set; } 
    }

   
    public class RegularEmployee
    {
        
        public int Id {get;set;}
        public decimal baseSalary {get;set;}
        public decimal daysAbsent {get;set;} 
        public Employee Employee {get;set;}
        public int EmployeeId { get; set; } 
    } 
}