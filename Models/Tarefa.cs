using System.ComponentModel.DataAnnotations;

namespace ListaTarefa.API.Models;

public class Tarefa
{
    public int Id { get; set; }
    [Required(ErrorMessage = "O titulo é obrigatório.")]
    [StringLength(75, ErrorMessage = "O título não pode ultrapassar 75 caracteres.")]
    public string Titulo { get; set; } = string.Empty;
    [Required(ErrorMessage = "A descrição é obrigatória.")]
    [StringLength(255, ErrorMessage = "A descrição deve ter no máximo 255 caracteres.")]
    public string Descricao { get; set; } = string.Empty;
    public bool Concluido { get; set; }
    public DateTime Dt_Criacao { get; set; }

}
