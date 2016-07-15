using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AttendanceCore.Entities
{
    public class ComedorExcel
    {
        public ComedorExcel()
        {
            EmpleadoId = 0;
            NumeroEmpleado = string.Empty;
            NombreEmpleado = string.Empty;
            FechaRegistro = string.Empty;
            Lugar = string.Empty;
            ImporteEmpresa = 0;
            ImporteRetencion = 0;
            RazonSocial = string.Empty;
            Nomina = string.Empty;
            ClaveCentroCostos = string.Empty;
            DescripcionCentroCostos = string.Empty;
        }
        public int EmpleadoId { get; set; }
        public string NumeroEmpleado { get; set; }
        public string NombreEmpleado { get; set; }
        public string FechaRegistro { get; set; }
        public string Lugar { get; set; }
        public decimal ImporteEmpresa { get; set; }
        public decimal ImporteRetencion { get; set; }
        public string RazonSocial { get; set; }
        public string Nomina { get; set; }
        public string ClaveCentroCostos { get; set; }
        public string DescripcionCentroCostos { get; set; }
    }
}
