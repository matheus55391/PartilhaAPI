using System;
using System.ComponentModel.DataAnnotations;

namespace PartilhaAPI.DTOs
{
    public class CreateDebtDto
    {
        [Required]
        public Guid CreatedBy { get; set; }

        [Required]
        public Guid PaidBy { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor total deve ser maior que zero.")]
        public decimal TotalAmount { get; set; } 

        [Required]
        public string Description { get; set; } 

        [Required]
        public Guid[] InvolvedUserIds { get; set; }
    }
}
