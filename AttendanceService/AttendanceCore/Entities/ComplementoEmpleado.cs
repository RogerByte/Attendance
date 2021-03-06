﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AttendanceCore.Entities
{
    public class ComplementoEmpleado
    {
        public ComplementoEmpleado()
        {
            EmpleadoId = 0;
            RazonSocial = string.Empty;
            Nomina = string.Empty;
            ClaveCentroCostos = string.Empty;
            DescripcionCentroCostos = string.Empty;
        }
        public int EmpleadoId { get; set; }
        public string RazonSocial { get; set; }
        public string Nomina { get; set; }
        public string ClaveCentroCostos { get; set; }
        public string DescripcionCentroCostos { get; set; }
    }
}
