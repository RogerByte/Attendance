using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Attendance.ServiceContracts
{
    public class Incidencia
    {
        public Incidencia ()
        {
            id = 0;
            icono = string.Empty;
            descripcion = string.Empty;
            estatus = string.Empty;
            periodo = string.Empty;
        }
        public int id { get; set; }
        public string icono { get; set; }
        public string descripcion { get; set; }
        public string estatus { get; set; }
        public string periodo { get; set; }
    }
}