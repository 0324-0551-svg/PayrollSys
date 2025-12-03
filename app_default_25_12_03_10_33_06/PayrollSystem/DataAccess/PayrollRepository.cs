using PayrollSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace PayrollSystem.DataAccess
{
    public class PayrollRepository
    {
        public List<Payroll> GetPayrollByPeriod(int payrollPeriodId)
        {
            var payrolls = new List<Payroll>();
            string sql = @"SELECT p.PayrollID, p.EmployeeID, p.PayrollPeriodID, p.GrossPay, p.Deductions, p.NetPay, p.PaymentDate,
                                  e.FirstName, e.LastName
                           FROM Payroll p 
                           INNER JOIN Employees e ON p.EmployeeID = e.EmployeeID
                           WHERE p.PayrollPeriodID = @PayrollPeriodID";
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@PayrollPeriodID", payrollPeriodId)
            };
            var dt = DatabaseHelper.ExecuteQuery(sql, parameters);
            foreach (DataRow row in dt.Rows)
            {
                payrolls.Add(new Payroll
                {
                    PayrollID = Convert.ToInt32(row["PayrollID"]),
                    EmployeeID = Convert.ToInt32(row["EmployeeID"]),
                    PayrollPeriodID = Convert.ToInt32(row["PayrollPeriodID"]),
                    GrossPay = Convert.ToDecimal(row["GrossPay"]),
                    Deductions = Convert.ToDecimal(row["Deductions"]),
                    NetPay = Convert.ToDecimal(row["NetPay"]),
                    PaymentDate = Convert.ToDateTime(row["PaymentDate"]),
                    Employee = new Employee
                    {
                        FirstName = row["FirstName"].ToString() ?? string.Empty,
                        LastName = row["LastName"].ToString() ?? string.Empty
                    }
                });
            }
            return payrolls;
        }

        public bool AddPayroll(Payroll payroll)
        {
            string sql = @"INSERT INTO Payroll (EmployeeID, PayrollPeriodID, GrossPay, Deductions, NetPay, PaymentDate)
                           VALUES (@EmployeeID, @PayrollPeriodID, @GrossPay, @Deductions, @NetPay, @PaymentDate)";
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@EmployeeID", payroll.EmployeeID),
                new SqlParameter("@PayrollPeriodID", payroll.PayrollPeriodID),
                new SqlParameter("@GrossPay", payroll.GrossPay),
                new SqlParameter("@Deductions", payroll.Deductions),
                new SqlParameter("@NetPay", payroll.NetPay),
                new SqlParameter("@PaymentDate", payroll.PaymentDate)
            };
            int rowsAffected = DatabaseHelper.ExecuteNonQuery(sql, parameters);
            return rowsAffected > 0;
        }

        public bool UpdatePayroll(Payroll payroll)
        {
            string sql = @"UPDATE Payroll 
                           SET GrossPay = @GrossPay, Deductions = @Deductions, NetPay = @NetPay, PaymentDate = @PaymentDate
                           WHERE PayrollID = @PayrollID";
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@GrossPay", payroll.GrossPay),
                new SqlParameter("@Deductions", payroll.Deductions),
                new SqlParameter("@NetPay", payroll.NetPay),
                new SqlParameter("@PaymentDate", payroll.PaymentDate),
                new SqlParameter("@PayrollID", payroll.PayrollID)
            };
            int rowsAffected = DatabaseHelper.ExecuteNonQuery(sql, parameters);
            return rowsAffected > 0;
        }

        public Payroll? GetPayrollByEmployeeAndPeriod(int employeeId, int payrollPeriodId)
        {
            string sql = @"SELECT PayrollID, EmployeeID, PayrollPeriodID, GrossPay, Deductions, NetPay, PaymentDate
                           FROM Payroll 
                           WHERE EmployeeID = @EmployeeID AND PayrollPeriodID = @PayrollPeriodID";
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@EmployeeID", employeeId),
                new SqlParameter("@PayrollPeriodID", payrollPeriodId)
            };
            var dt = DatabaseHelper.ExecuteQuery(sql, parameters);
            if (dt.Rows.Count == 0)
                return null;

            var row = dt.Rows[0];
            return new Payroll
            {
                PayrollID = Convert.ToInt32(row["PayrollID"]),
                EmployeeID = Convert.ToInt32(row["EmployeeID"]),
                PayrollPeriodID = Convert.ToInt32(row["PayrollPeriodID"]),
                GrossPay = Convert.ToDecimal(row["GrossPay"]),
                Deductions = Convert.ToDecimal(row["Deductions"]),
                NetPay = Convert.ToDecimal(row["NetPay"]),
                PaymentDate = Convert.ToDateTime(row["PaymentDate"])
            };
        }
    }
}
