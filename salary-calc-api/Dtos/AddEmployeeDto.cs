using System;
using salary_calc_api.Models;
using salary_calc_api.Validations;


using System.ComponentModel.DataAnnotations;
namespace salary_calc_api.Dtos
{
    public class AddEmployeeDto
    {
        [Required]
        public string name {get;set;}
        
        [CustomValidation(typeof(ValidationMethods), "FieldFutureDated")]
        public DateTime birthdate {get;set;}

        public string tin {get;set;}

        [Required]
        [CustomValidation(typeof(ValidationMethods), "FieldIsDefinedEmployeeTypeEnum")]
        public EmployeeType employeeType {get;set;}

        [Required]
        [CustomValidation(typeof(ValidationMethods), "FieldGreaterOrEqualToZero")]
        public decimal baseSalary {get;set;}

    }
}