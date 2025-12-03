using PayrollSystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PayrollSystem.Utilities
{
    public static class ValidationHelper
    {
        public static (bool IsValid, List<string> Errors) ValidateEmployee(Employee emp)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(emp, null, null);
            bool isValid = Validator.TryValidateObject(emp, context, results, true);
            var errors = new List<string>();
            foreach (var validationResult in results)
                errors.Add(validationResult.ErrorMessage ?? "Validation error");

            return (isValid, errors);
        }

        public static (bool IsValid, List<string> Errors) ValidateAttendance(Attendance att)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(att, null, null);
            bool isValid = Validator.TryValidateObject(att, context, results, true);
            var errors = new List<string>();
            foreach (var validationResult in results)
                errors.Add(validationResult.ErrorMessage ?? "Validation error");

            if (att.TimeOut <= att.TimeIn)
            {
                errors.Add("TimeOut must be later than TimeIn.");
                isValid = false;
            }

            return (isValid, errors);
        }

        public static (bool IsValid, List<string> Errors) ValidatePayroll(Payroll payroll)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(payroll, null, null);
            bool isValid = Validator.TryValidateObject(payroll, context, results, true);
            var errors = new List<string>();
            foreach (var validationResult in results)
                errors.Add(validationResult.ErrorMessage ?? "Validation error");

            if (payroll.NetPay != payroll.GrossPay - payroll.Deductions)
                errors.Add("NetPay must be equal to GrossPay minus Deductions.");

            return (isValid, errors);
        }
    }
}
