using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class Pessoa
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Nome { get; set; } = string.Empty;

        [Range(0, 120)]
        public int Idade { get; set; }

        public List<Transacao> Transacoes { get; set; } = new();
    }
}