using SistemaClinica.Models;
using SistemaClinica.Repositories;
using System;
using System.Collections.Generic;

namespace SistemaClinica
{
    public class PacienteUtils
    {
        private PacienteRepository Repository { get; set; }
        private MenuUtils MenuUtils { get; set; }

        public PacienteUtils()
        {
            Repository = new PacienteRepository();
            MenuUtils = new MenuUtils();
        }
                
        public void AgregarPaciente()
        {
            MenuUtils.LimpiarPantalla();
            Console.WriteLine("╔══════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                    AGREGAR PACIENTE                        ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════════╝\n");

            Console.Write("Nombre: ");
            string nombre = Console.ReadLine();

            Console.Write("Apellido: ");
            string apellido = Console.ReadLine();

            Console.Write("Fecha de Nacimiento (dd/mm/yyyy): ");
            string nacimiento = Console.ReadLine();

            Console.Write("DNI: ");
            string dni = Console.ReadLine();

            Console.Write("¿Tiene obra social? (s/n): ");
            string obraSocial = Console.ReadLine();

            try
            {
                if (obraSocial.ToLower() == "s")
                {
                    Console.Write("Obra Social: ");
                    string nombreObraSocial = Console.ReadLine();

                    Console.Write("Número de Afiliado: ");
                    string afiliado = Console.ReadLine();

                    Console.Write("Ultimo Recibo de Sueldo: ");
                    string recibo = Console.ReadLine();

                    var paciente = new PacienteObraSocial
                    {
                        Nombre = nombre,
                        Apellido = apellido,
                        FechaNacimiento = Convert.ToDateTime(nacimiento),
                        DNI = dni,
                        NombreObraSocial = nombreObraSocial,
                        NumeroDeAfiliado = afiliado,
                        UltimoReciboDeSueldo = recibo
                    };

                    Repository.Agregar(paciente);
                }
                else
                {
                    Console.Write("Número de Carnet: ");
                    string carnet = Console.ReadLine();

                    var paciente = new PacienteSinObraSocial
                    {
                        Nombre = nombre,
                        Apellido = apellido,
                        FechaNacimiento = Convert.ToDateTime(nacimiento),
                        DNI = dni,
                        NumeroDeCarnet = carnet
                    };

                    Repository.Agregar(paciente);
                }

                MenuUtils.MostrarMensaje("Paciente agregado con éxito.");
            }
            catch (Exception ex)
            {
                MenuUtils.MostrarMensaje($"Error al agregar paciente: {ex.Message}", true);
            }
        }

        public void ListarPacientes()
        {
            MenuUtils.LimpiarPantalla();
            Console.WriteLine("╔══════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                    LISTAR PACIENTES                        ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════════╝\n");

            try
            {
                List<Paciente> pacientes = Repository.ObtenerTodos();

                if (pacientes.Count == 0)
                {
                    Console.WriteLine("No hay pacientes registrados.");
                }
                else
                {
                    foreach (var p in pacientes)
                    {
                        Console.WriteLine($"ID: {p.IdPaciente} - {p.Nombre} {p.Apellido} - Fecha de Nacimiento: {p.FechaNacimiento:dd/MM/yyyy} - DNI: {p.DNI} - Tipo: {p.TipoPaciente}");

                        if (p is PacienteObraSocial obra)
                        {
                            Console.WriteLine($"   Obra Social: {obra.NombreObraSocial} - Afiliado: {obra.NumeroDeAfiliado} - Recibo: {obra.UltimoReciboDeSueldo}");
                        }
                        else if (p is PacienteSinObraSocial sin)
                        {
                            Console.WriteLine($"   Carnet: {sin.NumeroDeCarnet}");
                        }
                        Console.WriteLine();
                    }
                }
            }
            catch (Exception ex)
            {
                MenuUtils.MostrarMensaje($"Error al listar pacientes: {ex.Message}", true);
            }

            Console.WriteLine("\nPresiona cualquier tecla para continuar...");
            Console.ReadKey();
        }

        public void ActualizarPaciente()
        {
            MenuUtils.LimpiarPantalla();
            Console.WriteLine("╔══════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                  ACTUALIZAR PACIENTE                       ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════════╝\n");

            try
            {
                Console.Write("ID del paciente a actualizar: ");
                int idPaciente = int.Parse(Console.ReadLine());

                Paciente pacienteExistente = Repository.ObtenerPorId(idPaciente);
                if (pacienteExistente == null)
                {
                    MenuUtils.MostrarMensaje("Paciente no encontrado", true);
                    return;
                }

                Console.WriteLine($"\nActualizando paciente: {pacienteExistente.Nombre} {pacienteExistente.Apellido}\n");

                Console.Write("Nuevo Nombre: ");
                pacienteExistente.Nombre = Console.ReadLine();

                Console.Write("Nuevo Apellido: ");
                pacienteExistente.Apellido = Console.ReadLine();

                Console.Write("Nuevo DNI: ");
                pacienteExistente.DNI = Console.ReadLine();

                Console.Write("Nueva Fecha de Nacimiento (dd/mm/yyyy): ");
                pacienteExistente.FechaNacimiento = DateTime.Parse(Console.ReadLine());

                if (pacienteExistente is PacienteObraSocial obra)
                {
                    Console.Write("Nombre Obra Social: ");
                    obra.NombreObraSocial = Console.ReadLine();

                    Console.Write("Nuevo Número de Afiliado: ");
                    obra.NumeroDeAfiliado = Console.ReadLine();

                    Console.Write("Nuevo Recibo de Sueldo: ");
                    obra.UltimoReciboDeSueldo = Console.ReadLine();
                }
                else if (pacienteExistente is PacienteSinObraSocial sin)
                {
                    Console.Write("Nuevo Número de Carnet: ");
                    sin.NumeroDeCarnet = Console.ReadLine();
                }

                Repository.Actualizar(pacienteExistente);
                MenuUtils.MostrarMensaje("Paciente actualizado con éxito.");
            }
            catch (Exception ex)
            {
                MenuUtils.MostrarMensaje($"Error al actualizar paciente: {ex.Message}", true);
            }
        }

        public void EliminarPaciente()
        {
            MenuUtils.LimpiarPantalla();
            Console.WriteLine("╔══════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                   ELIMINAR PACIENTE                        ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════════╝\n");

            try
            {
                Console.Write("ID del paciente a eliminar: ");
                int id = int.Parse(Console.ReadLine());

                Paciente paciente = Repository.ObtenerPorId(id);
                if (paciente == null)
                {
                    MenuUtils.MostrarMensaje("Paciente no encontrado", true);
                    return;
                }

                Console.WriteLine($"\n¿Estás seguro de que quieres eliminar a {paciente.Nombre} {paciente.Apellido}? (s/n): ");
                string confirmacion = Console.ReadLine();

                if (confirmacion.ToLower() == "s")
                {
                    Repository.Eliminar(id);
                    MenuUtils.MostrarMensaje("Paciente eliminado con éxito.");
                }
                else
                {
                    MenuUtils.MostrarMensaje("Operación cancelada.");
                }
            }
            catch (Exception ex)
            {
                MenuUtils.MostrarMensaje($"Error al eliminar paciente: {ex.Message}", true);
            }
        }
    }
}
