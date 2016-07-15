using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AttendanceCore.Entities
{
    public class RegistroReporteComedor
    {
        public RegistroReporteComedor()
        {
            EmpleadoId = 0;
            Empleado = string.Empty;
            RazonSocial = string.Empty;
            Nomina = string.Empty;
            DIP = string.Empty;
            Nombre = string.Empty;
            Descripcion = string.Empty;
            Importe1 = string.Empty;
            FechaMovimiento = string.Empty;
            Referencia = string.Empty;
            NivelEstructura = string.Empty;
            Importe2 = string.Empty;
            Importe3 = string.Empty;
            SaldoActual = string.Empty;
            SaldoAnterior = string.Empty;
            ImporteCapturado = string.Empty;
        }
        public int EmpleadoId { get; set; }
        public string Empleado { get; set; }
        public string RazonSocial { get; set; }
        public string Nomina { get; set; }
        public string DIP { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Importe1 { get; set; }
        public string FechaMovimiento { get; set; }
        public string Referencia { get; set; }
        public string NivelEstructura { get; set; }
        public string Importe2 { get; set; }
        public string Importe3 { get; set; }
        public string SaldoActual { get; set; }
        public string SaldoAnterior { get; set; }
        public string ImporteCapturado { get; set; }
    }
}
