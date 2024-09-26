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
        Task<Transaction> CreateTransactionAsync([FromBody] CreateTransactionDto transactionDto);
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

        public async Task<Transaction> CreateTransactionAsync([FromBody] CreateTransactionDto transactionDto)
        {
            throw new NotImplementedException();
            // Aqui você pode adicionar regras de negócio antes de salvar
            //return await _transactionRepository.CreateTransactionAsync(transaction);
        }
    }
}
