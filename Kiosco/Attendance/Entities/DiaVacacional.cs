using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Attendance.Entities
{
    public class DiaVacacional
    {
        public DiaVacacional()
        {
            Tomado = false;
        }
        public bool Tomado { get; set; }
    }
}