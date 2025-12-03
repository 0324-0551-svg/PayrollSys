using PayrollSystem.Models;
using System;
using System.Data;
using System.Data.SqlClient;

namespace PayrollSystem.DataAccess
{
    public class PayrollPeriodRepository
    {
        public PayrollPeriod? GetCurrentPayrollPeriod()
        {
            string sql = @"SELECT PayrollPeriodID, StartDate, EndDate, IsProcessed 
                           FROM PayrollPeriod 
                           WHERE StartDate <= @Today AND EndDate >= @Today";
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@Today", DateTime.Today)
            };
            var dt = DatabaseHelper.ExecuteQuery(sql, parameters);
            if (dt.Rows.Count == 0)
                return null;

            var row = dt.Rows[0];
            return new PayrollPeriod
            {
                PayrollPeriodID = Convert.ToInt32(row["PayrollPeriodID"]),
                StartDate = Convert.ToDateTime(row["StartDate"]),
                EndDate = Convert.ToDateTime(row["EndDate"]),
                IsProcessed = Convert.ToBoolean(row["IsProcessed"])
            };
        }

        public bool AddPayrollPeriod(PayrollPeriod period)
        {
            string sql = @"INSERT INTO PayrollPeriod (StartDate, EndDate, IsProcessed)
                           VALUES (@StartDate, @EndDate, @IsProcessed)";
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@StartDate", period.StartDate),
                new SqlParameter("@EndDate", period.EndDate),
                new SqlParameter("@IsProcessed", period.IsProcessed)
            };
            int rowsAffected = DatabaseHelper.ExecuteNonQuery(sql, parameters);
            return rowsAffected > 0;
        }

        public bool MarkAsProcessed(int payrollPeriodId)
        {
            string sql = @"UPDATE PayrollPeriod SET IsProcessed = 1 WHERE PayrollPeriodID = @PayrollPeriodID";
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@PayrollPeriodID", payrollPeriodId)
            };
            int rowsAffected = DatabaseHelper.ExecuteNonQuery(sql, parameters);
            return rowsAffected > 0;
        }
    }
}
