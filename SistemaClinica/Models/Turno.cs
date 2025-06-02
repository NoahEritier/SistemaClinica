using SistemaClinica.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaClinica.Models
{
    public class Turno
    {
        public int IdTurno { get; set; }
        public DateTime FechaHora { get; set; }
        public Paciente PacienteAsignado { get; set; }
        public Medico MedicoAsignado { get; set; }
        public EstadoConsultas Estado { get; set; }
    }
}
