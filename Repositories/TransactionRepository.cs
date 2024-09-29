using PartilhaAPI.Data;
using Microsoft.EntityFrameworkCore;
using PartilhaAPI.Models;

namespace PartilhaAPI.Repositories
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<Transaction>> GetAllTransactionsAsync();
        Task<Transaction> CreateTransactionAsync(Transaction transaction);
    }

    public class TransactionRepository : ITransactionRepository
    {
        private readonly AppDbContext _context;

        public TransactionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Transaction>> GetAllTransactionsAsync()
        {
            return await _context.Transactions
                .Include(t => t.CreatedBy)    // Inclui o usuário que criou a transação
                .Include(t => t.PaidBy)      // Inclui o usuário que pagou a transação
                .Include(t => t.Members)     // Inclui os membros da transação
                .ToListAsync();
        }

        public async Task<Transaction> CreateTransactionAsync(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            if (transaction.Members != null && transaction.Members.Count > 0)
            {
                _context.TransactionMembers.AddRange(transaction.Members);
            }
            await _context.SaveChangesAsync();
            return transaction;
        }
    }
}
