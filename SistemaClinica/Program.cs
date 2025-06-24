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
            MenuUtils menuUtils = new MenuUtils();
            PacienteUtils pacienteUtils = new PacienteUtils();
            MedicoUtils medicoUtils = new MedicoUtils();
            EspecialidadUtils especialidadUtils = new EspecialidadUtils();
            
            // Limpiar pantalla al iniciar
            menuUtils.LimpiarPantalla();
            
            int opcion = -1; // Inicializar con un valor por defecto
            do
            {
                try
                {
                    menuUtils.ImprimirMenu("Sistema Clinica");
                    opcion = int.Parse(Console.ReadLine());

                    switch (opcion)
                    {
                        case 1:
                            pacienteUtils.AgregarPaciente();
                            break;
                        case 2:
                            pacienteUtils.ListarPacientes();
                            break;
                        case 3:
                            pacienteUtils.ActualizarPaciente();
                            break;
                        case 4:
                            pacienteUtils.EliminarPaciente();
                            break;
                        case 5:
                            medicoUtils.AgregarMedico();
                            break;
                        case 6:
                            medicoUtils.ListarMedicos();
                            break;
                        case 7:
                            medicoUtils.ActualizarMedico();
                            break;
                        case 8:
                            medicoUtils.EliminarMedico();
                            break;
                        case 9:
                            especialidadUtils.AgregarEspecialidad();
                            break;
                        case 10:
                            especialidadUtils.ListarEspecialidades();
                            break;
                        case 11:
                            especialidadUtils.ActualizarEspecialidad();
                            break;
                        case 12:
                            especialidadUtils.EliminarEspecialidad();
                            break;
                        case 0:
                            menuUtils.LimpiarPantalla();
                            Console.WriteLine("╔══════════════════════════════════════════════════════════════╗");
                            Console.WriteLine("║                    ¡HASTA LUEGO!                            ║");
                            Console.WriteLine("║              Gracias por usar el sistema                    ║");
                            Console.WriteLine("╚══════════════════════════════════════════════════════════════╝");
                            break;
                        default:
                            menuUtils.MostrarMensaje("Opción no válida. Por favor, selecciona una opción del menú.", true);
                            break;
                    }
                }
                catch (FormatException)
                {
                    menuUtils.MostrarMensaje("Error: Debes ingresar un número válido.", true);
                    opcion = -1; // Resetear para continuar el bucle
                }
                catch (Exception ex)
                {
                    menuUtils.MostrarMensaje($"Error inesperado: {ex.Message}", true);
                    opcion = -1; // Resetear para continuar el bucle
                }

            } while (opcion != 0);
        }
    }
}
