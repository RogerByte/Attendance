using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AttendanceCore.Entities
{
    public class Retardo
    {
        public Retardo()
        {
            EmpleadoId = 0;
            FechaRetardo = string.Empty;
        }
        public int EmpleadoId { get; set; }
        public string FechaRetardo { get; set; }
    }
}
