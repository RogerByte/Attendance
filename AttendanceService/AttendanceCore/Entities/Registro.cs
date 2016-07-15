using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AttendanceCore.Entities
{
    public class Registro
    {
        public Registro()
        {
            EmpleadoId = 0;
            VerifyMode = 0;
            InOutMode = 0;
            FechaRegistro = new DateTime();
        }
        public int EmpleadoId { get; set; }
        public int VerifyMode { get; set; }
        public int InOutMode { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
