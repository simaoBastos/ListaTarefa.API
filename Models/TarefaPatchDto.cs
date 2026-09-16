using System.ComponentModel.DataAnnotations;

namespace ListaTarefa.API.Models;

public class TarefaPatchDto
{
    [StringLength(75, ErrorMessage = "O título não pode ultrapassar 75 caracteres.")]
    public string? Titulo { get; set; }
    [StringLength(255, ErrorMessage = "A descrição deve ter no máximo 255 caracteres.")]
    public string? Descricao { get; set; }
    public bool? Concluido { get; set; }

}