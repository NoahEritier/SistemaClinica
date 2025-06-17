using MySql.Data.MySqlClient;
using SistemaClinica.Data;
using SistemaClinica.Models;
using System;
using System.Collections.Generic;

namespace SistemaClinica.Repositories
{
    public class EspecialidadRepository
    {
        public void Agregar(Especialidad especialidad)
        {
            using (var connection = DatabaseConnection.GetConnection())
            {
                string query = "INSERT INTO Especialidades (NombreEspecialidad, IdEspecialidadPadre) VALUES (@NombreEspecialidad, @IdEspecialidadPadre)";
                var cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@NombreEspecialidad", especialidad.NombreEspecialidad);
                cmd.Parameters.AddWithValue("@IdEspecialidadPadre", (object)especialidad.IdEspecialidadPadre ?? DBNull.Value);
                cmd.ExecuteNonQuery();

            }
        }

        public void Actualizar(Especialidad especialidad)
        {
            using (var connection = DatabaseConnection.GetConnection())
            {
                string query = "UPDATE Especialidades SET NombreEspecialidad = @NombreEspecialidad, IdEspecialidadPadre = @IdEspecialidadPadre WHERE IdEspecialidad = @IdEspecialidad";
                var cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@NombreEspecialidad", especialidad.NombreEspecialidad);
                cmd.Parameters.AddWithValue("@IdEspecialidadPadre", (object)especialidad.IdEspecialidadPadre ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@IdEspecialidad", especialidad.IdEspecialidad);
                cmd.ExecuteNonQuery();
            }
        }

        public void Eliminar(int id)
        {
            using (var connection = DatabaseConnection.GetConnection())
            {
                string query = "DELETE FROM Especialidades WHERE IdEspecialidad = @IdEspecialidad";
                var cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@IdEspecialidad", id);
                cmd.ExecuteNonQuery();
            }
        }

        public Especialidad ObtenerPorId(int id)
        {
            using (var connection = DatabaseConnection.GetConnection())
            {
                string query = "SELECT * FROM Especialidades WHERE IdEspecialidad = @IdEspecialidad";
                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@IdEspecialidad", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                            return EspecialidadMapper.Mapear(reader);
                    }
                }
            }
            return null;
        }

        public List<Especialidad> ObtenerTodas()
        {
            var lista = new List<Especialidad>();
            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                string query = "SELECT * FROM Especialidades";
                using (var cmd = new MySqlCommand(query, connection))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(EspecialidadMapper.Mapear(reader));
                    }
                }
            }
            return lista;
        }
    }
}