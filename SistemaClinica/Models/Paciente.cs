using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaClinica.Models
{
    public class Paciente
    {
        public int IdPaciente { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string DNI { get; set; }
    }

    public class PacienteObraSocial : Paciente
    {
        public string NumeroDeAfiliado { get; set; }
        public string UltimoReciboDeSueldo { get; set; }
    }

    public class PacienteSinObraSocial : Paciente
    {
        public string NumeroDeCarnet { get; set; }
    }
}
