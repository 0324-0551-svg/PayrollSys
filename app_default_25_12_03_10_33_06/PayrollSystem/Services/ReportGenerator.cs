using PayrollSystem.DataAccess;
using PayrollSystem.Models;
using PayrollSystem.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace PayrollSystem.Services
{
    public class ReportGenerator
    {
        private readonly EmployeeRepository _employeeRepo = new();
        private readonly PayrollRepository _payrollRepo = new();
        private readonly PayrollPeriodRepository _payrollPeriodRepo = new();

        public string GeneratePayslip(int employeeId, int payrollPeriodId)
        {
            var employee = _employeeRepo.GetEmployeeByID(employeeId);
            if (employee == null)
                throw new ArgumentException("Employee not found.");

            var payrolls = _payrollRepo.GetPayrollByPeriod(payrollPeriodId);
            Payroll? payroll = payrolls.Find(p => p.EmployeeID == employeeId);

            if (payroll == null)
                throw new ArgumentException("Payroll record not found for employee and period.");

            var payrollPeriod = _payrollPeriodRepo.GetCurrentPayrollPeriod();
            if (payrollPeriod == null)
                throw new ArgumentException("Payroll period not found.");

            var payslipData = new
            {
                EmployeeName = $"{employee.FirstName} {employee.LastName}",
                Position = employee.Position,
                Department = employee.Department,
                PayrollPeriod = $"{payrollPeriod.StartDate:yyyy-MM-dd} to {payrollPeriod.EndDate:yyyy-MM-dd}",
                GrossPay = payroll.GrossPay,
                Deductions = payroll.Deductions,
                NetPay = payroll.NetPay,
                PaymentDate = payroll.PaymentDate.ToShortDateString()
            };

            string fileName = $"Payslip_{employee.EmployeeID}_{payrollPeriodId}_{DateTime.Now:yyyyMMddHHmmss}.pdf";
            string filePath = Path.Combine(Path.GetTempPath(), fileName);

            ExportHelper.ExportToPDF(payslipData, filePath);

            // Open the generated file automatically
            Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });

            return filePath;
        }

        public string GenerateGovernmentReport(int payrollPeriodId)
        {
            var payrolls = _payrollRepo.GetPayrollByPeriod(payrollPeriodId);
            if (payrolls.Count == 0)
                throw new ArgumentException("No payroll records found for the given payroll period.");

            string fileName = $"GovernmentReport_{payrollPeriodId}_{DateTime.Now:yyyyMMddHHmmss}.xlsx";
            string filePath = Path.Combine(Path.GetTempPath(), fileName);

            ExportHelper.ExportToExcel(payrolls, filePath);

            // Open the generated file automatically
            Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });

            return filePath;
        }
    }
}
