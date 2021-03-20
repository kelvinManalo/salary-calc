using System;
using Newtonsoft.Json.Linq;
using salary_calc_api.Dtos;

namespace salary_calc_api.JsonConverters
{
    public class AddEmployeeDtoJsonConverter : JsonCreationConverter<AddEmployeeDto>
    {
     
        protected override AddEmployeeDto Create(Type objectType, JObject jObject)
        {
            if (jObject == null) throw new ArgumentNullException("jObject");

            if (jObject["ratePerDay"] != null)
            {
            return new AddContractualEmployeeDto();
            }
            else if (jObject["baseSalary"] != null)
            {
            return new AddRegularEmployeeDto();
            }
            else
            {
            return null;
            }
        }
    
    }
}