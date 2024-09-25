using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PartilhaAPI.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(255)]
        public string Email { get; set; }

        // Navigation properties
        public virtual ICollection<Friend> Friends { get; set; }
        public virtual ICollection<Transaction> TransactionsCreated { get; set; }
        public virtual ICollection<Transaction> TransactionsPaid { get; set; }
        public virtual ICollection<TransactionMember> TransactionMembers { get; set; }
    }
}
