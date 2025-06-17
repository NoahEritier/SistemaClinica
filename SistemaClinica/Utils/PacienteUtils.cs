using SistemaClinica.Models;
using SistemaClinica.Repositories;
using System;
using System.Collections.Generic;

namespace SistemaClinica
{
    public class PacienteUtils
    {
        private PacienteRepository Repository { get; set; }

        public PacienteUtils()
        {
            Repository = new PacienteRepository();
        }
                

        public void AgregarPaciente()
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

            Console.WriteLine("Paciente agregado con éxito.");
        }
        public void ListarPacientes()
        {
            List<Paciente> pacientes = Repository.ObtenerTodos();

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

        public void ActualizarPaciente()
        {
            Console.Write("ID del paciente a actualizar: ");
            int idPaciente = int.Parse(Console.ReadLine());

            Paciente pacienteExistente = Repository.ObtenerPorId(idPaciente);
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

            Repository.Actualizar(pacienteExistente);
            Console.WriteLine("Paciente actualizado con éxito.");
        }

        public void EliminarPaciente()
        {
            Console.Write("ID del paciente a eliminar: ");
            int id = int.Parse(Console.ReadLine());

            Repository.Eliminar(id);
            Console.WriteLine("Paciente eliminado con éxito.");
        }
    }
}
