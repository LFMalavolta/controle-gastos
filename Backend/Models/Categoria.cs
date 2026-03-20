using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public enum Finalidade
    {
        Despesa = 0,
        Receita = 1,
        Ambas = 2
    }

    public class Categoria
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(400)]
        public string Descricao { get; set; } = string.Empty;

        public Finalidade Finalidade { get; set; }

        public List<Transacao> Transacoes { get; set; } = new();
    }
}