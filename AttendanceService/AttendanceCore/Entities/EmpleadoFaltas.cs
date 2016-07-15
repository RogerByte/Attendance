using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AttendanceCore.Entities
{
    public class EmpleadoFaltas
    {
        public EmpleadoFaltas()
        {
            EmpleadoId = 0;
            NumeroEmpleado = 0;
            NombreEmpleado = string.Empty;
            Compania = string.Empty;
            Nomina = string.Empty;
            NumeroFaltas = 0;
            TipoFalta = string.Empty;
        }
        public int EmpleadoId { get; set; }
        public int NumeroEmpleado { get; set; }
        public string NombreEmpleado { get; set; }
        public string Compania { get; set; }
        public string Nomina { get; set; }
        public int NumeroFaltas { get; set; }
        public string TipoFalta { get; set; }
    }
}
