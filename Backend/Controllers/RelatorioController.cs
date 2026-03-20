using Backend.Data;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RelatorioController : ControllerBase
    {
        private readonly ControleGastosContext _context;

        public RelatorioController(ControleGastosContext context)
        {
            _context = context;
        }

        [HttpGet("pessoas")]
        public async Task<ActionResult> GetTotaisPorPessoa()
        {
            var pessoas = await _context.Pessoas
                .Include(p => p.Transacoes)
                .ToListAsync();

            var totaisPorPessoa = pessoas.Select(p => new
            {
                Pessoa = p.Nome,
                TotalReceitas = p.Transacoes
                    .Where(t => t.Tipo == TipoTransacao.Receita)
                    .Sum(t => t.Valor),

                TotalDespesas = p.Transacoes
                    .Where(t => t.Tipo == TipoTransacao.Despesa)
                    .Sum(t => t.Valor),

                Saldo = p.Transacoes
                    .Where(t => t.Tipo == TipoTransacao.Receita)
                    .Sum(t => t.Valor)
                    -
                    p.Transacoes
                    .Where(t => t.Tipo == TipoTransacao.Despesa)
                    .Sum(t => t.Valor)
            });

            var totalGeral = new
            {
                TotalReceitas = pessoas
                    .SelectMany(p => p.Transacoes)
                    .Where(t => t.Tipo == TipoTransacao.Receita)
                    .Sum(t => t.Valor),

                TotalDespesas = pessoas
                    .SelectMany(p => p.Transacoes)
                    .Where(t => t.Tipo == TipoTransacao.Despesa)
                    .Sum(t => t.Valor),

                Saldo = pessoas
                    .SelectMany(p => p.Transacoes)
                    .Where(t => t.Tipo == TipoTransacao.Receita)
                    .Sum(t => t.Valor)
                    -
                    pessoas
                    .SelectMany(p => p.Transacoes)
                    .Where(t => t.Tipo == TipoTransacao.Despesa)
                    .Sum(t => t.Valor)
            };

            return Ok(new
            {
                Pessoas = totaisPorPessoa,
                TotalGeral = totalGeral
            });
        }

        [HttpGet("categorias")]
        public async Task<ActionResult> GetTotaisPorCategoria()
        {
            var categorias = await _context.Categorias
                .Include(c => c.Transacoes)
                .ToListAsync();

            var resultado = categorias.Select(c => new
            {
                Categoria = c.Descricao,

                TotalReceitas = c.Transacoes
                    .Where(t => t.Tipo == TipoTransacao.Receita)
                    .Sum(t => t.Valor),

                TotalDespesas = c.Transacoes
                    .Where(t => t.Tipo == TipoTransacao.Despesa)
                    .Sum(t => t.Valor),

                Saldo = c.Transacoes
                    .Where(t => t.Tipo == TipoTransacao.Receita)
                    .Sum(t => t.Valor)
                    -
                    c.Transacoes
                    .Where(t => t.Tipo == TipoTransacao.Despesa)
                    .Sum(t => t.Valor)
            });

            var totalGeral = new
            {
                TotalReceitas = categorias
                    .SelectMany(c => c.Transacoes)
                    .Where(t => t.Tipo == TipoTransacao.Receita)
                    .Sum(t => t.Valor),

                TotalDespesas = categorias
                    .SelectMany(c => c.Transacoes)
                    .Where(t => t.Tipo == TipoTransacao.Despesa)
                    .Sum(t => t.Valor),

                Saldo = categorias
                    .SelectMany(c => c.Transacoes)
                    .Where(t => t.Tipo == TipoTransacao.Receita)
                    .Sum(t => t.Valor)
                    -
                    categorias
                    .SelectMany(c => c.Transacoes)
                    .Where(t => t.Tipo == TipoTransacao.Despesa)
                    .Sum(t => t.Valor)
            };

            return Ok(new
            {
                Categorias = resultado,
                TotalGeral = totalGeral
            });
        }
    }
}