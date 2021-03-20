using System;
using Newtonsoft.Json.Linq;
using salary_calc_api.Dtos;

namespace salary_calc_api.JsonConverters
{
    public class EmployeeJsonConverter : JsonCreationConverter<ComputeSalaryEmployeeDto>
    {
        protected override ComputeSalaryEmployeeDto Create(Type objectType, JObject jObject)
        {
            if (jObject == null) throw new ArgumentNullException("jObject");

            if (jObject["ratePerDay"] != null)
            {
            return new ContractualComputeSalaryDto();
            }
            else if (jObject["baseSalary"] != null)
            {
            return new RegularComputeSalaryDto();
            }
            else
            {
            return null;
            }
        }
    }
}