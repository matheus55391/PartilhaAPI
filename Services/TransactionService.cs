using Microsoft.AspNetCore.Mvc;
using PartilhaAPI.DTOs;
using PartilhaAPI.Models;
using PartilhaAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PartilhaAPI.Services
{
    public interface ITransactionService
    {
        Task<IEnumerable<Transaction>> GetAllTransactionsAsync();
        Task<Transaction> CreateTransactionAsync(CreateTransactionDto transactionDto, User loggedUser);
    }

    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<IEnumerable<Transaction>> GetAllTransactionsAsync()
        {
            return await _transactionRepository.GetAllTransactionsAsync();
        }

        public async Task<Transaction> CreateTransactionAsync(CreateTransactionDto transactionDto, User loggedUser)
        {

            var transaction = new Transaction
            {
                Id = Guid.NewGuid(),
                Description = transactionDto.Description,
                TotalAmount = transactionDto.TotalAmount,
                CreatedAt = DateTime.UtcNow,
                CreatedById = loggedUser.Id, 
                PaidById = transactionDto.PaidById,
                Members = transactionDto.Members.Select(member => new TransactionMember
                {
                    Id = Guid.NewGuid(),
                    UserId = member.UserId,
                    AmountOwed = member.AmountOwed
                }).ToList()
            };

            var createdTransaction = await _transactionRepository.CreateTransactionAsync(transaction);
            return createdTransaction;
        }
    }
}
