using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AttendanceCore.Entities
{
    public class DetalleFaltas
    {
        public DetalleFaltas()
        {
            EmpleadoId = 0;
            NumeroEmpleado = 0;
            NombreEmpleado = string.Empty;
            Compania = string.Empty;
            Nomina = string.Empty;
            Fecha = string.Empty;
            TipoFalta = string.Empty;
            NumeroFalta = 0;
        }
        public int EmpleadoId { get; set; }
        public int NumeroEmpleado { get; set; }
        public string NombreEmpleado { get; set; }
        public string Compania { get; set; }
        public string Nomina { get; set; }
        public string Fecha { get; set; }
        public string TipoFalta { get; set; }
        public int NumeroFalta { get; set; }
    }
}
