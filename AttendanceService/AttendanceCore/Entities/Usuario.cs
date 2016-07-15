using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AttendanceCore.Entities
{
    public class Usuario
    {
        public Usuario()
        {
            Nombre = string.Empty;
            NombreUsuario = string.Empty;
            Password = string.Empty;
            FechaSesion = string.Empty;
            idUsuario = 0;
        }
        public string Nombre { get; set; }
        public string NombreUsuario { get; set; }
        public string Password { get; set; }
        public string FechaSesion { get; set; }
        public int idUsuario { get; set; } 
    }
}
