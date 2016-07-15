using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AttendanceCore.Entities
{
    public class Hora
    {
        public Hora()
        {
            HoraId = 0;
            DescripcionHora = string.Empty;
        }
        public int HoraId { get; set; }
        public string DescripcionHora { get; set; }
    }
}
