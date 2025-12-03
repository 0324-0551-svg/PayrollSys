using System;
using System.Data;
using System.Data.SqlClient;
using PayrollSystem.DataAccess;

namespace PayrollSystem.Services
{
    public class AuthenticationService
    {
        public bool Authenticate(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                return false;

            try
            {
                string sql = "SELECT PasswordHash, PasswordSalt FROM Users WHERE Username = @Username";
                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@Username", username)
                };
                var dt = DatabaseHelper.ExecuteQuery(sql, parameters);

                if (dt.Rows.Count == 0)
                    return false;

                var row = dt.Rows[0];
                var storedHash = row["PasswordHash"].ToString();
                var storedSalt = row["PasswordSalt"].ToString();

                if (storedHash == null || storedSalt == null)
                    return false;

                return VerifyPasswordHash(password, storedHash, storedSalt);
            }
            catch
            {
                return false;
            }
        }

        private bool VerifyPasswordHash(string password, string storedHash, string storedSalt)
        {
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(storedHash) || string.IsNullOrEmpty(storedSalt))
                return false;

            try
            {
                byte[] saltBytes = Convert.FromBase64String(storedSalt);
                using var hmac = new System.Security.Cryptography.HMACSHA512(saltBytes);
                byte[] computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                string computedHashString = Convert.ToBase64String(computedHash);

                return computedHashString == storedHash;
            }
            catch
            {
                return false;
            }
        }
    }
}
