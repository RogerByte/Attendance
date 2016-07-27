using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Attendance.Entities
{
    public class cat_solicitud
    {
        public cat_solicitud()
        {
            iTipoSolicitudId = 0;
            Descripcion = string.Empty;
            Dip = string.Empty;
            Activo = false;
        }
        public int iTipoSolicitudId { get; set; }
        public string Descripcion { get; set; }
        public string Dip { get; set; }
        public bool Activo { get; set; }
    }
}