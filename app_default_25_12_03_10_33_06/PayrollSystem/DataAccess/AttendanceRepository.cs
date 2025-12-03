using PayrollSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace PayrollSystem.DataAccess
{
    public class AttendanceRepository
    {
        public Attendance? GetAttendanceByEmployeeAndDate(int employeeId, DateTime date)
        {
            string sql = @"SELECT AttendanceID, EmployeeID, Date, TimeIn, TimeOut, Status 
                           FROM Attendance 
                           WHERE EmployeeID = @EmployeeID AND Date = @Date";
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@EmployeeID", employeeId),
                new SqlParameter("@Date", date.Date)
            };
            var dt = DatabaseHelper.ExecuteQuery(sql, parameters);
            if (dt.Rows.Count == 0)
                return null;

            var row = dt.Rows[0];
            return new Attendance
            {
                AttendanceID = Convert.ToInt32(row["AttendanceID"]),
                EmployeeID = Convert.ToInt32(row["EmployeeID"]),
                Date = Convert.ToDateTime(row["Date"]),
                TimeIn = (TimeSpan)row["TimeIn"],
                TimeOut = (TimeSpan)row["TimeOut"],
                Status = (AttendanceStatus)Convert.ToInt32(row["Status"])
            };
        }

        public bool AddAttendance(Attendance att)
        {
            string sql = @"INSERT INTO Attendance (EmployeeID, Date, TimeIn, TimeOut, Status)
                           VALUES (@EmployeeID, @Date, @TimeIn, @TimeOut, @Status)";
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@EmployeeID", att.EmployeeID),
                new SqlParameter("@Date", att.Date.Date),
                new SqlParameter("@TimeIn", att.TimeIn),
                new SqlParameter("@TimeOut", att.TimeOut),
                new SqlParameter("@Status", (int)att.Status)
            };
            int rowsAffected = DatabaseHelper.ExecuteNonQuery(sql, parameters);
            return rowsAffected > 0;
        }

        public bool UpdateAttendance(Attendance att)
        {
            string sql = @"UPDATE Attendance 
                           SET TimeIn = @TimeIn, TimeOut = @TimeOut, Status = @Status
                           WHERE AttendanceID = @AttendanceID";
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@TimeIn", att.TimeIn),
                new SqlParameter("@TimeOut", att.TimeOut),
                new SqlParameter("@Status", (int)att.Status),
                new SqlParameter("@AttendanceID", att.AttendanceID)
            };
            int rowsAffected = DatabaseHelper.ExecuteNonQuery(sql, parameters);
            return rowsAffected > 0;
        }

        public List<Attendance> GetAttendanceByPeriod(DateTime start, DateTime end)
        {
            var attendances = new List<Attendance>();
            string sql = @"SELECT AttendanceID, EmployeeID, Date, TimeIn, TimeOut, Status 
                           FROM Attendance 
                           WHERE Date >= @Start AND Date <= @End";
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@Start", start.Date),
                new SqlParameter("@End", end.Date)
            };
            var dt = DatabaseHelper.ExecuteQuery(sql, parameters);
            foreach (DataRow row in dt.Rows)
            {
                attendances.Add(new Attendance
                {
                    AttendanceID = Convert.ToInt32(row["AttendanceID"]),
                    EmployeeID = Convert.ToInt32(row["EmployeeID"]),
                    Date = Convert.ToDateTime(row["Date"]),
                    TimeIn = (TimeSpan)row["TimeIn"],
                    TimeOut = (TimeSpan)row["TimeOut"],
                    Status = (AttendanceStatus)Convert.ToInt32(row["Status"])
                });
            }
            return attendances;
        }
    }
}
