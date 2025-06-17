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
        //Medicos//
        MedicoRepository Repository { get; set; }
        public MedicoUtils() {  Repository = new MedicoRepository(); }

        public void AgregarMedico()
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

            Repository.Agregar(medico);
            Console.WriteLine("Medico agregado con éxito.");
        }

        public void ListarMedicos()
        {
            List<Medico> medicos = Repository.ObtenerTodos();

            foreach (var m in medicos)
            {
                Console.WriteLine($"ID: {m.IdMedico} - {m.Nombre} {m.Apellido} - Matricula: {m.Matricula} - Especialidad: {m.Especialidad}");
            }
        }
        public void EliminarMedico()
        {
            Console.Write("ID del medico a eliminar: ");
            int id = int.Parse(Console.ReadLine());

            Repository.Eliminar(id);
            Console.WriteLine("Medico eliminado con éxito.");
        }

        public void ActualizarMedico()
        {
            Console.Write("ID del medico a actualizar: ");
            int id = int.Parse(Console.ReadLine());

            Medico medicoExistente = Repository.ObtenerPorId(id);
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

            Repository.Actualizar(medicoExistente);
            Console.WriteLine("Medico actualizado con éxito.");
        }
    }
}
