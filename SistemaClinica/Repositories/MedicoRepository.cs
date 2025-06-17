using SistemaClinica.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using SistemaClinica.Data;
using SistemaClinica.Mappers;

namespace SistemaClinica.Repositories
{
    public class MedicoRepository
    {
        public void Agregar(Medico medico)
        {
            using (var connection = DatabaseConnection.GetConnection())
            {
                string query = @"INSERT INTO Medicos (Nombre, Apellido, Matricula, Especialidad)
                                 VALUES (@nombre, @apellido, @matricula, @especialidad)";

                var cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@nombre", medico.Nombre);
                cmd.Parameters.AddWithValue("@apellido", medico.Apellido);
                cmd.Parameters.AddWithValue("@matricula", medico.Matricula);
                cmd.Parameters.AddWithValue("@especialidad", medico.Especialidad);
                cmd.ExecuteNonQuery();
            }
        }

        public Medico ObtenerPorId(int id)
        {
            using (var connection = DatabaseConnection.GetConnection())
            {
                string query = "SELECT * FROM medicos WHERE IdMedico = @Id";
                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("Id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MedicoMapper.Mapear(reader);
                        }
                    }
                }
                 
            }
            return null;
        }
        public List<Medico> ObtenerTodos()
        {
            var medicos = new List<Medico>();
            using (var connection = DatabaseConnection.GetConnection())
            {
                string query = "SELECT * FROM Medicos";
                var cmd = new MySqlCommand(query, connection);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        medicos.Add(MedicoMapper.Mapear(reader));
                    }
                }
            }
            return medicos;
        }

        public void Actualizar(Medico medico)
        {
            using (var connection = DatabaseConnection.GetConnection())
            {
                string query = @"UPDATE Medicos SET 
                                 Nombre=@nombre, Apellido=@apellido, Matricula=@matricula, Especialidad=@especialidad 
                                 WHERE IdMedico=@id";

                var cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", medico.IdMedico);
                cmd.Parameters.AddWithValue("@nombre", medico.Nombre);
                cmd.Parameters.AddWithValue("@apellido", medico.Apellido);
                cmd.Parameters.AddWithValue("@matricula", medico.Matricula);
                cmd.Parameters.AddWithValue("@especialidad", medico.Especialidad);
                cmd.ExecuteNonQuery();
            }
        }

        public void Eliminar(int idMedico)
        {
            using (var connection = DatabaseConnection.GetConnection())
            {
                string query = "DELETE FROM Medicos WHERE IdMedico=@id";
                var cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", idMedico);
                cmd.ExecuteNonQuery();
            }
        }
    }
}

