using PartilhaAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PartilhaAPI.Services
{
    public interface IDebtService
    {
        Task<Transaction> CreateDebtAsync();
        Task<IEnumerable<Transaction>> GetDebtsByUserIdAsync(Guid userId);
    }

    public class DebtService : IDebtService
    {
        // Aqui você pode injetar o AppDbContext
        // private readonly AppDbContext _context;

        // public DebtService(AppDbContext context)
        // {
        //     _context = context;
        // }

        public async Task<Transaction> CreateDebtAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Transaction>> GetDebtsByUserIdAsync(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
