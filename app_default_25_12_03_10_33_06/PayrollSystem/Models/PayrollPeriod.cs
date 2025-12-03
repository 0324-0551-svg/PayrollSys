using System;
using System.ComponentModel.DataAnnotations;

namespace PayrollSystem.Models
{
    public class PayrollPeriod
    {
        [Key]
        public int PayrollPeriodID { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public bool IsProcessed { get; set; }
    }
}
