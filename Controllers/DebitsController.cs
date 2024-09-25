using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PartilhaAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PartilhaAPI.Services;
using PartilhaAPI.DTOs;

namespace PartilhaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DebtsController : ControllerBase
    {
        private readonly IDebtService _debtService;

        public DebtsController(IDebtService debtService)
        {
            _debtService = debtService;
        }


        // POST: api/debts
        [HttpPost]
        public async Task<ActionResult<Transaction>> CreateDebt([FromBody] CreateDebtDto createDebtDto)
        {
            // Crie a dívida no serviço
            var createdDebt = await _debtService.CreateDebtAsync();

            // Retorna o resultado da criação
            return CreatedAtAction(nameof(GetDebtsByUserId), new { userId = createDebtDto.CreatedBy }, createdDebt);
        }

        // GET: api/debts/user/{userId}
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetDebtsByUserId(Guid userId)
        {
            var debts = await _debtService.GetDebtsByUserIdAsync(userId);
            return Ok(debts);
        }
    }
}
