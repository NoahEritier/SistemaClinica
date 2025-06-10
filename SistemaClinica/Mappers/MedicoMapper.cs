using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql;
using MySql.Data.MySqlClient;
using SistemaClinica.Models;

namespace SistemaClinica.Mappers
{
    public static class MedicoMapper
    {
        public static Medico Mapear(MySqlDataReader reader)
        {
            return new Medico
            {
                IdMedico = reader.GetInt32("IdMedico"),
                Nombre = reader.GetString("Nombre"),
                Apellido = reader.GetString("Apellido"),
                Matricula = reader.GetString("Matricula"),
                Especialidad = reader.GetString("Especialidad")
            };
        }
    }
}
