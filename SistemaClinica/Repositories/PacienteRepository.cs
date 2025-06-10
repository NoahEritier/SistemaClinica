using SistemaClinica.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using SistemaClinica.Data;
using SistemaClinica.Enums;
using SistemaClinica.Mappers;

namespace SistemaClinica.Repositories
{
    public class PacienteRepository
    {
        public void Agregar(Paciente paciente)
        {
            using (var connection = DatabaseConnection.GetConnection())
            {
                string query = @"INSERT INTO Pacientes 
                (Nombre, Apellido, FechaNacimiento, DNI, TipoPaciente, NombreObraSocial, NumeroDeAfiliado, UltimoReciboDeSueldo, NumeroDeCarnet)
                VALUES (@nombre, @apellido, @nacimiento, @dni, @tipo, @nombreObraSocial, @afiliado, @recibo, @carnet)";

                var cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@nombre", paciente.Nombre);
                cmd.Parameters.AddWithValue("@apellido", paciente.Apellido);
                cmd.Parameters.AddWithValue("@nacimiento", paciente.FechaNacimiento);
                cmd.Parameters.AddWithValue("@dni", paciente.DNI);
                cmd.Parameters.AddWithValue("@tipo", paciente.TipoPaciente.ToString());

                if (paciente is PacienteObraSocial obra)
                {
                    cmd.Parameters.AddWithValue("@nombreObraSocial", obra.NombreObraSocial);
                    cmd.Parameters.AddWithValue("@afiliado", obra.NumeroDeAfiliado);
                    cmd.Parameters.AddWithValue("@recibo", obra.UltimoReciboDeSueldo);
                    cmd.Parameters.AddWithValue("@carnet", null);
                }
                else if (paciente is PacienteSinObraSocial sin)
                {
                    cmd.Parameters.AddWithValue("@nombreObraSocial", null);
                    cmd.Parameters.AddWithValue("@afiliado", null);
                    cmd.Parameters.AddWithValue("@recibo", null);
                    cmd.Parameters.AddWithValue("@carnet", sin.NumeroDeCarnet);
                }

                cmd.ExecuteNonQuery();
            }
        }

        public Paciente ObtenerPorId(int id)
        {
            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                string query = "SELECT * FROM Pacientes WHERE IdPaciente = @Id";
                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return PacienteMapper.Mapear(reader);
                        }
                    }
                }
            }
            return null;
        }

        public List<Paciente> ObtenerTodos()
        {
            var pacientes = new List<Paciente>();
            using (var connection = DatabaseConnection.GetConnection())
            {
                string query = "SELECT * FROM Pacientes";
                var cmd = new MySqlCommand(query, connection);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        pacientes.Add(PacienteMapper.Mapear(reader));
                    }
                }
            }
            return pacientes;
        }

        public void Actualizar(Paciente paciente)
        {
            using (var connection = DatabaseConnection.GetConnection())
            {
                string query = @"UPDATE Pacientes SET 
                Nombre=@nombre, Apellido=@apellido, FechaNacimiento=@nacimiento DNI=@dni, TipoPaciente=@tipo,
                NombreObraSocial=@nombreObraSocial, NumeroDeAfiliado=@afiliado, UltimoReciboDeSueldo=@recibo, NumeroDeCarnet=@carnet
                WHERE IdPaciente=@id";

                var cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", paciente.IdPaciente);
                cmd.Parameters.AddWithValue("@nombre", paciente.Nombre);
                cmd.Parameters.AddWithValue("@apellido", paciente.Apellido);
                cmd.Parameters.AddWithValue("@nacimiento", paciente.FechaNacimiento);
                cmd.Parameters.AddWithValue("@dni", paciente.DNI);
                cmd.Parameters.AddWithValue("@tipo", paciente.TipoPaciente.ToString());

                if (paciente is PacienteObraSocial obra)
                {
                    cmd.Parameters.AddWithValue("@nombreObraSocial", obra.NombreObraSocial);
                    cmd.Parameters.AddWithValue("@afiliado", obra.NumeroDeAfiliado);
                    cmd.Parameters.AddWithValue("@recibo", obra.UltimoReciboDeSueldo);
                    cmd.Parameters.AddWithValue("@carnet", null);
                }
                else if (paciente is PacienteSinObraSocial sin)
                {
                    cmd.Parameters.AddWithValue("@nombreObraSocial", null);
                    cmd.Parameters.AddWithValue("@afiliado", null);
                    cmd.Parameters.AddWithValue("@recibo", null);
                    cmd.Parameters.AddWithValue("@carnet", sin.NumeroDeCarnet);
                }

                cmd.ExecuteNonQuery();
            }
        }

        public void Eliminar(int idPaciente)
        {
            using (var connection = DatabaseConnection.GetConnection())
            {
                string query = "DELETE FROM Pacientes WHERE IdPaciente=@id";
                var cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", idPaciente);
                cmd.ExecuteNonQuery();
            }
        }
    }
}

