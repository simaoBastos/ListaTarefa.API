using System.ComponentModel.DataAnnotations;

namespace ListaTarefa.API.Models
{
    public class Tarefa
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "O Titulo é obrigatório.")]
        public string Titulo { get; set; } = string.Empty;
        [Required(ErrorMessage = "A descrição é obrigatória.")]
        public string Descricao { get; set; } = string.Empty;
        [Required(ErrorMessage = "O status é obrigatório.")]
        public bool Concluido { get; set; }
        public DateTime Dt_Criacao { get; set; } 

    }
}
