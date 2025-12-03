using PayrollSystem.Models;
using System;
using System.Data;
using System.Data.SqlClient;

namespace PayrollSystem.DataAccess
{
    public class SettingsRepository
    {
        public CompanySettings GetCompanySettings()
        {
            string sql = @"SELECT TOP 1 CompanyName, Address, ContactNumber FROM CompanySettings";
            var dt = DatabaseHelper.ExecuteQuery(sql);
            if (dt.Rows.Count == 0)
                return new CompanySettings();

            var row = dt.Rows[0];
            return new CompanySettings
            {
                CompanyName = row["CompanyName"].ToString() ?? string.Empty,
                Address = row["Address"].ToString() ?? string.Empty,
                ContactNumber = row["ContactNumber"].ToString() ?? string.Empty
            };
        }

        public bool UpdateCompanySettings(CompanySettings settings)
        {
            string checkSql = "SELECT COUNT(*) FROM CompanySettings";
            int count = Convert.ToInt32(DatabaseHelper.ExecuteScalar(checkSql) ?? 0);

            if (count == 0)
            {
                string insertSql = @"INSERT INTO CompanySettings (CompanyName, Address, ContactNumber)
                                     VALUES (@CompanyName, @Address, @ContactNumber)";
                var insertParams = new SqlParameter[]
                {
                    new SqlParameter("@CompanyName", settings.CompanyName),
                    new SqlParameter("@Address", settings.Address),
                    new SqlParameter("@ContactNumber", settings.ContactNumber)
                };
                int rowsInserted = DatabaseHelper.ExecuteNonQuery(insertSql, insertParams);
                return rowsInserted > 0;
            }
            else
            {
                string updateSql = @"UPDATE CompanySettings 
                                     SET CompanyName = @CompanyName, Address = @Address, ContactNumber = @ContactNumber";
                var updateParams = new SqlParameter[]
                {
                    new SqlParameter("@CompanyName", settings.CompanyName),
                    new SqlParameter("@Address", settings.Address),
                    new SqlParameter("@ContactNumber", settings.ContactNumber)
                };
                int rowsUpdated = DatabaseHelper.ExecuteNonQuery(updateSql, updateParams);
                return rowsUpdated > 0;
            }
        }

        public GovernmentRates GetGovernmentRates()
        {
            string sql = @"SELECT TOP 1 SSSRate, PhilHealthRate, PagIbigRate FROM GovernmentRates";
            var dt = DatabaseHelper.ExecuteQuery(sql);
            if (dt.Rows.Count == 0)
                return new GovernmentRates();

            var row = dt.Rows[0];
            return new GovernmentRates
            {
                SSSRate = Convert.ToDecimal(row["SSSRate"]),
                PhilHealthRate = Convert.ToDecimal(row["PhilHealthRate"]),
                PagIbigRate = Convert.ToDecimal(row["PagIbigRate"])
            };
        }

        public bool UpdateGovernmentRates(GovernmentRates rates)
        {
            string checkSql = "SELECT COUNT(*) FROM GovernmentRates";
            int count = Convert.ToInt32(DatabaseHelper.ExecuteScalar(checkSql) ?? 0);

            if (count == 0)
            {
                string insertSql = @"INSERT INTO GovernmentRates (SSSRate, PhilHealthRate, PagIbigRate)
                                     VALUES (@SSSRate, @PhilHealthRate, @PagIbigRate)";
                var insertParams = new SqlParameter[]
                {
                    new SqlParameter("@SSSRate", rates.SSSRate),
                    new SqlParameter("@PhilHealthRate", rates.PhilHealthRate),
                    new SqlParameter("@PagIbigRate", rates.PagIbigRate)
                };
                int rowsInserted = DatabaseHelper.ExecuteNonQuery(insertSql, insertParams);
                return rowsInserted > 0;
            }
            else
            {
                string updateSql = @"UPDATE GovernmentRates
                                     SET SSSRate = @SSSRate, PhilHealthRate = @PhilHealthRate, PagIbigRate = @PagIbigRate";
                var updateParams = new SqlParameter[]
                {
                    new SqlParameter("@SSSRate", rates.SSSRate),
                    new SqlParameter("@PhilHealthRate", rates.PhilHealthRate),
                    new SqlParameter("@PagIbigRate", rates.PagIbigRate)
                };
                int rowsUpdated = DatabaseHelper.ExecuteNonQuery(updateSql, updateParams);
                return rowsUpdated > 0;
            }
        }
    }
}
