using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PayrollSystem.Models
{
    public class Payroll
    {
        [Key]
        public int PayrollID { get; set; }

        [Required]
        [ForeignKey("Employee")]
        public int EmployeeID { get; set; }

        [Required]
        [ForeignKey("PayrollPeriod")]
        public int PayrollPeriodID { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal GrossPay { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Deductions { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal NetPay { get; set; }

        [Required]
        public DateTime PaymentDate { get; set; }

        public Employee? Employee { get; set; }
        public PayrollPeriod? PayrollPeriod { get; set; }

        // Method to calculate NetPay given GrossPay and Deductions
        public void CalculateNetPay()
        {
            NetPay = GrossPay - Deductions;
            if (NetPay < 0)
                NetPay = 0;
        }
    }
}
