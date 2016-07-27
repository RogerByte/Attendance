using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Attendance.Entities
{
    public class Solicitud
    {
        public Solicitud()
        {
            iSolicitudId = 0;
            iTipoSolicitud = 0;
            EmpleadoId = 0;
            FechaInicio = new DateTime();
            FechaFin = new DateTime();
            iEstatusSolicitud = 0;
            Cerrada = false;
            strTipoSolicitud = string.Empty;
            strEstatusSolicitud = string.Empty;
            strObservaciones = string.Empty;
        }
        public int iSolicitudId { get; set; }
        public int iTipoSolicitud { get; set; }
        public int EmpleadoId { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int iEstatusSolicitud { get; set; }
        public bool Cerrada { get; set; }
        public string strTipoSolicitud { get; set; }
        public string strEstatusSolicitud { get; set; }
        public string strObservaciones { get; set; }
    }
}