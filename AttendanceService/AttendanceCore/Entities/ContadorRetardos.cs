using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AttendanceCore.Entities
{
    public class ContadorRetardos
    {
        public ContadorRetardos()
        {
            EmpleadoId = 0;
            NumeroEmpleado = 0;
            NombreEmpleado = string.Empty;
            NumeroRetardos = 0;
            Nomina = string.Empty;
            Compania = string.Empty;
        }
        public int EmpleadoId { get; set; }
        public int NumeroEmpleado { get; set; }
        public string NombreEmpleado { get; set; }
        public string Compania { get; set; }
        public string Nomina { get; set; }
        public int NumeroRetardos { get; set; }
    }
}
