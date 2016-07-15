using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AttendanceCore.Entities
{
    public class DiaFeriado
    {
        public DiaFeriado()
        {
            DiaFeriadoId = 0;
            Descripcion = string.Empty;
            Fecha = string.Empty;
        }
        public int DiaFeriadoId { get; set; }
        public string Descripcion { get; set; }
        public string Fecha { get; set; }
    }
}
