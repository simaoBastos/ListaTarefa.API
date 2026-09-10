namespace ListaTarefa.API.Models;
public class TarefaPatchDto
{
    public string? Titulo { get; set; }
    public string? Descricao { get; set; }
    public bool? Concluido { get; set; }

}
