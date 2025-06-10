using System;
using SistemaClinica.Models;
using SistemaClinica.Repositories;
using System.Collections.Generic;

namespace SistemaClinica
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PacienteRepository pacienteRepo = new PacienteRepository();
            MedicoRepository medicoRepo = new MedicoRepository();

            int opcion;
            do
            {
                Console.WriteLine("\n--- Sistema Clinica ---");
                Console.WriteLine("1. Agregar Paciente");
                Console.WriteLine("2. Listar Pacientes");
                Console.WriteLine("3. Actualizar Paciente");
                Console.WriteLine("4. Eliminar Paciente");
                Console.WriteLine("5. Agregar Medico");
                Console.WriteLine("6. Listar Medicos");
                Console.WriteLine("7. Actualizar Medico");
                Console.WriteLine("8. Eliminar medico");
                Console.WriteLine("0. Salir");
                Console.Write("Selecciona una opción: ");
                opcion = int.Parse(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        AgregarPaciente(pacienteRepo);
                        break;
                    case 2:
                        ListarPacientes(pacienteRepo);
                        break;
                    case 3:
                        ActualizarPaciente(pacienteRepo);
                        break;
                    case 4:
                        EliminarPaciente(pacienteRepo);
                        break;
                    case 5:
                        AgregarMedico(medicoRepo);
                        break;
                    case 6:
                        ListarMedicos(medicoRepo);
                        break;
                    case 7:
                        ActualizarMedico(medicoRepo);
                        break;
                    case 8:
                        EliminarMedico(medicoRepo);
                        break;
                    case 0:
                        Console.WriteLine("Saliendo...");
                        break;
                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }

            } while (opcion != 0);
        }


        //Pacientes//

        static void AgregarPaciente(PacienteRepository repo)
        {
            Console.Write("Nombre: ");
            string nombre = Console.ReadLine();

            Console.Write("Apellido: ");
            string apellido = Console.ReadLine();

            Console.Write("Fecha de Nacimiento: ");
            string nacimiento = Console.ReadLine();

            Console.Write("DNI: ");
            string dni = Console.ReadLine();

            Console.Write("¿Tiene obra social? (s/n): ");
            string obraSocial = Console.ReadLine();

            if (obraSocial.ToLower() == "s")
            {
                Console.Write("Obra Social:");
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

                repo.Agregar(paciente);
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

                repo.Agregar(paciente);
            }

            Console.WriteLine("Paciente agregado con éxito.");
        }

        static void ListarPacientes(PacienteRepository repo)
        {
            List<Paciente> pacientes = repo.ObtenerTodos();

            foreach (var p in pacientes)
            {
                Console.WriteLine($"ID: {p.IdPaciente} - {p.Nombre} {p.Apellido} - Fecha de Nacimiento {p.FechaNacimiento} - DNI: {p.DNI} - Tipo: {p.TipoPaciente}");

                if (p is PacienteObraSocial obra)
                {
                    Console.WriteLine($"   Obra Social: {obra.NombreObraSocial} - Afiliado: {obra.NumeroDeAfiliado} - Recibo: {obra.UltimoReciboDeSueldo}");
                }
                else if (p is PacienteSinObraSocial sin)
                {
                    Console.WriteLine($"   Carnet: {sin.NumeroDeCarnet}");
                }
            }
        }

        static void ActualizarPaciente(PacienteRepository repo)
        {
            Console.Write("ID del paciente a actualizar: ");
            int idPaciente = int.Parse(Console.ReadLine());

            Paciente pacienteExistente = repo.ObtenerPorId(idPaciente);
            if (pacienteExistente == null)
            {
                Console.WriteLine("Paciente no encontrado");
                return;
            }

            Console.Write("Nuevo Nombre: ");
            pacienteExistente.Nombre = Console.ReadLine();

            Console.Write("Nuevo Apellido: ");
            pacienteExistente.Apellido = Console.ReadLine();

            Console.Write("Nuevo DNI: ");
            pacienteExistente.DNI = Console.ReadLine();

            Console.Write("Nueva Fecha de Nacimiento: ");
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

            repo.Actualizar(pacienteExistente);
            Console.WriteLine("Paciente actualizado con éxito.");
        }

        static void EliminarPaciente(PacienteRepository repo)
        {
            Console.Write("ID del paciente a eliminar: ");
            int id = int.Parse(Console.ReadLine());

            repo.Eliminar(id);
            Console.WriteLine("Paciente eliminado con éxito.");
        }

        //Medicos//

        static void AgregarMedico(MedicoRepository repo)
            {
                Console.Write("Nombre: ");
                string nombre = Console.ReadLine();

                Console.Write("Apellido: ");
                string apellido = Console.ReadLine();

                Console.Write("Matricula: ");
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

                repo.Agregar(medico);
                Console.WriteLine("Medico agregado con éxito.");
            }

        static void ListarMedicos(MedicoRepository repo)
        {
            List<Medico> medicos = repo.ObtenerTodos();

            foreach (var m in medicos)
            {
                Console.WriteLine($"ID: {m.IdMedico} - {m.Nombre} {m.Apellido} - Matricula: {m.Matricula} - Especialidad: {m.Especialidad}");
            }
        }
        static void EliminarMedico(MedicoRepository repo)
        {
            Console.Write("ID del medico a eliminar: ");
            int id = int.Parse(Console.ReadLine());

            repo.Eliminar(id);
            Console.WriteLine("Medico eliminado con éxito.");
        }

        static void ActualizarMedico(MedicoRepository repo)
        {
            Console.Write("ID del medico a actualizar: ");
            int id = int.Parse(Console.ReadLine());

            Medico medicoExistente = repo.ObtenerPorId(id);
            if (medicoExistente == null)
            {
                Console.WriteLine("Medico no encontrado.");
                return;
            }

            Console.Write("Nuevo Nombre: ");
            medicoExistente.Nombre = Console.ReadLine();

            Console.Write("Nuevo Apellido: ");
            medicoExistente.Apellido = Console.ReadLine();

            Console.Write("Nueva Matricula: ");
            medicoExistente.Matricula = Console.ReadLine();

            Console.Write("Nueva Especialidad: ");
            medicoExistente.Especialidad = Console.ReadLine();

            repo.Actualizar(medicoExistente);
            Console.WriteLine("Medico actualizado con éxito.");
        }
    }
}
