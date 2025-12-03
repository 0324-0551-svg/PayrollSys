using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PayrollSystem.Models
{
    public enum AttendanceStatus
    {
        Present = 1,
        Absent = 2,
        Leave = 3
    }

    public class Attendance
    {
        [Key]
        public int AttendanceID { get; set; }

        [Required]
        [ForeignKey("Employee")]
        public int EmployeeID { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public TimeSpan TimeIn { get; set; }

        [Required]
        public TimeSpan TimeOut { get; set; }

        [Required]
        public AttendanceStatus Status { get; set; }

        public Employee? Employee { get; set; }
    }
}
