using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using salary_calc_api.Models;
using salary_calc_api.Validations;
using salary_calc_api.JsonConverters;

namespace salary_calc_api.Dtos
{
    [JsonConverter(typeof(EmployeeJsonConverter))]
    public class ComputeSalaryEmployeeDto
    {
        public int Id {get;set;}
        [Required]
        [CustomValidation(typeof(ValidationMethods), "FieldIsDefinedEmployeeTypeEnum")]
        public EmployeeType employeeType {get;set;}
        [CustomValidation(typeof(ValidationMethods), "FieldGreaterOrEqualToZero")]
        public int completed {get;set;}
    }

    public class RegularComputeSalaryDto : ComputeSalaryEmployeeDto
    {
        public decimal baseSalary {get;set;}
        public int daysAbsent {get;set;}
    }

    public class ContractualComputeSalaryDto : ComputeSalaryEmployeeDto
    {
        public decimal ratePerDay {get;set;}
        public int workedDays {get;set;}
    }
}