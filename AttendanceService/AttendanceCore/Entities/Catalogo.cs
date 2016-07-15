using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AttendanceCore.Entities
{
    public class Catalogo
    {
        public Catalogo()
        {
            id = 0;
            Descripcion = string.Empty;
        }
        public int id { get; set; }
        public string Descripcion { get; set; }
    }
}
