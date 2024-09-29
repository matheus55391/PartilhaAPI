using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PartilhaAPI.DTOs;
using PartilhaAPI.Models;
using PartilhaAPI.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PartilhaAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class TransactionsController : BaseController
    {
        private readonly ITransactionService _transactionService;

        public TransactionsController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        // GET: api/transactions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetAllTransactions()
        {
            var transactions = await _transactionService.GetAllTransactionsAsync();
            return Ok(transactions);
        }

        // POST: api/transactions
        [HttpPost]
        public async Task<ActionResult<Transaction>> CreateTransaction([FromBody] CreateTransactionDto transactionDto)
        {
            var createdTransaction = await _transactionService.CreateTransactionAsync(transactionDto, LoggedUser);
            return CreatedAtAction(nameof(GetAllTransactions), new { id = createdTransaction.Id }, createdTransaction);
        }
    }
}
