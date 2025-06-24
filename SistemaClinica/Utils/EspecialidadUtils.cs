using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaClinica.Models;
using SistemaClinica.Repositories;

namespace SistemaClinica
{
    public class EspecialidadUtils
    {
        private EspecialidadRepository Repository { get; set; }
        private MenuUtils MenuUtils { get; set; }

        public EspecialidadUtils() 
        { 
            Repository = new EspecialidadRepository();
            MenuUtils = new MenuUtils();
        }

        public void ListarEspecialidades()
        {
            MenuUtils.LimpiarPantalla();
            Console.WriteLine("╔══════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                  LISTAR ESPECIALIDADES                     ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════════╝\n");

            try
            {
                var especialidades = Repository.ObtenerTodas();

                if (especialidades.Count == 0)
                {
                    Console.WriteLine("No hay especialidades registradas.");
                }
                else
                {
                    foreach (var e in especialidades)
                    {
                        Console.WriteLine($"ID: {e.IdEspecialidad} - Nombre: {e.NombreEspecialidad} - PadreID: {e.IdEspecialidadPadre?.ToString() ?? "Ninguno"}");
                        Console.WriteLine();
                    }
                }
            }
            catch (Exception ex)
            {
                MenuUtils.MostrarMensaje($"Error al listar especialidades: {ex.Message}", true);
            }

            Console.WriteLine("\nPresiona cualquier tecla para continuar...");
            Console.ReadKey();
        }

        public void AgregarEspecialidad()
        {
            MenuUtils.LimpiarPantalla();
            Console.WriteLine("╔══════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                 AGREGAR ESPECIALIDAD                       ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════════╝\n");

            try
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
                MenuUtils.MostrarMensaje("Especialidad agregada con éxito.");
            }
            catch (Exception ex)
            {
                MenuUtils.MostrarMensaje($"Error al agregar especialidad: {ex.Message}", true);
            }
        }

        public void ActualizarEspecialidad()
        {
            MenuUtils.LimpiarPantalla();
            Console.WriteLine("╔══════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║               ACTUALIZAR ESPECIALIDAD                      ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════════╝\n");

            try
            {
                Console.Write("Ingrese ID de la especialidad a actualizar: ");
                int id = int.Parse(Console.ReadLine());

                var especialidad = Repository.ObtenerPorId(id);
                if (especialidad == null)
                {
                    MenuUtils.MostrarMensaje("Especialidad no encontrada.", true);
                    return;
                }

                Console.WriteLine($"\nActualizando especialidad: {especialidad.NombreEspecialidad}\n");

                Console.Write("Ingrese nuevo nombre de la especialidad: ");
                especialidad.NombreEspecialidad = Console.ReadLine();

                Console.Write("Ingrese nuevo ID de especialidad padre (o vacío si no tiene): ");
                string padreInput = Console.ReadLine();
                especialidad.IdEspecialidadPadre = string.IsNullOrEmpty(padreInput) ? (int?)null : int.Parse(padreInput);

                Repository.Actualizar(especialidad);
                MenuUtils.MostrarMensaje("Especialidad actualizada con éxito.");
            }
            catch (Exception ex)
            {
                MenuUtils.MostrarMensaje($"Error al actualizar especialidad: {ex.Message}", true);
            }
        }

        public void EliminarEspecialidad()
        {
            MenuUtils.LimpiarPantalla();
            Console.WriteLine("╔══════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                ELIMINAR ESPECIALIDAD                       ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════════╝\n");

            try
            {
                Console.Write("Ingrese ID de la especialidad a eliminar: ");
                int id = int.Parse(Console.ReadLine());

                var especialidad = Repository.ObtenerPorId(id);
                if (especialidad == null)
                {
                    MenuUtils.MostrarMensaje("Especialidad no encontrada", true);
                    return;
                }

                Console.WriteLine($"\n¿Estás seguro de que quieres eliminar la especialidad '{especialidad.NombreEspecialidad}'? (s/n): ");
                string confirmacion = Console.ReadLine();

                if (confirmacion.ToLower() == "s")
                {
                    Repository.Eliminar(id);
                    MenuUtils.MostrarMensaje("Especialidad eliminada con éxito.");
                }
                else
                {
                    MenuUtils.MostrarMensaje("Operación cancelada.");
                }
            }
            catch (Exception ex)
            {
                MenuUtils.MostrarMensaje($"Error al eliminar especialidad: {ex.Message}", true);
            }
        }
    }
}

