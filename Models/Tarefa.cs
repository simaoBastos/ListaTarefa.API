namespace ListaTarefa.API.Models
{
    public class Tarefa
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public bool Status { get; set; }
        public DateTime dataCriacao { get; set; } = DateTime.Now;

    }
}
