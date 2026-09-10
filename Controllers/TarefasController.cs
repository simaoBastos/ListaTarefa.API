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
            _connectionString = configuration.GetConnectionString("ListaConnection");
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
                            Concluido = (bool)reader["Concluido"],
                            Dt_Criacao = (DateTime)reader["Dt_Criacao"]
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
                        Concluido = (bool)reader["Concluido"],
                        Dt_Criacao = (DateTime)reader["Dt_Criacao"]

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
                INSERT INTO Tarefa (Titulo, Descricao)
                VALUES
                (@Titulo, @Descricao)", connection);

            command.Parameters.AddWithValue("@Titulo", tarefa.Titulo);
            command.Parameters.AddWithValue("@Descricao", tarefa.Descricao);
            

            command.ExecuteNonQuery();

            return StatusCode(201, tarefa);
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
            var command = new SqlCommand(@"DELETE  FROM Tarefa WHERE Id = @Id", connection);

            command.Parameters.AddWithValue("@Id", id);

            command.ExecuteNonQuery();

            
            
            return NoContent();
        }
    }
}
