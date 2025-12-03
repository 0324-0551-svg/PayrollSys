using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace PayrollSystem.DataAccess
{
    public static class DatabaseHelper
    {
        private static readonly string ConnectionString;

        static DatabaseHelper()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["PayrollDB"]?.ConnectionString
                ?? throw new InvalidOperationException("Connection string 'PayrollDB' not found in configuration.");
        }

        public static DataTable ExecuteQuery(string sql, SqlParameter[]? parameters = null)
        {
            var dt = new DataTable();
            try
            {
                using var conn = new SqlConnection(ConnectionString);
                using var cmd = new SqlCommand(sql, conn);
                if (parameters != null)
                    cmd.Parameters.AddRange(parameters);
                using var adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                throw new DataException("Error executing query: " + ex.Message, ex);
            }
            return dt;
        }

        public static int ExecuteNonQuery(string sql, SqlParameter[]? parameters = null)
        {
            try
            {
                using var conn = new SqlConnection(ConnectionString);
                using var cmd = new SqlCommand(sql, conn);
                if (parameters != null)
                    cmd.Parameters.AddRange(parameters);
                conn.Open();
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new DataException("Error executing non-query: " + ex.Message, ex);
            }
        }

        public static object? ExecuteScalar(string sql, SqlParameter[]? parameters = null)
        {
            try
            {
                using var conn = new SqlConnection(ConnectionString);
                using var cmd = new SqlCommand(sql, conn);
                if (parameters != null)
                    cmd.Parameters.AddRange(parameters);
                conn.Open();
                return cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw new DataException("Error executing scalar: " + ex.Message, ex);
            }
        }
    }
}
