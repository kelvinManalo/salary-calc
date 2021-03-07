using System;
using System.ComponentModel.DataAnnotations;
using salary_calc_api.Models;
using salary_calc_api.Validations;

namespace salary_calc_api.Dtos
{
    public class ComputeSalaryEmployeeDto
    {
        public int employeeId {get;set;}
        [Required]
        [CustomValidation(typeof(ValidationMethods), "FieldIsDefinedEmployeeTypeEnum")]
        public EmployeeType employeeType {get;set;}
        [CustomValidation(typeof(ValidationMethods), "FieldGreaterOrEqualToZero")]
        public decimal effectiveDays {get;set;}
        public int completed {get;set;}

    }
}