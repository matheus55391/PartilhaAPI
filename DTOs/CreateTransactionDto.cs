using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PartilhaAPI.DTOs
{
    public class CreateTransactionDto
    {
        [Required]
        [MaxLength(255)]
        public string Description { get; set; }

        [Required]
        public decimal TotalAmount { get; set; }

        [Required]
        public Guid PaidById { get; set; }

        [Required]
        public List<TransactionMemberDto> Members { get; set; }
    }

    public class TransactionMemberDto
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public decimal AmountOwed { get; set; }
    }
}
