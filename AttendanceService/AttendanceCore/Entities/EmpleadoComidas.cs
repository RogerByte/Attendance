using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AttendanceCore.Entities
{
    public class EmpleadoComidas
    {
        public EmpleadoComidas()
        {
            EmpleadoId = 0;
            NumeroEmpleado = 0;
            NombreEmpleado = string.Empty;
            Compania = string.Empty;
            Nomina = string.Empty;
            NumeroComidas = 0;
        }
        public int EmpleadoId { get; set; }
        public int NumeroEmpleado { get; set; }
        public string NombreEmpleado { get; set; }
        public string Compania { get; set; }
        public string Nomina { get; set; }
        public int NumeroComidas { get; set; }
    }
}
