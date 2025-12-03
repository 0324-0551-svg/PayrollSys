using System.ComponentModel.DataAnnotations;

namespace PayrollSystem.Models
{
    public class CompanySettings
    {
        [Required]
        [StringLength(100)]
        public string CompanyName { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string Address { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string ContactNumber { get; set; } = string.Empty;
    }
}
