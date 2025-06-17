using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaClinica.Models;
using SistemaClinica.Repositories;

public class EspecialidadUtils
{
    EspecialidadRepository Repository { get; set; }
    public EspecialidadUtils() { Repository = new EspecialidadRepository(); }
    public void ListarEspecialidades()
    {
        var especialidades = Repository.ObtenerTodas();
        foreach (var e in especialidades)
        {
            Console.WriteLine($"ID: {e.IdEspecialidad}, Nombre: {e.NombreEspecialidad}, PadreID: {e.IdEspecialidadPadre?.ToString() ?? "Ninguno"}");
        }
    }

    public void AgregarEspecialidad()
    {
        Console.Write("Ingrese nombre de la especialidad: ");
        string nombre = Console.ReadLine();

        Console.Write("Ingrese ID de especialidad padre (o vacío si no tiene): ");
        string padreInput = Console.ReadLine();
        int? idPadre = string.IsNullOrEmpty(padreInput) ? (int?)null : int.Parse(padreInput);
        var especialidad = new Especialidad
        {
            NombreEspecialidad = nombre,
            IdEspecialidadPadre = idPadre
        };
        Repository.Agregar(especialidad);
        Console.WriteLine("Especialidad agregada.");
    }

    public void ActualizarEspecialidad()
    {
        Console.Write("Ingrese ID de la especialidad a actualizar: ");
        int id = int.Parse(Console.ReadLine());

        var especialidad = Repository.ObtenerPorId(id);
        if (especialidad == null)
        {
            Console.WriteLine("Especialidad no encontrada.");
            return;
        }

        Console.Write("Ingrese nuevo nombre de la especialidad: ");
        especialidad.NombreEspecialidad = Console.ReadLine();

        Console.Write("Ingrese nuevo ID de especialidad padre (o vacío si no tiene): ");
        string padreInput = Console.ReadLine();
        especialidad.IdEspecialidadPadre = string.IsNullOrEmpty(padreInput) ? (int?)null : int.Parse(padreInput);

        Repository.Actualizar(especialidad);
        Console.WriteLine("Especialidad actualizada.");
    }

    public void EliminarEspecialidad()
    {
        Console.Write("Ingrese ID de la especialidad a eliminar: ");
        int id = int.Parse(Console.ReadLine());
        Repository.Eliminar(id);
        Console.WriteLine("Especialidad eliminada.");
    }
}

