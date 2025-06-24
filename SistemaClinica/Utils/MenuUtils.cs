using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaClinica
{
    public class MenuUtils
    {
        public void LimpiarPantalla()
        {
            Console.Clear();
        }

        public void ImprimirMenu(string nombre)
        {
            LimpiarPantalla();
            Console.WriteLine("╔══════════════════════════════════════════════════════════════╗");
            Console.WriteLine($"║                    {nombre.ToUpper()}                    ║");
            Console.WriteLine("╠══════════════════════════════════════════════════════════════╣");
            Console.WriteLine("║  PACIENTES:                                                 ║");
            Console.WriteLine("║    1. Agregar Paciente                                      ║");
            Console.WriteLine("║    2. Listar Pacientes                                      ║");
            Console.WriteLine("║    3. Actualizar Paciente                                   ║");
            Console.WriteLine("║    4. Eliminar Paciente                                     ║");
            Console.WriteLine("║                                                              ║");
            Console.WriteLine("║  MÉDICOS:                                                   ║");
            Console.WriteLine("║    5. Agregar Médico                                        ║");
            Console.WriteLine("║    6. Listar Médicos                                        ║");
            Console.WriteLine("║    7. Actualizar Médico                                     ║");
            Console.WriteLine("║    8. Eliminar Médico                                       ║");
            Console.WriteLine("║                                                              ║");
            Console.WriteLine("║  ESPECIALIDADES:                                            ║");
            Console.WriteLine("║    9. Agregar Especialidad                                  ║");
            Console.WriteLine("║   10. Listar Especialidades                                 ║");
            Console.WriteLine("║   11. Actualizar Especialidad                               ║");
            Console.WriteLine("║   12. Eliminar Especialidad                                 ║");
            Console.WriteLine("║                                                              ║");
            Console.WriteLine("║    0. Salir                                                 ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════════╝");
            Console.Write("Selecciona una opción: ");
        }

        public void MostrarMensaje(string mensaje, bool esError = false)
        {
            if (esError)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"❌ {mensaje}");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"✅ {mensaje}");
            }
            Console.ResetColor();
            Console.WriteLine("\nPresiona cualquier tecla para continuar...");
            Console.ReadKey();
        }
    }
}
