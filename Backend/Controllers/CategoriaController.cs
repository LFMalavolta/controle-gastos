using Backend.Data;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriaController : ControllerBase
    {
        private readonly ControleGastosContext _context;

        public CategoriaController(ControleGastosContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categoria>>> GetCategorias()
        {
            return await _context.Categorias.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Categoria>> CreateCategoria(Categoria categoria)
        {
            // Validação da descrição da categoria
            if (string.IsNullOrWhiteSpace(categoria.Descricao))
                return BadRequest("Descrição é obrigatória");

            _context.Categorias.Add(categoria);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCategorias), new { id = categoria.Id }, categoria);
        }
    }
}