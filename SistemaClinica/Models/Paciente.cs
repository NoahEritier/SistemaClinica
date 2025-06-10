using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaClinica.Enums;

namespace SistemaClinica.Models
{
    public class Paciente
    {
        public int IdPaciente { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaNacimiento {  get; set; }
        public string DNI { get; set; }
        public TipoPaciente TipoPaciente { get; set; }
    }

    public class PacienteObraSocial : Paciente
    {
        public string NombreObraSocial { get; set; }
        public string NumeroDeAfiliado { get; set; }
        public string UltimoReciboDeSueldo { get; set; }

        public PacienteObraSocial()
        {
            TipoPaciente = TipoPaciente.ConObraSocial;
        }
    }

    public class PacienteSinObraSocial : Paciente
    {
        public string NumeroDeCarnet { get; set; }

        public PacienteSinObraSocial()
        {
            TipoPaciente = TipoPaciente.SinObraSocial;
        }
    }
}
