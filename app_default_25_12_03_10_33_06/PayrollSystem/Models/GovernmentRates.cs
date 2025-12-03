using System.ComponentModel.DataAnnotations;

namespace PayrollSystem.Models
{
    public class GovernmentRates
    {
        [Required]
        [Range(0, 100)]
        public decimal SSSRate { get; set; }

        [Required]
        [Range(0, 100)]
        public decimal PhilHealthRate { get; set; }

        [Required]
        [Range(0, 100)]
        public decimal PagIbigRate { get; set; }
    }
}
