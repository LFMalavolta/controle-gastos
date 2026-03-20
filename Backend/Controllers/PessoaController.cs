using Backend.Data;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PessoaController : ControllerBase
    {
        private readonly ControleGastosContext _context;

        public PessoaController(ControleGastosContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pessoa>>> GetPessoas()
        {
            return await _context.Pessoas.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Pessoa>> CreatePessoa(Pessoa pessoa)
        {
            if (string.IsNullOrWhiteSpace(pessoa.Nome))
                return BadRequest("Nome é obrigatório");

            _context.Pessoas.Add(pessoa);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPessoas), new { id = pessoa.Id }, pessoa);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePessoa(int id, Pessoa pessoa)
        {
            var pessoaExistente = await _context.Pessoas.FindAsync(id);

            if (pessoaExistente == null)
                return NotFound();

            pessoaExistente.Nome = pessoa.Nome;
            pessoaExistente.Idade = pessoa.Idade;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePessoa(int id)
        {
            var pessoa = await _context.Pessoas
                .Include(p => p.Transacoes)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pessoa == null)
                return NotFound();

            // ao deletar pessoa → deletar transações
            _context.Transacoes.RemoveRange(pessoa.Transacoes);
            _context.Pessoas.Remove(pessoa);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}