using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Attendance.ServiceContracts
{
    public class TipoSolicitud
    {
        public TipoSolicitud()
        {
            value = string.Empty;
            display = string.Empty;
        }
        public string value { get; set; }
        public string display { get; set; }
    }
}