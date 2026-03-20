using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Backend.Models
{
    public enum TipoTransacao
    {
        Despesa = 0,
        Receita = 1
    }

    public class Transacao
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(400)]
        public string Descricao { get; set; } = string.Empty;

        [Range(0.01, double.MaxValue)]
        public decimal Valor { get; set; }

        public TipoTransacao Tipo { get; set; }

        [ForeignKey("Categoria")]
        public int CategoriaId { get; set; }

        [JsonIgnore]
        public Categoria? Categoria { get; set; }

        [ForeignKey("Pessoa")]
        public int PessoaId { get; set; }

        [JsonIgnore]
        public Pessoa? Pessoa { get; set; }
    }
}