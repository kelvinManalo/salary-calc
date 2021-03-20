using System;
using salary_calc_api.Models;
using salary_calc_api.Validations;


using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using salary_calc_api.JsonConverters;

namespace salary_calc_api.Dtos
{
    [JsonConverter(typeof(AddEmployeeDtoJsonConverter))]
    public class AddEmployeeDto
    {
        [Required]
        public string name {get;set;}
        
        // [CustomValidation(typeof(ValidationMethods), "FieldFutureDated")]
        public DateTime birthdate {get;set;}

        public string tin {get;set;}

        [Required]
        [CustomValidation(typeof(ValidationMethods), "FieldIsDefinedEmployeeTypeEnum")]
        public EmployeeType employeeType {get;set;}

    }

    public class AddContractualEmployeeDto : AddEmployeeDto
    {
        public decimal ratePerDay {get;set;}
        public decimal workedDays {get;set;} 
    }

    public class AddRegularEmployeeDto : AddEmployeeDto
    {
        public decimal baseSalary {get;set;}
        public int daysAbsent {get;set;}
    }
}
