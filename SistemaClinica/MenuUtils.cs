using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaClinica
{
    public class MenuUtils
    {
        public void ImprimirMenu(string nombre)
        {
            Console.WriteLine("\n--- " + nombre +" ---");
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
        }
    }
}
