using MySql.Data.MySqlClient;
using SistemaClinica.Models;

public static class EspecialidadMapper
{
    public static Especialidad Mapear(MySqlDataReader reader)
    {
        return new Especialidad
        {
            IdEspecialidad = reader.GetInt32("IdEspecialidad"),
            NombreEspecialidad = reader.GetString("NombreEspecialidad"),
            IdEspecialidadPadre = reader.IsDBNull(reader.GetOrdinal("IdEspecialidadPadre"))
                ? (int?)null: reader.GetInt32("IdEspecialidadPadre")
        };
    }
}

