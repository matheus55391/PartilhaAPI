using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PartilhaAPI.Models
{
    public class Transaction
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Description { get; set; }

        [Required]
        public decimal TotalAmount { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        public Guid CreatedById { get; set; }

        [Required]
        public Guid PaidById { get; set; }

        // Navigation properties
        public virtual User CreatedBy { get; set; }
        public virtual User PaidBy { get; set; }
        public virtual ICollection<TransactionMember> Members { get; set; }
    }
}
