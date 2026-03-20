using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data
{
    public class ControleGastosContext : DbContext
    {
        public ControleGastosContext(DbContextOptions<ControleGastosContext> options)
            : base(options) { }

        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Transacao> Transacoes { get; set; }
    }
}