using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AttendanceCore.Entities
{
    public class Configuracion
    {
        public Configuracion()
        {
            ConfiguracionId = 0;
            Descripcion = string.Empty;
            Value = string.Empty;
        }
        public int ConfiguracionId { get; set; }
        public string Descripcion { get; set; }
        public string Value { get; set; }
    }
}
