using PayrollSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace PayrollSystem.DataAccess
{
    public class EmployeeRepository
    {
        public List<Employee> GetAllEmployees()
        {
            var employees = new List<Employee>();
            string sql = "SELECT EmployeeID, FirstName, LastName, MiddleName, Position, Department, DateHired, Status FROM Employees";

            var dt = DatabaseHelper.ExecuteQuery(sql);
            foreach (DataRow row in dt.Rows)
            {
                employees.Add(new Employee
                {
                    EmployeeID = Convert.ToInt32(row["EmployeeID"]),
                    FirstName = row["FirstName"].ToString() ?? string.Empty,
                    LastName = row["LastName"].ToString() ?? string.Empty,
                    MiddleName = row["MiddleName"] as string,
                    Position = row["Position"].ToString() ?? string.Empty,
                    Department = row["Department"].ToString() ?? string.Empty,
                    DateHired = Convert.ToDateTime(row["DateHired"]),
                    Status = (EmployeeStatus)Convert.ToInt32(row["Status"])
                });
            }
            return employees;
        }

        public Employee? GetEmployeeByID(int id)
        {
            string sql = "SELECT EmployeeID, FirstName, LastName, MiddleName, Position, Department, DateHired, Status FROM Employees WHERE EmployeeID = @EmployeeID";
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@EmployeeID", id)
            };
            var dt = DatabaseHelper.ExecuteQuery(sql, parameters);
            if (dt.Rows.Count == 0)
                return null;

            var row = dt.Rows[0];
            return new Employee
            {
                EmployeeID = Convert.ToInt32(row["EmployeeID"]),
                FirstName = row["FirstName"].ToString() ?? string.Empty,
                LastName = row["LastName"].ToString() ?? string.Empty,
                MiddleName = row["MiddleName"] as string,
                Position = row["Position"].ToString() ?? string.Empty,
                Department = row["Department"].ToString() ?? string.Empty,
                DateHired = Convert.ToDateTime(row["DateHired"]),
                Status = (EmployeeStatus)Convert.ToInt32(row["Status"])
            };
        }

        public bool AddEmployee(Employee emp)
        {
            string sql = @"INSERT INTO Employees (FirstName, LastName, MiddleName, Position, Department, DateHired, Status)
                           VALUES (@FirstName, @LastName, @MiddleName, @Position, @Department, @DateHired, @Status)";
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@FirstName", emp.FirstName),
                new SqlParameter("@LastName", emp.LastName),
                new SqlParameter("@MiddleName", (object?)emp.MiddleName ?? DBNull.Value),
                new SqlParameter("@Position", emp.Position),
                new SqlParameter("@Department", emp.Department),
                new SqlParameter("@DateHired", emp.DateHired),
                new SqlParameter("@Status", (int)emp.Status)
            };
            int rowsAffected = DatabaseHelper.ExecuteNonQuery(sql, parameters);
            return rowsAffected > 0;
        }

        public bool UpdateEmployee(Employee emp)
        {
            string sql = @"UPDATE Employees 
                           SET FirstName = @FirstName, LastName = @LastName, MiddleName = @MiddleName,
                               Position = @Position, Department = @Department, DateHired = @DateHired, Status = @Status
                           WHERE EmployeeID = @EmployeeID";
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@FirstName", emp.FirstName),
                new SqlParameter("@LastName", emp.LastName),
                new SqlParameter("@MiddleName", (object?)emp.MiddleName ?? DBNull.Value),
                new SqlParameter("@Position", emp.Position),
                new SqlParameter("@Department", emp.Department),
                new SqlParameter("@DateHired", emp.DateHired),
                new SqlParameter("@Status", (int)emp.Status),
                new SqlParameter("@EmployeeID", emp.EmployeeID)
            };
            int rowsAffected = DatabaseHelper.ExecuteNonQuery(sql, parameters);
            return rowsAffected > 0;
        }

        public bool DeleteEmployee(int id)
        {
            string sql = "DELETE FROM Employees WHERE EmployeeID = @EmployeeID";
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@EmployeeID", id)
            };
            int rowsAffected = DatabaseHelper.ExecuteNonQuery(sql, parameters);
            return rowsAffected > 0;
        }
    }
}
