using System;
using System.ComponentModel.DataAnnotations;

namespace PayrollSystem.Models
{
    public enum EmployeeStatus
    {
        Active = 1,
        Inactive = 2
    }

    public class Employee
    {
        [Key]
        public int EmployeeID { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        [StringLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last Name is required.")]
        [StringLength(50)]
        public string LastName { get; set; } = string.Empty;

        [StringLength(50)]
        public string? MiddleName { get; set; }

        [Required(ErrorMessage = "Position is required.")]
        [StringLength(50)]
        public string Position { get; set; } = string.Empty;

        [Required(ErrorMessage = "Department is required.")]
        [StringLength(50)]
        public string Department { get; set; } = string.Empty;

        [Required(ErrorMessage = "Date Hired is required.")]
        public DateTime DateHired { get; set; }

        [Required]
        public EmployeeStatus Status { get; set; }
    }
}
