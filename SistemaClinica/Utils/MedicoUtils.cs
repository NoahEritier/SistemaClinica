using SistemaClinica.Models;
using SistemaClinica.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaClinica
{
    public class MedicoUtils
    {
        private MedicoRepository Repository { get; set; }
        private MenuUtils MenuUtils { get; set; }

        public MedicoUtils() 
        { 
            Repository = new MedicoRepository();
            MenuUtils = new MenuUtils();
        }

        public void AgregarMedico()
        {
            MenuUtils.LimpiarPantalla();
            Console.WriteLine("╔══════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                     AGREGAR MÉDICO                          ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════════╝\n");

            try
            {
                Console.Write("Nombre: ");
                string nombre = Console.ReadLine();

                Console.Write("Apellido: ");
                string apellido = Console.ReadLine();

                Console.Write("Matrícula: ");
                string matricula = Console.ReadLine();

                Console.Write("Especialidad: ");
                string especialidad = Console.ReadLine();

                var medico = new Medico
                {
                    Nombre = nombre,
                    Apellido = apellido,
                    Matricula = matricula,
                    Especialidad = especialidad
                };

                Repository.Agregar(medico);
                MenuUtils.MostrarMensaje("Médico agregado con éxito.");
            }
            catch (Exception ex)
            {
                MenuUtils.MostrarMensaje($"Error al agregar médico: {ex.Message}", true);
            }
        }

        public void ListarMedicos()
        {
            MenuUtils.LimpiarPantalla();
            Console.WriteLine("╔══════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                     LISTAR MÉDICOS                          ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════════╝\n");

            try
            {
                List<Medico> medicos = Repository.ObtenerTodos();

                if (medicos.Count == 0)
                {
                    Console.WriteLine("No hay médicos registrados.");
                }
                else
                {
                    foreach (var m in medicos)
                    {
                        Console.WriteLine($"ID: {m.IdMedico} - {m.Nombre} {m.Apellido} - Matrícula: {m.Matricula} - Especialidad: {m.Especialidad}");
                        Console.WriteLine();
                    }
                }
            }
            catch (Exception ex)
            {
                MenuUtils.MostrarMensaje($"Error al listar médicos: {ex.Message}", true);
            }

            Console.WriteLine("\nPresiona cualquier tecla para continuar...");
            Console.ReadKey();
        }

        public void EliminarMedico()
        {
            MenuUtils.LimpiarPantalla();
            Console.WriteLine("╔══════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                    ELIMINAR MÉDICO                          ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════════╝\n");

            try
            {
                Console.Write("ID del médico a eliminar: ");
                int id = int.Parse(Console.ReadLine());

                Medico medico = Repository.ObtenerPorId(id);
                if (medico == null)
                {
                    MenuUtils.MostrarMensaje("Médico no encontrado", true);
                    return;
                }

                Console.WriteLine($"\n¿Estás seguro de que quieres eliminar al Dr. {medico.Nombre} {medico.Apellido}? (s/n): ");
                string confirmacion = Console.ReadLine();

                if (confirmacion.ToLower() == "s")
                {
                    Repository.Eliminar(id);
                    MenuUtils.MostrarMensaje("Médico eliminado con éxito.");
                }
                else
                {
                    MenuUtils.MostrarMensaje("Operación cancelada.");
                }
            }
            catch (Exception ex)
            {
                MenuUtils.MostrarMensaje($"Error al eliminar médico: {ex.Message}", true);
            }
        }

        public void ActualizarMedico()
        {
            MenuUtils.LimpiarPantalla();
            Console.WriteLine("╔══════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                   ACTUALIZAR MÉDICO                         ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════════╝\n");

            try
            {
                Console.Write("ID del médico a actualizar: ");
                int id = int.Parse(Console.ReadLine());

                Medico medicoExistente = Repository.ObtenerPorId(id);
                if (medicoExistente == null)
                {
                    MenuUtils.MostrarMensaje("Médico no encontrado.", true);
                    return;
                }

                Console.WriteLine($"\nActualizando médico: Dr. {medicoExistente.Nombre} {medicoExistente.Apellido}\n");

                Console.Write("Nuevo Nombre: ");
                medicoExistente.Nombre = Console.ReadLine();

                Console.Write("Nuevo Apellido: ");
                medicoExistente.Apellido = Console.ReadLine();

                Console.Write("Nueva Matrícula: ");
                medicoExistente.Matricula = Console.ReadLine();

                Console.Write("Nueva Especialidad: ");
                medicoExistente.Especialidad = Console.ReadLine();

                Repository.Actualizar(medicoExistente);
                MenuUtils.MostrarMensaje("Médico actualizado con éxito.");
            }
            catch (Exception ex)
            {
                MenuUtils.MostrarMensaje($"Error al actualizar médico: {ex.Message}", true);
            }
        }
    }
}
