using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AttendanceCore.Entities
{
    public class ReporteIncidencia
    {
        public ReporteIncidencia()
        {
            EmpleadoId = 0;
            NumeroEmpleado = 0;
            NombreEmpleado = string.Empty;
            Compania = string.Empty;
            Nomina = string.Empty;
            Fecha = string.Empty;
            Concepto = string.Empty;

            RazonSocial = string.Empty;
            ClaveCentroCostos = string.Empty;
            DescripcionCentroCostos = string.Empty;
        }
        public int EmpleadoId { get; set; }
        public int NumeroEmpleado { get; set; }
        public string NombreEmpleado { get; set; }
        public string Compania { get; set; }
        public string Nomina { get; set; }
        public string Fecha { get; set; }
        public string Concepto { get; set; }

        public string RazonSocial { get; set; }
        public string ClaveCentroCostos { get; set; }
        public string DescripcionCentroCostos { get; set; }
    }
}
