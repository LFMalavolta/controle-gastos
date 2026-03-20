using Backend.Data;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransacaoController : ControllerBase
    {
        private readonly ControleGastosContext _context;

        public TransacaoController(ControleGastosContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transacao>>> GetTransacoes()
        {
            return await _context.Transacoes
                .Include(t => t.Pessoa)
                .Include(t => t.Categoria)
                .ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> CreateTransacao(Transacao transacao)
        {
            if (transacao.Valor <= 0)
                return BadRequest(new { mensagem = "Valor deve ser positivo" });

            var pessoa = await _context.Pessoas.FindAsync(transacao.PessoaId);
            if (pessoa == null)
                return BadRequest(new { mensagem = "Pessoa não encontrada" });

            var transacaoEhReceita = transacao.Tipo == TipoTransacao.Receita;
            if (pessoa.Idade < 18 && transacaoEhReceita)
                return BadRequest(new { mensagem = "Menor de idade só pode ter despesas" });

            var categoria = await _context.Categorias.FindAsync(transacao.CategoriaId);
            if (categoria == null)
                return BadRequest(new { mensagem = "Categoria não encontrada" });

            var categoriaEhReceita = categoria.Finalidade == Finalidade.Receita;
            var transacaoEhDespesa = transacao.Tipo == TipoTransacao.Despesa;
            if (transacaoEhDespesa && categoriaEhReceita)
                return BadRequest(new { mensagem = "Categoria não permite despesa" });

            var categoriaEhDespesa = categoria.Finalidade == Finalidade.Despesa;
            if (transacaoEhReceita && categoriaEhDespesa)
                return BadRequest(new { mensagem = "Categoria não permite receita" });

            _context.Transacoes.Add(transacao);
            await _context.SaveChangesAsync();

            return Ok(transacao);
        }
    }
}