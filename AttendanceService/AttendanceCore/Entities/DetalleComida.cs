using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AttendanceCore.Entities
{
    public class DetalleComida
    {
        public DetalleComida()
        {
            FechaRegistro = string.Empty;
            LugarRegistro = string.Empty;
        }
        public string FechaRegistro { get; set; }
        public string LugarRegistro { get; set; }
    }
}
