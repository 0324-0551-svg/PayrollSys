using PayrollSystem.DataAccess;
using PayrollSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PayrollSystem.Services
{
    public class PayrollCalculator
    {
        public Payroll CalculatePayroll(Employee emp, List<Attendance> attendances, GovernmentRates rates, int payrollPeriodId)
        {
            if (emp == null)
                throw new ArgumentNullException(nameof(emp));
            if (attendances == null)
                throw new ArgumentNullException(nameof(attendances));

            // Basic assumptions for calculation:
            // - Daily rate = fixed or derived from position (simplified here)
            // - Work days = count of Present or Leave days in the period
            // - Deductions = sum of SSS, PhilHealth, PagIbig contributions based on gross

            decimal dailyRate = GetDailyRate(emp.Position); // Simplified fixed rate
            int workDays = attendances.Count(a => a.Status == AttendanceStatus.Present || a.Status == AttendanceStatus.Leave);
            decimal grossPay = dailyRate * workDays;

            // Calculate deductions percentages of gross pay
            decimal sssDeduction = grossPay * rates.SSSRate / 100m;
            decimal philHealthDeduction = grossPay * rates.PhilHealthRate / 100m;
            decimal pagIbigDeduction = grossPay * rates.PagIbigRate / 100m;
            decimal totalDeductions = sssDeduction + philHealthDeduction + pagIbigDeduction;

            var payroll = new Payroll
            {
                EmployeeID = emp.EmployeeID,
                PayrollPeriodID = payrollPeriodId,
                GrossPay = Math.Round(grossPay, 2),
                Deductions = Math.Round(totalDeductions, 2),
                PaymentDate = DateTime.Now
            };
            payroll.CalculateNetPay();
            return payroll;
        }

        private decimal GetDailyRate(string position)
        {
            // Simplified: in real apps, daily rate is from database or config
            return position.ToLower() switch
            {
                "manager" => 1500m,
                "developer" => 1200m,
                "accountant" => 1000m,
                "clerk" => 800m,
                _ => 700m,
            };
        }
    }
}
