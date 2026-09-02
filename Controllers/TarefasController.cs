using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System;
using ListaTarefa.API.Models;

namespace ListaTarefa.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarefasController : ControllerBase
    {
        private readonly string? _connectionString;
        public TarefasController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("EstoqueConnection");
        }

        [HttpGet]
        public IActionResult ListarTodos()
        {
            var tarefa = new List<Tarefa>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM Tarefa", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tarefa.Add(new Tarefa
                        {
                            Id = (int)reader["Id"],
                            Titulo = reader["Titulo"].ToString(),
                            Descricao = reader["Descricao"].ToString(),
                            Status = (bool)reader["Status"],
                            DataCriacao = (DateTime)reader["DataCriacao"]
                        });
                    }
                }
            }
            return Ok(tarefa);
        }

        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            using var command = new SqlCommand("SELECT * FROM Tarefa WHERE Id = @Id", connection);
            command.Parameters.AddWithValue("@Id", id);
            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    var tarefa = new Tarefa
                    {
                        Id = (int)reader["Id"],
                        Titulo = reader["Titulo"].ToString(),
                        Descricao = reader["Descricao"].ToString(),
                        Status = (bool)reader["Status"],
                        DataCriacao = (DateTime)reader["DataCriacao"]

                    };
                    return Ok(tarefa);
                }
            }
            // Se nenhum produto for encontrado, retorna NotFound
            return NotFound();

        }

        [HttpPost]
        public IActionResult CriarTarefa(Tarefa tarefa)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            var command = new SqlCommand(@"
                INSERT INTO Tarefas (Titulo, Descricao, Status, DataCriacao)
                VALUES
                (@Titulo, @Descricao, @Status, @DataCriacao)", connection);

            command.Parameters.AddWithValue("@Titulo", tarefa.Titulo);
            command.Parameters.AddWithValue("@Descricao", tarefa.Descricao);
            command.Parameters.AddWithValue("@Status", tarefa.Status);
            command.Parameters.AddWithValue("@DataCriacao", tarefa.DataCriacao);

            command.ExecuteNonQuery();

            return CreatedAtAction(nameof(BuscarPorId), new { id = tarefa.Id }, tarefa);
        }

        [HttpPatch("{id}")]
        public IActionResult AtualizarTarefa(int id, Tarefa tarefa)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            var command = new SqlCommand(@"", connection);

            return NoContent();

        }

        [HttpDelete("{id}")]
        public IActionResult DeletarTarefa(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            var command = new SqlCommand("DELETE * FROM Tarefas WHERE Id = @Id", connection);

            command.Parameters.AddWithValue("@Id", id);

            int linhasAfetadas = command.ExecuteNonQuery();

            if (linhasAfetadas == 0)
            {
                return NotFound();
            }
            
            return NoContent();
        }
    }
}
