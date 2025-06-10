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
                Console.WriteLine("3. Agregar Medico");
                Console.WriteLine("4. Listar Medicos");
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
                        AgregarMedico(medicoRepo);
                        break;
                    case 4:
                        ListarMedicos(medicoRepo);
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
    }
}
