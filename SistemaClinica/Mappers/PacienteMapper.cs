using MySql.Data.MySqlClient;
using SistemaClinica.Enums;
using SistemaClinica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaClinica.Mappers
{
    public static class PacienteMapper
    {
        public static Paciente Mapear(MySqlDataReader reader)
        {
            TipoPaciente tipo = (TipoPaciente)Enum.Parse(typeof(TipoPaciente), reader.GetString("TipoPaciente"));

            if (tipo == TipoPaciente.ConObraSocial)
            {
                return new PacienteObraSocial
                {
                    IdPaciente = reader.GetInt32("IdPaciente"),
                    Nombre = reader.GetString("Nombre"),
                    Apellido = reader.GetString("Apellido"),
                    DNI = reader.GetString("DNI"),
                    FechaNacimiento = reader.GetDateTime("FechaNacimiento"),
                    NombreObraSocial = reader["NombreObraSocial"] as string,
                    NumeroDeAfiliado = reader["NumeroDeAfiliado"] as string,
                    UltimoReciboDeSueldo = reader["UltimoReciboDeSueldo"] as string
                };
            }
            else
            {
                return new PacienteSinObraSocial
                {
                    IdPaciente = reader.GetInt32("IdPaciente"),
                    Nombre = reader.GetString("Nombre"),
                    Apellido = reader.GetString("Apellido"),
                    DNI = reader.GetString("DNI"),
                    FechaNacimiento = reader.GetDateTime("FechaNacimiento"),
                    NumeroDeCarnet = reader["NumeroDeCarnet"] as string
                };
            }
        }
    }
}
