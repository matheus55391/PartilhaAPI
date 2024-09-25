using System.ComponentModel.DataAnnotations;

namespace PartilhaAPI.Models
{
    public class TransactionMember
    {
        [Key]
        public Guid Id { get; set; }

        public Guid TransactionId { get; set; } // FK referenciando Transaction
        public Guid UserId { get; set; } // FK referenciando User
        public decimal AmountOwed { get; set; } // Valor que o membro deve

        // Propriedades de navegação
        public virtual Transaction Transaction { get; set; } // Transação referenciada
        public virtual User User { get; set; } // Usuário referenciado
    }
}
