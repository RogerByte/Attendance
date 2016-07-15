using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AttendanceCore.Entities
{
    public class DiasJustificados
    {
        public DiasJustificados()
        {
            EmpleadoId = 0;
            FechaDesde = new DateTime();
            NumeroDias = 0;
            Concepto = string.Empty;
        }
        public int EmpleadoId { get; set; }
        public DateTime FechaDesde { get; set; }
        public int NumeroDias { get; set; }
        public string Concepto { get; set; }
    }
}
