using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;
using salary_calc_api.Models;

namespace salary_calc_api.Validations
{
    public class ValidationMethods
    {
        public static ValidationResult FieldGreaterOrEqualToZero(decimal value, ValidationContext context)
        {
            bool isValid = true;

            if (value < decimal.Zero)
            {
                isValid = false;
            }

            if (isValid)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(
                    string.Format("The field {0} must be greater than or equal to 0.", context.MemberName),
                    new List<string>() { context.MemberName });
            }
        }

        public static ValidationResult FieldFutureDated(DateTime value, ValidationContext context)
        {
            bool isValid = true;

            if (value > DateTime.Now.Date)
            {
                isValid = false;
            }

            if (isValid)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(
                    string.Format("The field {0} cannot be future dated.", context.MemberName),
                    new List<string>() { context.MemberName });
            }
        }

        public static ValidationResult FieldIsDefinedEmployeeTypeEnum(EmployeeType value, ValidationContext context)
        {
            bool isValid = true;

            if (!Enum.IsDefined(value))
            {
                isValid = false;
            }

            if (isValid)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(
                    string.Format("The field {0} is not defined in the enum.", context.MemberName),
                    new List<string>() { context.MemberName });
            }
        }
    }
}